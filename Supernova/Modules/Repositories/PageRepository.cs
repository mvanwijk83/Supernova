using Supernova.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Supernova.Modules.Repositories
{
    public class PageRepository : IRepository<Page>
    {
        #region "Interface implementation"
        public Page Item(object id)
        {
            return All().FirstOrDefault(item => item.ID == id);
        }

        public List<Page> List(out int total, int pageNumber, int pageSize)
        {
            List<Page> items = All();
            total = items.Count();

            if (pageNumber == 0) pageNumber = 1;

            List<Page> paged = items.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return paged;
        }
        #endregion

        public Page Item(string url)
        {
            url = url.ToLower();
            return All().FirstOrDefault(item => item.Url.ToLower() == url);
        }

        public List<Page> All()
        {
            XDocument config = XDocument.Load(@"C:\dev\Supernova\Supernova\Data\Pages.xml");
            IEnumerable<XElement> elements = config.Descendants("Page");
            List<Page> pages = new List<Page>();

            foreach (XElement element in elements)
            {
                Page page = new Page();
                page.ID = element.Element("ID").Value;
                page.Title = element.Element("Title").Value;
                page.Url = element.Element("Url").Value;
                pages.Add(page);
            }

            return pages;
        }
    }
}