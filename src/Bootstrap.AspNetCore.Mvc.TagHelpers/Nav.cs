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
    public enum NavType
    {
        links,
        tabs,
        pills
    }

    [HtmlTargetElement(TAG)]
    public class Nav : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string TAG = Global.PREFIX + "nav";
        public const string NAV_TYPE_ATTRIBUTE_NAME = "nav-type";
        public const string JUSTIFIED_ATTRIBUTE_NAME = "nav-justified";
        public const string STACKED_ATTRIBUTE_NAME = "nav-stacked";

        [HtmlAttributeName(NAV_TYPE_ATTRIBUTE_NAME)]
        public NavType NavType { get; set; } = NavType.links;

        [HtmlAttributeName(JUSTIFIED_ATTRIBUTE_NAME)]
        public bool IsJustified { get; set; } = false;

        [HtmlAttributeName(STACKED_ATTRIBUTE_NAME)]
        public bool IsStacked { get; set; } = false;

        public override string CssClass
        {
            get
            {
                List<string> cssClasses = new List<string>
                {
                    "nav",
                };
                switch (NavType)
                {
                    case NavType.tabs:
                    case NavType.pills:
                        cssClasses.Add($"nav-{NavType}");
                        break;
                    case NavType.links:
                    default:
                        break;
                }
                if (IsJustified)
                {
                    cssClasses.Add("nav-justified");
                }
                if (IsStacked)
                {
                    cssClasses.Add("nav-stacked");
                }
                return string.Join(" ", cssClasses);
            }
        }

        [HtmlAttributeNotBound]
        public override string OutputTag { get; set; } = "ul";
        #endregion
        #endregion

        #region Methods
        #region Public methods
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);
        }
        #endregion
        #endregion
    }

    [HtmlTargetElement(TAG, ParentTag = Nav.TAG)]
    public class NavItem : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string TAG = Global.PREFIX + "nav-item";
        public const string DISABLED_ATTRIBUTE_NAME = "nav-item-disabled";
        public const string ACTIVE_ATTRIBUTE_NAME = "nav-item-active";

        public override string CssClass
        {
            get
            {
                List<string> cssClasses = new List<string>();
                if (IsActive)
                {
                    cssClasses.Add("active");
                }
                if (IsDisabled)
                {
                    cssClasses.Add("disabled");
                }
                return string.Join(" ", cssClasses);
            }
        }

        [HtmlAttributeNotBound]
        public override string OutputTag { get; set; } = "li";

        [HtmlAttributeName(DISABLED_ATTRIBUTE_NAME)]
        public bool IsDisabled { get; set; } = false;

        [HtmlAttributeName(ACTIVE_ATTRIBUTE_NAME)]
        public bool IsActive { get; set; } = false;
        #endregion
        #endregion

        #region Methods
        #region Public methods
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);
            output.Attributes.SetAttribute("role", "presentation");
        }
        #endregion
        #endregion
    }
}
