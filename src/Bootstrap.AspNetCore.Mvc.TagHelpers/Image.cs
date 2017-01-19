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
    public enum ImageShape
    {
        Normal,
        Rounded,
        Circle,
        Thumbnail,
    }

    [HtmlTargetElement("img", Attributes = RESPONSIVE_ATTRIBUTE_NAME)]
    public class Image : BootstrapTagHelperBase
    {
        #region Properties
        #region Public Properties
        public const string RESPONSIVE_ATTRIBUTE_NAME = Global.PREFIX + "responsive";
        public const string SHAPE_ATRRIBUTE_NAME = Global.PREFIX + "shape";

        public override string CssClass
        {
            get
            {
                List<string> cssClasses = new List<string>();
                if (IsResponsive)
                {
                    cssClasses.Add("img-responsive");
                }

                switch (ImageShape)
                {
                    case ImageShape.Circle:
                    case ImageShape.Rounded:
                    case ImageShape.Thumbnail:
                        cssClasses.Add($"img-{ImageShape.ToString().ToLower()}");
                        break;
                    case ImageShape.Normal:
                    default:
                        break;
                }
                return string.Join(" ", cssClasses);
            }
        }

        [HtmlAttributeName(RESPONSIVE_ATTRIBUTE_NAME)]
        public bool IsResponsive { get; set; }

        [HtmlAttributeName(SHAPE_ATRRIBUTE_NAME)]
        public ImageShape ImageShape { get; set; }
        #endregion
        #endregion

        #region Methods
        #region Public Methods
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            AppendDefaultCssClass(output);
            return Task.CompletedTask;
        }
        #endregion
        #endregion
    }
}
