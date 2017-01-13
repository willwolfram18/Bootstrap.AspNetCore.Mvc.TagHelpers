using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public const string REQUIRED_ATTRIBUTE_NAMES = BUTTON_TYPE_ATTRIBUTE_NAME + "," + OUTPUT_TAG_ATTRIBUTE_NAME;
        public const string BUTTON_TYPE_ATTRIBUTE_NAME = "btn-type";
        public const string BUTTON_SIZE_ATTRIBUTE_NAME = "btn-size";

        [HtmlAttributeName(BUTTON_TYPE_ATTRIBUTE_NAME)]
        public ButtonType ButtonType { get; set; }

        [HtmlAttributeName(BUTTON_SIZE_ATTRIBUTE_NAME)]
        public ButtonSize ButtonSize { get; set; } = ButtonSize.normal;

        public override string CssClass
        {
            get
            {
                string cssClass = $"btn btn-{ButtonType.ToString().ToLower()}";
                switch (ButtonSize)
                {
                    case ButtonSize.xs:
                    case ButtonSize.sm:
                    case ButtonSize.lg:
                        cssClass += $" btn-{ButtonSize}";
                        break;
                    case ButtonSize.normal:
                    default:
                        break;
                }
                return cssClass;
            }
        }
        #endregion
        #endregion

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();
            output.Content.AppendHtml(content);
            output.TagName = OutputTag;
            AppendDefaultCssClass(output);
        }
    }
}
