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
    [HtmlTargetElement(TAG, Attributes = TEXT_ATTRIBUTE_NAME, TagStructure = TagStructure.WithoutEndTag)]
    public class PageHeader : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string TAG = Global.PREFIX + "page-header";
        public const string TEXT_ATTRIBUTE_NAME = "page-header-text";
        public const string SUBTEXT_ATTRIBUTE_NAME = "page-header-subtext";

        public override string CssClass
        {
            get
            {
                return "page-header";
            }
        }

        [HtmlAttributeName(TEXT_ATTRIBUTE_NAME)]
        public string HeaderText { get; set; }

        [HtmlAttributeName(SUBTEXT_ATTRIBUTE_NAME)]
        public string Subtext { get; set; }
        #endregion
        #endregion

        #region Methods
        #region Public methods
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            string subtextTemplate = GetHeaderSubtext();
            string headerTextTemplate = $"<h1>{HeaderText}{subtextTemplate}</h1>";
            output.Content.SetHtmlContent(headerTextTemplate);
            output.TagMode = TagMode.StartTagAndEndTag;
            return base.ProcessAsync(context, output);
        }


        #endregion

        #region Private methods
        private string GetHeaderSubtext()
        {
            if (!string.IsNullOrWhiteSpace(Subtext))
            {
                return $" <small>{Subtext}</small";
            }

            return "";
        }
        #endregion
        #endregion
    }
}
