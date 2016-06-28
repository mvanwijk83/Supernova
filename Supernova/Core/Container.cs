using Supernova.Modules;
using Supernova.Modules.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Supernova.Core.Logging;
using System.Reflection;

namespace Supernova.Core
{
    public class Container
    {
        private List<Module> _modules = new List<Module>();
        private bool _isInitialized = false;
        private Dictionary<Type, Module> _mappings = new Dictionary<Type, Module>();
        private static string _instanceKey;
        private List<ILogger> _loggers = new List<ILogger>();
        private AppInfo _appInfo;
        private bool _suppressInitMessages = false;

        public List<Module> Modules
        {
            get
            {
                return _modules;
            }
        }

        public bool IsInitialized
        {
            get
            {
                return _isInitialized;
            }
        }

        public ILogger Logger
        {
            get
            {
                return _loggers.FirstOrDefault();
            }
        }

        public AppInfo AppInfo
        {
            get
            {
                return _appInfo;
            }
        }

        public bool SuppressInitMessages
        {
            get
            {
                return _suppressInitMessages;
            }
        }

        private Container(string key)
        {
            _instanceKey = key;
        }

        public static Container Initialize()
        {
            string key = "SNCNT" + DateTime.Now.Ticks.ToString();
            Container c = new Container(key);
            Exception initEx = null;

            try
            {
                c.ReadConfig();
            }
            catch (Exception e)
            {
                initEx = e;
            }
            

            if (c.IsInitialized)
            {
                HttpContext.Current.Application[key] = c;
                c.WriteStartupMessages();
                return c;
            }
            else
            {
                throw new ContainerInitException(initEx);
            }
        }

        private void ReadConfig()
        {
            _appInfo = new AppInfo(@"C:\dev\Supernova\Supernova\Data\Container.xml");

            GetContainerConfig();
            GetModuleConfig();

            _isInitialized = true;
        }

        private void GetContainerConfig()
        {
            XDocument config = XDocument.Load(@"C:\dev\Supernova\Supernova\Data\Container.xml");
            XElement root = config.Element("Container");
            _suppressInitMessages = bool.Parse(root.Element("SuppressInitMessages").Value);

            XElement loggers = root.Element("Loggers");
            GetLoggers(loggers);
        }

        private void GetLoggers(XElement node)
        {
            foreach (XElement element in node.Elements("Logger"))
            {
                try
                {
                    ObjectHandle handle = Activator.CreateInstance(element.Attribute("Assembly").Value, element.Attribute("Type").Value);
                    if (handle != null)
                    {
                        ILogger logger = (ILogger)handle.Unwrap();
                        _loggers.Add(logger);
                    }
                }
                catch (Exception) { }
            }
        }

        private void GetModuleConfig()
        {
            XDocument config = XDocument.Load(@"C:\dev\Supernova\Supernova\Data\Modules.xml");
            IEnumerable<XElement> elements = config.Descendants("Module");

            foreach (XElement element in elements)
            {
                Module module = new Module(element);
                _modules.Add(module);

                Type type = Type.GetType(module.Type);
                _mappings.Add(type, module);
            }
        }

        private void WriteStartupMessages()
        {
            // emit startup messages to the default logger (if not suppressed in the container config)
            if (this.Logger != null && !this.SuppressInitMessages)
            {
                this.Logger.Write(EventType.Information, "Supernova version " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " by M. van Wijk");
                this.Logger.Write(EventType.Information, "Started " + this.AppInfo.Name);

                if (this.IsInitialized)
                    this.Logger.Write(EventType.Information, "Container initialized successfully");

                this.Logger.Write(EventType.Information, $"Loaded {this.Modules.Count} module(s)");
            }
        }

        public static Container Get()
        {
            object obj = HttpContext.Current.Application[_instanceKey];
            return (Container)obj;
        }

        public Module GetModule<T>() where T : IContent
        {
            if (!_isInitialized)
            {
                return null;
            }
            else
            {
                Type type = typeof(T);
                if (_mappings.ContainsKey(type))
                {
                    return _mappings[type];
                }
                else
                {
                    return null;
                }
            }
        }

        public IRepository<T> GetRepository<T>() where T : IContent
        {
            Module module = GetModule<T>();
            IRepository<T> repo = module.GetRepository<T>();
            return repo;
        }

        /// <summary>
        /// Returns a logger of the specified concrete type.
        /// </summary>
        /// <typeparam name="T">The requested type, implementing the ILogger interface.</typeparam>
        /// <returns>An instance of the specified concrete type, or null if no logger of that type was registered in the container.</returns>
        public ILogger GetLogger<T>() where T : ILogger
        {
            return _loggers.FirstOrDefault(l => l.GetType() == typeof(T));
        }

    }
}