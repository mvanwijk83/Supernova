using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Supernova.Core;
using Supernova.Utils.Extensions;

namespace MyWebsite
{
    public partial class Default : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            Response.WriteLine(base.CurrentPage.Title);
        }
    }
}