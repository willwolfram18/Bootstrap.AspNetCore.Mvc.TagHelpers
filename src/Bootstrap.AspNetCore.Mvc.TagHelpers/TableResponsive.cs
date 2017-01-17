using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement(TAG)]
    public class TableResponsive : BootstrapTagHelperBase
    {
        #region Properties
        #region Public Properties
        public const string TAG = Global.PREFIX + "table-responsive";

        public override string CssClass
        {
            get
            {
                return "table-responsive";
            }
        }
        #endregion
        #endregion
    }
}
