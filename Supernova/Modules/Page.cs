using Supernova.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Supernova.Modules
{
    public class Page : IContent, IEditable
    {
        #region "IContent implementation"
        public object ID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        #endregion

        #region "IEditable implementation"
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }        
        public IUser LastModifiedBy { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        #endregion

        public string Content { get; set; }

    }
}