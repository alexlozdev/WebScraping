using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CarScraping
{
    class WebScrap
    {
        public static string DEF_LOG_FILE = "log.txt";
        private readonly string DEF_BASE_URL = "http://platesmania.com";
        private WebBrowser _webBrowser = null;
        private bool _isNavigateCompleted = true;

        private CountryInfo _countryInfo = null;
        public CountryInfo Country_Info
        {
            get => _countryInfo;
            set => _countryInfo = value;
        }

        private string _basePath = "";
        public string BasePath
        {
            get => _basePath;
            set => _basePath = value;
        }

        /// <summary>
        /// initialize before scraping
        /// </summary>
        public void Init(WebBrowser webBrowser)
        {
            if (_webBrowser == null)
            {
                _webBrowser = webBrowser;
                _webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(NavigateCompleteHandler);

            }

            //_webBrowser.Visible = true;
            //NativeMethods.AllocConsole();

        }        

        /// <summary>
        /// web browser navigate complete handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateCompleteHandler(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            _isNavigateCompleted = true;
        }

        /// <summary>
        /// get html content from web browser
        /// </summary>
        /// <param name="paramInterval"></param>
        /// <returns></returns>
        private System.Windows.Forms.HtmlDocument GetDocument(int paramInterval)
        {
            int cnt = 0;
            while (_webBrowser.ReadyState != WebBrowserReadyState.Complete )
            {
                Application.DoEvents();
                cnt++;
                Thread.Sleep(paramInterval);
            }

            return _webBrowser.Document;
        }

        public void DoScrapThread()
        {
            Thread thread = new Thread(new ThreadStart(DoScrap));
            //thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }



        public void DoScrap()
        {
            //_countryInfo = countryInfo;
            string homeUrl = string.Format("{0}/{1}/gallery", DEF_BASE_URL, _countryInfo.Acronym);
            
            GoUrl(homeUrl);
            HtmlDocument htmlDocument = GetDocument(2);

            // scrap page number
            int pageCnt = ScrapPageCount.GetPageCount(htmlDocument);

            // page includes 10 or less but page count = scraped number / 10
            pageCnt = pageCnt / 10;

            int carCount = 0;
                        
            for (int i = 0; i <= pageCnt; i++)
            {
                //string log = string.Format("\t{0} - Page {1} ", _countryInfo.Name,i+1);
                //Console.WriteLine(log);

                string pageUrl = string.Format("{0}/{1}/gallery-{2}", DEF_BASE_URL, _countryInfo.Acronym,i);
                GoUrl(pageUrl);
                htmlDocument = GetDocument(2);

                // scrap car info
                ScrapCarInfo scrapCarInfo = new ScrapCarInfo();
                List<CarInfo> arrCarInfo = scrapCarInfo.ScrapData(htmlDocument);

                // store image into local
                for (int j = 0; j < arrCarInfo.Count(); j++)
                {
                    CarInfo carInfo = arrCarInfo[j];

                    string log = string.Format("\t{0} - {1} ({2}) ", _countryInfo.Name, carInfo.Name, ++carCount);
                    Console.WriteLine(log);

                    // car image
                    bool isPlate = false;
                    StoreImage(carInfo, isPlate);

                    // car plate image
                    isPlate = true;
                    StoreImage(carInfo, isPlate);
                }
            }
        }

        private void GoUrl(string paramUrl)
        {
            // param check
            if (_webBrowser == null || string.IsNullOrEmpty(paramUrl))
                return;

            _webBrowser.Navigate(paramUrl);
            return;
        }

        private void StoreImage(CarInfo carInfo, bool isPlate)
        {
            string srcPath = "";
            string fileName = "";
            string countryName = "All";
            string carName = "NoName";

            Uri uri = null;
            string srcFileName = "";
            string srcFileExt = "";

            if (carInfo == null)
                return;
            
            // if car name doesn't exist
            if (string.IsNullOrEmpty(carInfo.Name))
            {
                carName = "NoName";
            } else
            {
                carName = carInfo.Name;
            }

            if (isPlate)
            {
                srcPath = carInfo.ImageCarPath;
                
                // get ext name form http source path
                uri = new Uri(srcPath);
                srcFileName = Path.GetFileNameWithoutExtension(uri.AbsolutePath);
                srcFileExt = Path.GetExtension(uri.AbsolutePath);

                fileName = string.Format("{0}_{1}", carName, srcFileName);                
            } else
            {
                srcPath = carInfo.ImagePlatePath;

                // get ext name form http source path
                uri = new Uri(srcPath);
                srcFileName = Path.GetFileNameWithoutExtension(uri.AbsolutePath);
                srcFileExt = Path.GetExtension(uri.AbsolutePath);

                fileName = string.Format("{0}_{1}_plate", carName, srcFileName);
            }

            if (_countryInfo != null)
            {
                countryName = _countryInfo.Name;
            }

            // destination path
            string dstPath = "";
            string validName = MakeValidPath(carName);
            if (_basePath.Length < 4)
                dstPath = string.Format("{0}{1}/{2}", _basePath, countryName, validName);
            else
                dstPath = string.Format("{0}/{1}/{2}", _basePath, countryName, validName);

            Directory.CreateDirectory(dstPath);
            

            fileName = MakeValidPath(fileName);

            string fullFileName = string.Format("{0}{1}", fileName, srcFileExt);            
            string validDstPath = string.Format("{0}/{1}", dstPath, fullFileName);

            // download image from http url
            using (WebClient webClient = new WebClient())
            {
                //string log = string.Format("\t\t{0} -> {1}", srcPath, validDstPath);
                string log = string.Format("\t\t{0}", validDstPath);

                try
                {
                    webClient.DownloadFile(srcPath, validDstPath);
                    Console.WriteLine(log);                    
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    log = string.Format("\t\t***Error***: {0} -> {1} \n", srcPath, validDstPath);
                    File.AppendAllText(DEF_LOG_FILE, log);
                }
                
            }
            
        }   
        
        private string MakeValidPath(string paramPath)
        {
            string result = "";
            if (string.IsNullOrEmpty(paramPath))
                return result;

            // convert special character into space to be valid path
            result = paramPath.Replace("\r\n", " ");
            result = result.Replace("\n", " ");
            result = result.Replace("\r", "");
            result = result.Replace("/", "-");
            result = result.Replace("\\", "");
            result = result.Replace(":", "");
            result = result.Replace("*", "");
            result = result.Replace("?", "");
            result = result.Replace("<", "");
            result = result.Replace(">", "");
            result = result.Replace("|", "");

            return result;
        }


    }
}
