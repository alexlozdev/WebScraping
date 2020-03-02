using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CarScraping
{
    class ScrapCarInfo
    {
        private static string DEF_CAR_ATTRIBUTE = "className";
        private static string DEF_CAR_VALUE = "img-responsive center-block";

        private List<CarInfo> _arrCarInfo = new List<CarInfo>();       

        public List<CarInfo> ScrapData(HtmlDocument htmlDocument)
        {
            _arrCarInfo.Clear();
            
            // check param
            if (htmlDocument == null)
                return _arrCarInfo;

            // <div class="panel-body">
            var divElements = htmlDocument.GetElementsByTagName("div");
            if (divElements == null)
                return _arrCarInfo;

            foreach (HtmlElement divElement in divElements)
            {
                string strVal = divElement.GetAttribute("className");
                if (strVal.Equals("panel-body"))
                {
                    // scrap car name, image path, plate image path
                    string carName = ScrapCarName(divElement);
                    string carImagePath = ScrapCarImagePath(divElement);
                    string plateImagePath = ScrapPlateImagePath(divElement);
                    string user = ScrapUser(divElement);

                    CarInfo carInfo = new CarInfo(carName, carImagePath, plateImagePath, user);
                    _arrCarInfo.Add(carInfo);

                }
            }

            return _arrCarInfo;
        }

        /// <summary>
        /// get the car name from html
        /// </summary>
        /// <param name="htmlDocument"></param>
        /// <returns></returns>
        private string ScrapCarName(HtmlElement panelBodyElement)
        {
            string result = "";

            // check param
            if (panelBodyElement == null)
                return result;

            // <div class="panel-body">
            foreach (HtmlElement hDivElement in panelBodyElement.Children)
            {
                // <h4 class="text-center"> Mercedes-Benz</h4>
                if (hDivElement.TagName.ToLower().Equals("h4"))
                {
                    foreach (HtmlElement aElement in hDivElement.Children)
                    {
                        // <a href="/am/nomer14071374">Mercedes_Benz</a>
                        if (aElement.TagName.ToLower().Equals("a"))
                        {
                            result = aElement.InnerText;
                            return result;
                        }
                    }
                }

            }

            return result;
        }

        /// <summary>
        /// get the car image path
        /// </summary>
        /// <param name="htmlDocument"></param>
        private string ScrapCarImagePath(HtmlElement panelBodyElement)
        {
            string result = "";

            // check param
            if (panelBodyElement == null)
                return result;
            
            foreach (HtmlElement divElement in panelBodyElement.Children)
            {                
                string strTag = divElement.TagName.ToLower();
                string strVal = divElement.GetAttribute("className");
                
                // <div class="row">
                if (strTag.Equals("div") && strVal.Equals("row") )
                {
                    foreach (HtmlElement aElement in divElement.Children)
                    {
                        // <a href="/am/nomer14071374"> <img src=""> </a>
                        if (aElement.TagName.ToLower().Equals("a"))
                        {
                            foreach (HtmlElement imgElement in aElement.Children)
                            {
                                strTag = imgElement.TagName.ToLower();
                                strVal = imgElement.GetAttribute("className");

                                // <img src="http://img03.platesmania.com/200114/m/14071374.jpg" class="img-responsive center-block" alt="">
                                if (strTag.Equals("img") && strVal.Equals("img-responsive center-block"))
                                {
                                    string imgPath = imgElement.GetAttribute("src");
                                    result = GetLargeImagePath(imgPath);
                                    return result;
                                }
                            }

                        }
                    }
                }

            }            

            return result;
        }

        private string ScrapPlateImagePath(HtmlElement panelBodyElement)
        {
            string result = "";

            // check param
            if (panelBodyElement == null)
                return result;

            foreach (HtmlElement divElement in panelBodyElement.Children)
            {
                string strTag = divElement.TagName.ToLower();
                string strVal = divElement.GetAttribute("className");

                // <div class="row">
                if (strTag.Equals("div") && strVal.Equals("row"))
                {
                    foreach (HtmlElement subDivElement in divElement.Children)
                    {
                        strTag = subDivElement.TagName.ToLower();
                        strVal = subDivElement.GetAttribute("className");
                        
                        // <div class="col-xs-offset-3 col-xs-6 text-center">
                        if (strTag.Equals("div") && strVal.Equals("col-xs-offset-3 col-xs-6 text-center"))
                        {
                            foreach (HtmlElement aElement in subDivElement.Children)
                            {
                                strTag = aElement.TagName.ToLower();

                                // < a href = "/am/nomer14071374" >
                                if (strTag.Equals("a"))
                                {
                                    foreach (HtmlElement imgElement in aElement.Children)
                                    {
                                        strTag = imgElement.TagName.ToLower();
                                        strVal = imgElement.GetAttribute("className");

                                        // <img src="http://img03.platesmania.com/200114/inf/140713745c253a.png" class="img-responsive center-block margin-bottom-10" alt="24 QQ 240">
                                        if (strTag.Equals("img") && strVal.Equals("img-responsive center-block margin-bottom-10"))
                                        {
                                            result = imgElement.GetAttribute("src");                                            
                                            return result;
                                        }
                                    }

                                }
                            }
                        }   // if  subdiv                  
                            
                    }  // foreach
                } // if div

            } // foreach
                        
            return result;
        }

        private string ScrapArea(HtmlElement panelBodyElement)
        {
            string result = "";

            // check param
            if (panelBodyElement == null)
                return result;

            foreach (HtmlElement smallElement in panelBodyElement.Children)
            {
                string strTag = smallElement.TagName.ToLower();
                //string strVal = divElement.GetAttribute("className");

                // <small>
                if (strTag.Equals("small"))
                {
                    foreach (HtmlElement pElement in smallElement.Children)
                    {
                        strTag = pElement.TagName.ToLower();
                        string strVal = pElement.GetAttribute("className");

                        // <p class="text-center">
                        if (strTag.Equals("p") && strVal.Equals("text-center"))
                        {
                            result = pElement.InnerText;
                            return result;
                        }
                    }
                }

            }

            return result;
        }

        private string ScrapUser(HtmlElement panelBodyElement)
        {
            string result = "";

            // check param
            if (panelBodyElement == null)
                return result;

            foreach (HtmlElement pElement in panelBodyElement.Children)
            {
                string strTag = pElement.TagName.ToLower();
                //string strVal = divElement.GetAttribute("className");

                // <p>
                if (strTag.Equals("p"))
                {
                    foreach (HtmlElement aElement in pElement.Children)
                    {
                        strTag = aElement.TagName.ToLower();
                        
                        // <a href="/user6183">
                        if (strTag.Equals("a") )
                        {
                            result = aElement.InnerText;
                            return result;
                        }
                    }
                }

            }

            return result;
        }

        /// <summary>
        /// get the car image path
        /// </summary>
        /// <param name="htmlDocument"></param>
        private string ScrapCarImagePathEx(HtmlDocument htmlDocument)
        {
            string result = "";

            // check param
            if (htmlDocument == null)
                return result;

            // <img src="http://img03.platesmania.com/200114/m/14071374.jpg" class="img-responsive center-block" alt="">
            var imgElements = htmlDocument.GetElementsByTagName("img");
            if (imgElements == null)
                return result;

            foreach (HtmlElement imgElement in imgElements)
            {
                // class="img-responsive center-block"
                string strVal = imgElement.GetAttribute(DEF_CAR_ATTRIBUTE);
                if (strVal.Equals(DEF_CAR_VALUE))
                {
                    // src="http://img03.platesmania.com/200114/m/14071374.jpg"
                    string imgPath = imgElement.GetAttribute("src");
                    result = GetLargeImagePath(imgPath);
                    return result;
                }
            }

            return result;
        }

        private string ScrapPlateImagePathEx(HtmlDocument htmlDocument)
        {
            string result = "";

            // check param
            if (htmlDocument == null)
                return result;

            // <img src="http://img03.platesmania.com/200114/inf/14071129ae7b8e.png" class="img-responsive center-block margin-bottom-10" alt="36 FZ 622" /></a>
            var imgElements = htmlDocument.GetElementsByTagName("img");
            if (imgElements == null)
                return result;

            foreach (HtmlElement imgElement in imgElements)
            {
                // class="img-responsive center-block"
                string strVal = imgElement.GetAttribute("className");
                if (strVal.Equals("img-responsive center-block margin-bottom-10"))
                {
                    // src="http://img03.platesmania.com/200114/inf/14071129ae7b8e.png"
                    result = imgElement.GetAttribute("src");

                    return result;
                }
            }

            return result;
        }

        /// <summary>
        /// convert the small image path into large image path
        /// http://img03.platesmania.com/200114/m/14071374.jpg -> http://img03.platesmania.com/200114/o/14071374.jpg
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        private string GetLargeImagePath(string imagePath)
        {
            string result = "";

            // check param
            if (string.IsNullOrEmpty(imagePath))
            {
                return result;
            }

            // replace /m/ with /o/
            result = imagePath.Replace("/m/", "/o/");

            return result;
        }


    }
}
