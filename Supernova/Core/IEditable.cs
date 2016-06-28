using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supernova.Core
{
    public interface IEditable
    {
        DateTime Created { get; set; }
        DateTime LastModified { get; set; }
        IUser LastModifiedBy { get; set; }
        DateTime PublicationDate { get; set; }
        DateTime ExpirationDate { get; set; }
        bool IsActive { get; set; }
    }
}
