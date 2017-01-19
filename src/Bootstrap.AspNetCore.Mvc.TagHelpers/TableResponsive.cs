/*
 * Copyright (c) 2016 Billy Wolfington
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * https://github.com/Bwolfing/Bootstrap.AspNetCore.Mvc.TagHelpers
 *
 */

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
