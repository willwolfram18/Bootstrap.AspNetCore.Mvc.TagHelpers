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
using System.Threading.Tasks;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement(Global.PREFIX + "alert-dismissble")]
    public class AlertDismissible : Alert
    {
        public override string CssClass
        {
            get
            {
                return base.CssClass + " alert-dismissible";
            }
        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.AppendHtml(Global.CloseButton("alert"));
            return base.ProcessAsync(context, output);
        }
    }
}
