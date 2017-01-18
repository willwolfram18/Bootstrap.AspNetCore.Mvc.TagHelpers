using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement(TAG)]
    public class Dropup : Dropdown
    {
        public const string TAG = Global.PREFIX + "dropup";

        public override string CssClass
        {
            get
            {
                return "dropup";
            }
        }
    }

    [HtmlTargetElement(TAG, ParentTag = Dropup.TAG)]
    public class DropupItem : DropdownItem
    {
    }
}
