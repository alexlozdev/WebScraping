using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarScraping
{
    class CountryInfo
    {
        /// <summary>
        /// Country Name
        /// </summary>
        private string _name = "";
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        /// <summary>
        /// Country Acronym
        /// </summary>
        private string _acronym = "";
        public string Acronym
        {
            get => _acronym;
            set => _acronym = value;
        }

        /// <summary>
        /// Constructor 1
        /// </summary>
        public CountryInfo()
        {

        }

        /// <summary>
        /// Constructor 2
        /// </summary>
        /// <param name="name"></param>
        /// <param name="acronym"></param>
        public CountryInfo(string name, string acronym)
        {
            _name = name;
            _acronym = acronym;
        }
    
    }
}
