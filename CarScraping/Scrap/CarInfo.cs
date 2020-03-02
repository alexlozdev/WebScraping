using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CarScraping
{
    class CarInfo
    {
        /// <summary>
        /// car name
        /// </summary>
        private string _name = "";
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        /// <summary>
        /// car image path
        /// </summary>
        private string _imageCarPath = "";
        public string ImageCarPath
        {
            get => _imageCarPath;
            set => _imageCarPath = value;
        }

        /// <summary>
        /// car number plate image path
        /// </summary>
        private string _imagePlatePath = "";
        public string ImagePlatePath
        {
            get => _imagePlatePath;
            set => _imagePlatePath = value;
        }

        private string _area = "";
        public string Area
        {
            get => _area;
            set => _area = value;
        }

        private string _user = "";
        public string User
        {
            get => _user;
            set => _user = value;
        }

        /// <summary>
        /// constructor 1
        /// </summary>
        public CarInfo()
        {

        }

        /// <summary>
        /// Constructor2
        /// </summary>
        /// <param name="name"></param>
        /// <param name="imageCarPath"></param>
        /// <param name="imagePlatePath"></param>
        public CarInfo(string name, string imageCarPath, string imagePlatePath, string user)
        {
            _name = name;
            _imageCarPath = imageCarPath;
            _imagePlatePath = imagePlatePath;
            _user = user;
        }


    }
}
