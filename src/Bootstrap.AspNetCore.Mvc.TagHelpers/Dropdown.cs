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
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Bootstrap.AspNetCore.Mvc.TagHelpers.Extensions;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    public class DropdownContext
    {
        public List<IHtmlContent> DropdownItems { get; set; }

        public DropdownContext()
        {
            DropdownItems = new List<IHtmlContent>();
        }
    }

    [HtmlTargetElement(TAG, Attributes = TEXT_ATTRIBUTE_NAME)]
    public class Dropdown : BootstrapTagHelperBase
    {
        #region Properties
        #region Public Properties
        public const string TAG = Global.PREFIX + "dropdown";
        public const string TEXT_ATTRIBUTE_NAME = "dropdown-text";
        public const string RIGHT_ALIGN_ATTRIBUTE_NAME = "dropdown-right-align";
        public const string SPLIT_ATTRIBUTE_NAME = "dropdown-split";

        public override string CssClass
        {
            get
            {
                List<string> cssClasses = new List<string>() { "dropdown" };
                if (IsSplitDropdown)
                {
                    cssClasses.Add("btn-group");
                }
                return string.Join(" ", cssClasses);
            }
        }

        [HtmlAttributeName(TEXT_ATTRIBUTE_NAME)]
        public string DropdownText { get; set; }

        [HtmlAttributeName(RIGHT_ALIGN_ATTRIBUTE_NAME)]
        public bool IsMenuRightAligned { get; set; } = false;

        [HtmlAttributeName(SPLIT_ATTRIBUTE_NAME)]
        public bool IsSplitDropdown { get; set; } = false;
        #endregion

        #region Private Properties
        private readonly DropdownContext _dropdownContext = new DropdownContext();
        #endregion
        #endregion

        #region Methods
        #region Public Methods
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            context.InsertContext(_dropdownContext);
            var childContent = await output.GetChildContentAsync();

            if (context.GetButtonGroupContext() != null)
            {
                AddDropdownToButtnGroupContext(context, output);
                output.SuppressOutput();
            }
            else
            {
                output.TagMode = TagMode.StartTagAndEndTag;
                await base.ProcessAsync(context, output);
                AppendDropdownToggle(output.Content);
                output.Content.AppendHtml(DropdownMenu());
                if (IsSplitDropdown)
                {
                    output.Attributes.Add("role", "group");
                }
            }
        }
        #endregion

        #region Private methods
        private void AppendDropdownToggle(IHtmlContentBuilder parentTag)
        {
            TagBuilder dropdownToggleButton = new TagBuilder("button");
            dropdownToggleButton.AddCssClass("btn btn-default dropdown-toggle");
            dropdownToggleButton.Attributes.Add("type", "button");
            dropdownToggleButton.Attributes.Add("data-toggle", "dropdown");
            dropdownToggleButton.Attributes.Add("aria-haspopup", "true");
            dropdownToggleButton.Attributes.Add("aria-expanded", "true");
            if (IsSplitDropdown)
            {
                TagBuilder textButton = new TagBuilder("button");
                textButton.AddCssClass("btn btn-default");
                textButton.InnerHtml.SetHtmlContent(DropdownText);
                parentTag.AppendHtml(textButton);
                dropdownToggleButton.InnerHtml.SetHtmlContent("<span class='caret'></span><span class='sr-only'>Toggle dropdown</span>");
            }
            else
            {
                dropdownToggleButton.InnerHtml.SetHtmlContent($"{DropdownText} <span class='caret'></span>");
            }

            parentTag.AppendHtml(dropdownToggleButton);
        }

        private IHtmlContent DropdownMenu()
        {
            TagBuilder dropdownMenu = new TagBuilder("ul");
            dropdownMenu.AddCssClass("dropdown-menu");
            if (IsMenuRightAligned)
            {
                dropdownMenu.AddCssClass("dropdown-menu-right");
            }
            foreach (var dropdownItem in _dropdownContext.DropdownItems)
            {
                dropdownMenu.InnerHtml.AppendHtml(dropdownItem);
            }

            return dropdownMenu;
        }

        private void AddDropdownToButtnGroupContext(TagHelperContext context, TagHelperOutput output)
        {
            ButtonGroupContext btnGroupContext = context.GetButtonGroupContext();
            TagBuilder dropdownContainer = new TagBuilder(OutputTag);
            dropdownContainer.AddAttributes(output.Attributes);
            dropdownContainer.AddCssClass(CssClass);
            AppendDropdownToggle(dropdownContainer.InnerHtml);
            dropdownContainer.InnerHtml.AppendHtml(DropdownMenu());
            btnGroupContext.Buttons.Add(dropdownContainer);
        }
        #endregion
        #endregion
    }

    public enum DropdownItemType
    {
        normal,
        header,
        divider,
    }

    [HtmlTargetElement(TAG, ParentTag = Dropdown.TAG)]
    public class DropdownItem : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string TAG = Global.PREFIX + "dropdown-item";
        public const string ACTIVE_ATTRIBUTE_NAME = "dropdown-item-active";
        public const string DISABLED_ATTRIBUTE_NAME = "dropdown-item-disabled";
        public const string TYPE_ATTRIBUTE_NAME = "dropdown-item-type";

        public override string CssClass
        {
            get
            {
                if (ItemType == DropdownItemType.divider)
                {
                    return "divider";
                }
                if (ItemType == DropdownItemType.header)
                {
                    return "dropdown-header";
                }

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

        [HtmlAttributeName(ACTIVE_ATTRIBUTE_NAME)]
        public bool IsActive { get; set; } = false;

        [HtmlAttributeName(DISABLED_ATTRIBUTE_NAME)]
        public bool IsDisabled { get; set; } = false;

        [HtmlAttributeName(TYPE_ATTRIBUTE_NAME)]
        public DropdownItemType ItemType { get; set; } = DropdownItemType.normal;

        [HtmlAttributeNotBound]
        public override string OutputTag { get; set; } = "li";
        #endregion

        #region Private properties
        private TagBuilder _item;
        #endregion
        #endregion

        #region Methods
        #region Public methods
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            DropdownContext dropdownContext = context.GetDropdownContext();

            _item = new TagBuilder(OutputTag);
            AppendAttributes(output.Attributes);
            await SetItemContent(output);
            dropdownContext.DropdownItems.Add(_item);

            output.SuppressOutput();
        }
        #endregion

        #region Private methods
        private void AppendAttributes(TagHelperAttributeList attributes)
        {
            _item.AddCssClass(CssClass);
            _item.AddAttributes(attributes);
        }
        private async Task SetItemContent(TagHelperOutput output)
        {
            if (ItemType == DropdownItemType.divider)
            {
                _item.Attributes.Add("role", "separator");
                _item.InnerHtml.SetHtmlContent("");
            }
            else
            {
                var childContent = await output.GetChildContentAsync();
                _item.InnerHtml.SetHtmlContent(childContent);
            }
        }
        #endregion
        #endregion
    }
}
