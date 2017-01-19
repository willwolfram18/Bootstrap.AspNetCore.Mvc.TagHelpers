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

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement(Global.PREFIX + "container-fluid")]
    public class ContainerFluid : Container
    {
        public override string CssClass
        {
            get
            {
                return "container-fluid";
            }
        }
    }
}
