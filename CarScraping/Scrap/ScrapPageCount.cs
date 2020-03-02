using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CarScraping
{
    class ScrapPageCount
    {
        /// <summary>
        /// get page count from html page
        /// </summary>
        /// <param name="htmlDocument"></param>
        /// <returns></returns>
        public static int GetPageCount(HtmlDocument htmlDocument)
        {
            int result = 0;

            // check param
            if (htmlDocument == null)
                return result;

            // <div class="col-xs-12">
            var htmlElements = htmlDocument.GetElementsByTagName("div");
            if (htmlElements == null)
                return result;
            
            foreach (HtmlElement divElement in htmlElements)
            {
                string strClass = divElement.GetAttribute("className");
                if (strClass.Equals("col-xs-12"))
                {
                    // <h1 class="pull-left">
                    foreach (HtmlElement hElement in divElement.Children)
                    {
                        // <b> 23.38 </b>
                        foreach (HtmlElement bElement in hElement.Children)
                        {
                            if (bElement.TagName.ToLower().Equals("b"))
                            {
                                string strVal = bElement.InnerText;
                                // remove point from string
                                strVal = strVal.Replace(".", "");

                                result = ConvertInt32(strVal);
                                return result;
                            }
                        }

                            
                    }
                }
            }

            return result;
        }

        private static int ConvertInt32(string paramVal)
        {
            int result = 0;

            // check param
            if (string.IsNullOrEmpty(paramVal))
                return result;
            
            try
            {
                result = Int32.Parse(paramVal);
            } catch (FormatException)
            {
            }

            return result;
        }
    }
}
