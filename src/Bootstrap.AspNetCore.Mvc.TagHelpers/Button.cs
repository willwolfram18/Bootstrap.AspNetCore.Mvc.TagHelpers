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

    [HtmlTargetElement(Global.TAG_PREFIX + "btn", Attributes = REQUIRED_ATTRIBUTE_NAMES)]
    public class Button : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string REQUIRED_ATTRIBUTE_NAMES = OUTPUT_TAG_ATTRIBUTE_NAME + "," + BUTTON_TYPE_ATTRIBUTE_NAME;
        public const string BUTTON_TYPE_ATTRIBUTE_NAME = "btn-type";
        public const string BUTTON_SIZE_ATTRIBUTE_NAME = "btn-size";
        public const string BUTTON_ACTIVE_ATTRIBUTE_NAME = "btn-active";
        public const string BUTTON_DISABLED_ATTRIBUTE_NAME = "btn-disabled";

        [HtmlAttributeName(BUTTON_TYPE_ATTRIBUTE_NAME)]
        public ButtonType ButtonType { get; set; }

        [HtmlAttributeName(BUTTON_SIZE_ATTRIBUTE_NAME)]
        public ButtonSize ButtonSize { get; set; } = ButtonSize.normal;

        [HtmlAttributeName(BUTTON_ACTIVE_ATTRIBUTE_NAME)]
        public bool IsActive { get; set; } = false;

        [HtmlAttributeName(BUTTON_DISABLED_ATTRIBUTE_NAME)]
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

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();
            output.Content.AppendHtml(content);
            output.TagName = OutputTag;
            if (IsActive)
            {
                output.Attributes.SetAttribute("aria-pressed", "true");
            }
            if (IsDisabled && OutputTag.Trim() == "button")
            {
                output.Attributes.SetAttribute("disabled", "disabled");
            }
            AppendDefaultCssClass(output);
        }
    }
}
