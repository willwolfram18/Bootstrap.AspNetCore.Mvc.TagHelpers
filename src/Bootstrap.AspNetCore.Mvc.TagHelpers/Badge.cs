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
    [HtmlTargetElement(TAG, Attributes = VALUE_ATTRIBUTE_NAME, TagStructure = TagStructure.WithoutEndTag)]
    public class Badge : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string TAG = Global.PREFIX + "badge";
        public const string VALUE_ATTRIBUTE_NAME = "badge-value";

        public override string CssClass
        {
            get
            {
                return "badge";
            }
        }

        [HtmlAttributeName(VALUE_ATTRIBUTE_NAME)]
        public string Value { get; set; }

        public override string OutputTag { get; set; } = "span";
        #endregion
        #endregion

        #region Methods
        #region Public methods
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = OutputTag;
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetContent(Value);
            AppendDefaultCssClass(output);
            return Task.CompletedTask;
        }
        #endregion
        #endregion
    }
}
