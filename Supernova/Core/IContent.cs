﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supernova.Core
{
    public interface IContent
    {
        object ID { get; set; }
        string Title { get; set; }
        string Url { get; set; }
    }
}
