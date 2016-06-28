using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Supernova.Core
{
    public class ContainerInitException : Exception
    {
        public ContainerInitException(Exception e) : base("Supernova failed to initialize. Check the inner exception for details.", e)
        { }
    }
}