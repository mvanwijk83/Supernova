using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Supernova.Modules;
using Supernova.Modules.Repositories;

namespace Supernova.Core
{
    public class PageBase : System.Web.UI.Page
    {
        private Container _container;
        private Page _page;

        public Container Container
        {
            get
            {
                if (_container == null)
                    _container = Container.Get();

                return _container;
            }
        }

        public Page CurrentPage
        {
            get
            {
                return _page;
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            PageRepository repo = (PageRepository)Container.GetRepository<Page>();
            string requestPath = "~" + HttpContext.Current.Request.Url.AbsolutePath;
            _page = repo.Item(requestPath);
        }

    }
}