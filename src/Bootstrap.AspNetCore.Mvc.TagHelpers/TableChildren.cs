/*
 * Copyright (c) 2016 Billy Wolfington
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * https://github.com/Bwolfing/Bootstrap.AspNetCore.Mvc.TagHelpers
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    public enum TableRowVariation
    {
        Normal,
        Active,
        Success,
        Info,
        Warning,
        Danger,
    }

    [HtmlTargetElement("tr", Attributes = VARIATION_ATTRIBUTE_NAME, ParentTag = Table.TAG)]
    [HtmlTargetElement("th", Attributes = VARIATION_ATTRIBUTE_NAME, ParentTag = "tr")]
    [HtmlTargetElement("td", Attributes = VARIATION_ATTRIBUTE_NAME, ParentTag = "tr")]
    public class TableChildren : BootstrapTagHelperBase
    {
        #region Properties
        #region Public Properties
        public const string VARIATION_ATTRIBUTE_NAME = Global.PREFIX + "variation";

        [HtmlAttributeNotBound]
        public override string CssClass
        {
            get
            {
                switch (RowVariation)
                {
                    case TableRowVariation.Active:
                    case TableRowVariation.Danger:
                    case TableRowVariation.Info:
                    case TableRowVariation.Success:
                    case TableRowVariation.Warning:
                        return $"{RowVariation.ToString().ToLower()}";
                    case TableRowVariation.Normal:
                    default:
                        return "";
                }
            }
        }

        [HtmlAttributeName(VARIATION_ATTRIBUTE_NAME)]
        public TableRowVariation RowVariation { get; set; }

        [HtmlAttributeNotBound]
        public override string OutputTag { get; set; }
        #endregion
        #endregion

        #region Methods
        #region Public Methods
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(content);
            AppendDefaultCssClass(output);
        }
        #endregion
        #endregion
    }
}
