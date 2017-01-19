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
    public enum LabelVariation
    {
        Default,
        Primary,
        Success,
        Info,
        Warning,
        Danger
    }

    [HtmlTargetElement(TAG, Attributes = VARIATION_ATTRIBUTE_NAME)]
    public class Label : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string TAG = Global.PREFIX + "label";
        public const string VARIATION_ATTRIBUTE_NAME = "label-variation";

        public override string CssClass
        {
            get
            {
                return $"label label-{LabelVariation.ToString().ToLower()}";
            }
        }

        [HtmlAttributeName(VARIATION_ATTRIBUTE_NAME)]
        public LabelVariation LabelVariation { get; set; }

        public override string OutputTag { get; set; } = "span";
        #endregion
        #endregion

        #region Methods
        #region Public methods
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();
            output.TagName = OutputTag;
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent(content);
            AppendDefaultCssClass(output);
        }
        #endregion
        #endregion
    }
}
