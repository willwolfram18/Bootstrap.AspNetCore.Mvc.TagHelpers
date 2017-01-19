/*
 * Copyright (c) 2016 Billy Wolfington
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * https://github.com/Bwolfing/Bootstrap.AspNetCore.Mvc.TagHelpers
 *
 */

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
