﻿using Microsoft.AspNetCore.Razor.TagHelpers;
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
        public const string TAG = Global.TAG_PREFIX + "badge";
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
