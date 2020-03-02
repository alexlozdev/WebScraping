using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace CarScraping
{
    public partial class FormMain : Form
    {
        List<CountryInfo> _arrCountryInfos = new List<CountryInfo>();
        
        string _baseFolderPath = "D://platesmania.com";
        WebScrap _webScrap = new WebScrap();

        bool _isPrepared = false;

        public FormMain()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            // base path
            textBoxBasePath.Text = _baseFolderPath;            

            // init browser
            webBrowser.ScriptErrorsSuppressed = true;
            _webScrap.Init(webBrowser);
            string urlLang = "http://platesmania.com";
            webBrowser.Navigate(urlLang);

            // init log
            if (File.Exists(WebScrap.DEF_LOG_FILE))
            {
                File.Delete(WebScrap.DEF_LOG_FILE);
            }   

            // init the country info
            AddCountryInfo("Armenia", "am");
            AddCountryInfo("Austria", "at");
            AddCountryInfo("Azerbaijan", "az");
            AddCountryInfo("Belarus", "by");
            AddCountryInfo("Belgium", "be");
            AddCountryInfo("Bosnia and Herzegovina", "ba");
            AddCountryInfo("Bulgaria", "bg");
            AddCountryInfo("Canada", "ca");
            AddCountryInfo("China", "cn");
            AddCountryInfo("Croatia", "hr");
            AddCountryInfo("Cyprus", "cy");
            AddCountryInfo("Czech Republic", "cz");
            AddCountryInfo("Denmark", "dk");
            AddCountryInfo("Estonia", "ee");
            AddCountryInfo("Finland", "fi");
            AddCountryInfo("France", "fr");
            AddCountryInfo("Georgia", "ge");
            AddCountryInfo("Germany", "de");
            AddCountryInfo("Great Britain", "uk");
            AddCountryInfo("Greece", "gr");
            AddCountryInfo("Hong Kong", "hk");
            AddCountryInfo("Hungary", "hu");
            AddCountryInfo("Israel", "il");
            AddCountryInfo("Israel", "it");
            AddCountryInfo("Japan", "jp");
            AddCountryInfo("Kazakhstan", "kz");
            AddCountryInfo("Kyrgyzstan", "kg");
            AddCountryInfo("Latvia", "lv");
            AddCountryInfo("Lithuania", "lt");
            AddCountryInfo("Luxembourg", "lu");
            AddCountryInfo("Moldova", "md");
            AddCountryInfo("Monaco", "mc");
            AddCountryInfo("Mongolia", "mn");
            AddCountryInfo("Montenegro", "me");
            AddCountryInfo("Netherlands", "nl");
            AddCountryInfo("North Macedonia", "mk");
            AddCountryInfo("Norway", "no");
            AddCountryInfo("Poland", "pl");
            AddCountryInfo("Portugal", "pt");
            AddCountryInfo("Romania", "ro");
            AddCountryInfo("Russia", "ru");
            AddCountryInfo("Serbia", "rs");
            AddCountryInfo("Slovakia", "sk");
            AddCountryInfo("Slovenia", "si");
            AddCountryInfo("Spain", "es");
            AddCountryInfo("Sweden", "se");
            AddCountryInfo("Switzerland", "ch");
            AddCountryInfo("Thailand", "th");
            AddCountryInfo("Turkey", "tr");
            AddCountryInfo("UAE", "ae");
            AddCountryInfo("USA", "us");
            AddCountryInfo("USSR", "su");
            AddCountryInfo("Ukraine", "ua");
            AddCountryInfo("Uzbekistan", "uz");
            AddCountryInfo("Vietnam", "vn");
            AddCountryInfo("Ex-USSR", "xx");

            // country combobox
            comboBoxCountry.Items.Add("All");
            for (int i = 0; i < _arrCountryInfos.Count; i++)
            {
                comboBoxCountry.Items.Add(_arrCountryInfos[i].Name);
            }

            comboBoxCountry.SelectedIndex = 0;
            
        }
            

        /// <summary>
        /// Add country info into list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="acronym"></param>
        private void AddCountryInfo(string name, string acronym)
        {
            // check params
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(acronym))
            {
                return;
            }

            // create the new countryinfo
            CountryInfo countryInfo = new CountryInfo(name, acronym);
            _arrCountryInfos.Add(countryInfo);
        }

        private void EnableStartButton()
        {
            if(buttonScrap.InvokeRequired)
            {
                buttonScrap.Invoke(new Action(() =>
                    {
                        buttonScrap.Enabled = true;
                    }
                ));
            } else
            {
                buttonScrap.Enabled = true;
            }
        }

        private void SetCommenctLabelText(String comment)
        {
            if (labelComment.InvokeRequired)
            {
                labelComment.Invoke(new Action(() =>
                {
                    labelComment.Text = comment;
                }
                ));
            }
            else
            {
                labelComment.Text = comment;
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                _baseFolderPath = folderDlg.SelectedPath;
                textBoxBasePath.Text = _baseFolderPath;
                            
            }
        }

        private void buttonScrap0_Click(object sender, EventArgs e)
        {

            // set the destination base folder path            
            _webScrap.BasePath = _baseFolderPath;

            int countryIndex = comboBoxCountry.SelectedIndex;

            if (countryIndex == 0)
            {
                // country select
                for (int i = 0; i < _arrCountryInfos.Count(); i++)
                {
                    _webScrap.Country_Info = _arrCountryInfos[i];
                    string log = string.Format("\nCountry: {0} ", _webScrap.Country_Info.Name);
                    Console.WriteLine(log);

                    this.WindowState = FormWindowState.Minimized;
                    _webScrap.DoScrap();
                }
            } else
            {
                // 0 : all
                countryIndex = countryIndex - 1;

                _webScrap.Country_Info = _arrCountryInfos[countryIndex];
                string log = string.Format("\nCountry: {0} ", _webScrap.Country_Info.Name);
                Console.WriteLine(log);

                this.WindowState = FormWindowState.Minimized;
                _webScrap.DoScrap();
            }

            Console.WriteLine("Finished scrapping.");

        }

        private void comboBoxCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser0_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (!_isPrepared)
            {
                // check if ddos checking has completed
                String title = webBrowser.DocumentTitle;
                //Console.WriteLine("Title: " + title);
                if (title == "Photos of vehicles and license plates")
                {
                    _isPrepared = true;

                    EnableStartButton();
                    SetCommenctLabelText("Press [Start] button to start scrapping!");
                }
            }
        }
    }
}
