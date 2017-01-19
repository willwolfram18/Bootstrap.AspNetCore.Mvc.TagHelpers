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
    public class Breadcrumbs : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string TAG = Global.PREFIX + "breadcrumb";

        public override string CssClass
        {
            get
            {
                return "breadcrumb";
            }
        }

        public override string OutputTag { get; set; } = "ol";
        #endregion
        #endregion

        #region Methods
        #region Public methods
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = OutputTag;
            output.TagMode = TagMode.StartTagAndEndTag;
            var content = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(content);
            AppendDefaultCssClass(output);
        }
        #endregion
        #endregion
    }

    [HtmlTargetElement(TAG, ParentTag = Breadcrumbs.TAG)]
    public class Crumb : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string TAG = Global.PREFIX + "crumb";
        public const string ACTIVE_ATTRIBUTE_NAME = "bs-active";

        [HtmlAttributeName(ACTIVE_ATTRIBUTE_NAME)]
        public bool IsActive { get; set; } = false;

        public override string CssClass
        {
            get
            {
                return (IsActive ? "active" : "");
            }
        }

        [HtmlAttributeNotBound]
        public override string OutputTag { get; set; } = "li";
        #endregion
        #endregion

        #region Methods
        #region Public methods
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = OutputTag;
            output.TagMode = TagMode.StartTagAndEndTag;
            var content = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(content);
            AppendDefaultCssClass(output);
        }
        #endregion
        #endregion
    }
}
