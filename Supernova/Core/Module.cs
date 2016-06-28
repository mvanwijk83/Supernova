using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Supernova.Core
{
    public class Module
    {
        private string _name;
        private string _description;
        private Guid _uid;
        private string _assembly;
        private string _type;
        private string _repositoryType;
        private string _version;
        private string _minAppVersion;
        private string _author;
        private string _website;
        private string _updateUrl;

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
        }

        public Guid UID
        {
            get
            {
                return _uid;
            }
        }

        public string Assembly
        {
            get
            {
                return _assembly;
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }
        }

        public string RepositoryType
        {
            get
            {
                return _repositoryType;
            }
        }

        public string Version
        {
            get
            {
                return _version;
            }
        }

        public string MinAppVersion
        {
            get
            {
                return _minAppVersion;
            }
        }

        public string Author
        {
            get
            {
                return _author;
            }
        }

        public string Website
        {
            get
            {
                return _website;
            }
        }

        public string UpdateUrl
        {
            get
            {
                return _updateUrl;
            }
        }

        public Module(XmlNode node)
        {
            _name = node.SelectSingleNode("//Name").Value;
            _description = node.SelectSingleNode("//Description").Value;
            _uid = new Guid(node.SelectSingleNode("//UID").Value);
            _assembly = node.SelectSingleNode("//Assembly").Value;
            _type = node.SelectSingleNode("//Type").Value;
            _repositoryType = node.SelectSingleNode("//RepositoryType").Value;
            _version = node.SelectSingleNode("//Version").Value;
            _minAppVersion = node.SelectSingleNode("//MinAppVersion").Value;
            _author = node.SelectSingleNode("//Author").Value;
            _website = node.SelectSingleNode("//Website").Value;
            _updateUrl = node.SelectSingleNode("//UpdateUrl").Value;
        }

        public Module(XElement element)
        {
            _name = element.Element("Name").Value;
            _description = element.Element("Description").Value;
            _assembly = element.Element("Assembly").Value;
            _type = element.Element("Type").Value;
            _repositoryType = element.Element("RepositoryType").Value;
            _version = element.Element("Version").Value;
            _minAppVersion = element.Element("MinAppVersion").Value;
            _author = element.Element("Author").Value;
            _website = element.Element("Website").Value;
            _updateUrl = element.Element("UpdateUrl").Value;
        }

        public IRepository<T> GetRepository<T>() where T : IContent
        {
            try
            {
                ObjectHandle handle = Activator.CreateInstance(_assembly, _repositoryType);

                if (handle != null)
                {
                    return (IRepository<T>)handle.Unwrap();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}