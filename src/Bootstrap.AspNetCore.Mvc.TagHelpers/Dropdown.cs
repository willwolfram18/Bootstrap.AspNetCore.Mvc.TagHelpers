using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
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

        public override string CssClass
        {
            get
            {
                return "dropdown";
            }
        }

        [HtmlAttributeName(TEXT_ATTRIBUTE_NAME)]
        public string DropdownText { get; set; }
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
        #endregion

        #region Methods
        #region Public methods
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            DropdownContext dropdownContext = context.Items[typeof(DropdownContext)] as DropdownContext;
            if (ItemType == DropdownItemType.divider)
            {
                output.Attributes.SetAttribute("role", "separator");
                output.Content.SetHtmlContent("");
            }
            else
            {
                var childContent = await output.GetChildContentAsync();
                output.Content.SetHtmlContent(childContent);
            }
            dropdownContext.DropdownItems.Add(output);
            output.SuppressOutput();
        }
        #endregion
        #endregion
    }
}
