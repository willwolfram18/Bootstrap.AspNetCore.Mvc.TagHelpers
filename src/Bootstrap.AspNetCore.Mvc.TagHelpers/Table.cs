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
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{

    [HtmlTargetElement(TAG)]
    public class Table : BootstrapTagHelperBase
    {
        #region Properties
        #region Public Properties
        public const string TAG = Global.PREFIX + "table";
        public const string HOVER_ATTRIBUTE_NAME = "table-hover";
        public const string STIPED_ATTRIBUTE_NAME = "table-striped";
        public const string BORDERED_ATTRIBUTE_NAME = "table-bordered";
        public const string CONDENSED_ATTRIBUTE_NAME = "table-condensed";

        public override string CssClass
        {
            get
            {
                List<string> cssClasses = new List<string>() { "table" };
                if (IsHoverTable)
                {
                    cssClasses.Add("table-hover");
                }
                if (IsStriped)
                {
                    cssClasses.Add("table-striped");
                }
                if (IsBordered)
                {
                    cssClasses.Add("table-bordered");
                }
                if (IsCondensed)
                {
                    cssClasses.Add("table-condensed");
                }
                return string.Join(" ", cssClasses);
            }
        }

        [HtmlAttributeName(HOVER_ATTRIBUTE_NAME)]
        public bool IsHoverTable { get; set; } = false;

        [HtmlAttributeName(STIPED_ATTRIBUTE_NAME)]
        public bool IsStriped { get; set; } = false;

        [HtmlAttributeName(BORDERED_ATTRIBUTE_NAME)]
        public bool IsBordered { get; set; } = false;

        [HtmlAttributeName(CONDENSED_ATTRIBUTE_NAME)]
        public bool IsCondensed { get; set; } = false;

        [HtmlAttributeNotBound]
        public override string OutputTag {get; set;} = "table";
        #endregion
        #endregion

        #region Methods
        #region Public Methods
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            output.TagMode = TagMode.StartTagAndEndTag;
            await base.ProcessAsync(context, output);
        }
        #endregion
        #endregion
    }
}