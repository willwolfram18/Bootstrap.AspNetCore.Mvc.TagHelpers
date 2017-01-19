/*
 * Copyright (c) 2016 Billy Wolfington
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * https://github.com/Bwolfing/Bootstrap.AspNetCore.Mvc.TagHelpers
 *
 */

using Bootstrap.AspNetCore.Mvc.TagHelpers.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    public class ButtonGroupContext
    {
        public List<TagBuilder> Buttons { get; set; }

        public ButtonGroupContext()
        {
            Buttons = new List<TagBuilder>();
        }
    }

    [HtmlTargetElement(TAG)]
    public class ButtonGroup : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string TAG = Global.PREFIX + "btn-group";
        public const string GROUP_SIZE_ATTRIBUTE_NAME = "btn-group-size";
        public const string VERTIAL_ATTRIBUTE_NAME = "btn-group-vertical";

        [HtmlAttributeName(GROUP_SIZE_ATTRIBUTE_NAME)]
        public ButtonSize ButtonGroupSize { get; set; } = ButtonSize.normal;

        [HtmlAttributeName(VERTIAL_ATTRIBUTE_NAME)]
        public bool IsVertical { get; set; } = false;

        public override string CssClass
        {
            get
            {
                List<string> cssClasses = new List<string> { "btn-group" };
                switch (ButtonGroupSize)
                {
                    case ButtonSize.xs:
                    case ButtonSize.sm:
                    case ButtonSize.lg:
                        cssClasses.Add($"btn-group-{ButtonGroupSize}");
                        break;
                    case ButtonSize.normal:
                    default:
                        break;
                }
                if (IsVertical)
                {
                    cssClasses.Add("btn-group-vertical");
                }
                return string.Join(" ", cssClasses);
            }
        }
        #endregion

        #region Private properties
        private readonly ButtonGroupContext _btnGroupContext = new ButtonGroupContext();
        #endregion
        #endregion

        #region Methods
        #region Public methods
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            context.Items.Add(_btnGroupContext.GetType(), _btnGroupContext);
            var content = await output.GetChildContentAsync();
            await SetupButtonGroup(context, output);
            SetHtmlContent(output);
        }


        #endregion

        #region Private methods
        private async Task SetupButtonGroup(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("role", "group");
        }

        private void SetHtmlContent(TagHelperOutput output)
        {
            foreach (var button in _btnGroupContext.Buttons)
            {
                var buttonToAdd = button;
                if (button.HasCssClass("dropdown") || button.HasCssClass("dropup"))
                {
                    TagBuilder nestedGroup = new TagBuilder("div");
                    nestedGroup.AddCssClass(CssClass);
                    if (button.HasCssClass("dropup"))
                    {
                        nestedGroup.AddCssClass("dropup");
                    }
                    nestedGroup.Attributes.Add("role", "group");
                    nestedGroup.InnerHtml.SetHtmlContent(button.InnerHtml);
                    buttonToAdd = nestedGroup;
                }
                output.Content.AppendHtml(buttonToAdd);
            }
        }
        #endregion
        #endregion
    }
}
