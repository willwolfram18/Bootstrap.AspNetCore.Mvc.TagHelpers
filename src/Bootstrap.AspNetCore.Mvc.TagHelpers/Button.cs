using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    public enum ButtonType
    {
        Default,
        Primary,
        Success,
        Info,
        Warning,
        Danger,
        Link,
    }

    public enum ButtonSize
    {
        xs,
        sm,
        normal,
        lg,
    }

    [HtmlTargetElement(TAG, Attributes = REQUIRED_ATTRIBUTE_NAMES)]
    public class Button : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string TAG = Global.PREFIX + "btn";
        public const string REQUIRED_ATTRIBUTE_NAMES = OUTPUT_TAG_ATTRIBUTE_NAME + "," + TYPE_ATTRIBUTE_NAME;
        public const string TYPE_ATTRIBUTE_NAME = "btn-type";
        public const string SIZE_ATTRIBUTE_NAME = "btn-size";
        public const string ACTIVE_ATTRIBUTE_NAME = "btn-active";
        public const string DISABLED_ATTRIBUTE_NAME = "btn-disabled";

        [HtmlAttributeName(TYPE_ATTRIBUTE_NAME)]
        public ButtonType ButtonType { get; set; }

        [HtmlAttributeName(SIZE_ATTRIBUTE_NAME)]
        public ButtonSize ButtonSize { get; set; } = ButtonSize.normal;

        [HtmlAttributeName(ACTIVE_ATTRIBUTE_NAME)]
        public bool IsActive { get; set; } = false;

        [HtmlAttributeName(DISABLED_ATTRIBUTE_NAME)]
        public bool IsDisabled { get; set; } = false;

        public override string CssClass
        {
            get
            {
                StringBuilder cssClass = new StringBuilder($"btn btn-{ButtonType.ToString().ToLower()}");
                switch (ButtonSize)
                {
                    case ButtonSize.xs:
                    case ButtonSize.sm:
                    case ButtonSize.lg:
                        cssClass.Append($" btn-{ButtonSize}");
                        break;
                    case ButtonSize.normal:
                    default:
                        break;
                }

                if (IsActive)
                {
                    cssClass.Append(" active");
                }
                if (IsDisabled)
                {
                    if (OutputTag.Trim() != "button")
                    {
                        cssClass.Append(" disabled");
                    }
                }
                return cssClass.ToString();
            }
        }
        #endregion
        #endregion

        #region Methods
        #region Public methods
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();
            output.Content.AppendHtml(content);
            output.TagName = OutputTag;
            output.TagMode = TagMode.StartTagAndEndTag;
            IncludeExtraAttributes(output);
            AppendDefaultCssClass(output);
        }
        #endregion

        #region Private methods
        private void IncludeExtraAttributes(TagHelperOutput output)
        {
            if (IsActive)
            {
                output.Attributes.SetAttribute("aria-pressed", "true");
            }
            if (IsDisabled && OutputTag.Trim() == "button")
            {
                output.Attributes.SetAttribute("disabled", "disabled");
            }
        }
        #endregion
        #endregion
    }
}
