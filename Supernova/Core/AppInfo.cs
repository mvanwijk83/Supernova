using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Supernova.Core
{
    public class AppInfo
    {
        private string _name;
        private string _url;
        private string _author;

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Url
        {
            get
            {
                return _url;
            }
        }
        public string Author
        {
            get
            {
                return _author;
            }
        }

        public AppInfo(string xmlPath)
        {
            XDocument config = XDocument.Load(@"C:\dev\Supernova\Supernova\Data\AppInfo.xml");
            XElement root = config.Element("Application");
            _name = root.Element("Name").Value;
            _url = root.Element("Url").Value;
            _author = root.Element("Author").Value;
        }
    }
}