using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

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

        public override string CssClass
        {
            get
            {
                return "dropdown";
            }
        }

        [HtmlAttributeName(TEXT_ATTRIBUTE_NAME)]
        public string DropdownText { get; set; }

        [HtmlAttributeName(RIGHT_ALIGN_ATTRIBUTE_NAME)]
        public bool IsMenuRightAligned { get; set; } = false;
        #endregion

        #region Private Properties
        private readonly DropdownContext _dropdownContext = new DropdownContext();
        #endregion
        #endregion

        #region Methods
        #region Public Methods
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);
            context.Items.Add(_dropdownContext.GetType(), _dropdownContext);

            var childContent = await output.GetChildContentAsync();
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Content.AppendHtml(DropdownButton());
            output.Content.AppendHtml(DropdownItems());
        }
        #endregion

        #region Private methods
        private IHtmlContent DropdownButton()
        {
            TagBuilder button = new TagBuilder("button");
            button.AddCssClass("btn btn-default dropdown-toggle");
            button.Attributes.Add("type", "button");
            button.Attributes.Add("data-toggle", "dropdown");
            button.Attributes.Add("aria-haspopup", "true");
            button.Attributes.Add("aria-expanded", "true");
            button.InnerHtml.SetHtmlContent($"{DropdownText} <span class='caret'></span>");

            return button;
        }

        private IHtmlContent DropdownItems()
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
            DropdownContext dropdownContext = context.Items[typeof(DropdownContext)] as DropdownContext;

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
            foreach (var attribute in attributes)
            {
                _item.Attributes.Add(attribute.Name, attribute.Value.ToString());
            }
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
