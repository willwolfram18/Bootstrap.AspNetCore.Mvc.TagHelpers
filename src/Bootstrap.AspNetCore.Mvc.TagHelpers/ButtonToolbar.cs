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
    public class ButtonToolbar : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string TAG = Global.PREFIX + "btn-toolbar";

        public override string CssClass
        {
            get
            {
                return "btn-toolbar";
            }
        }
        #endregion
        #endregion

        #region Methods
        #region Public methods
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();
            output.Content.AppendHtml(content);
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("role", "toolbar");
            await base.ProcessAsync(context, output);
        }
        #endregion
        #endregion
    }
}
