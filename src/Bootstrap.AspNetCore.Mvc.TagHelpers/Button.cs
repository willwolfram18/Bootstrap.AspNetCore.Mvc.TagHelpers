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
        ExtraSmall,
        Small,
        Normal,
        Large,
    }

    [HtmlTargetElement(Global.TAG_PREFIX + "btn", Attributes = REQUIRED_ATTRIBUTE_NAMES)]
    public class Button : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string REQUIRED_ATTRIBUTE_NAMES = BUTTON_TYPE_ATTRIBUTE_NAME + "," + BUTTON_OUTPUT_TAG_ATTRIBUTE_NAME;
        public const string BUTTON_TYPE_ATTRIBUTE_NAME = "btn-type";
        public const string BUTTON_OUTPUT_TAG_ATTRIBUTE_NAME = "btn-tag";
        public const string BUTTON_SIZE_ATTRIBUTE_NAME = "btn-size";

        [HtmlAttributeName(BUTTON_TYPE_ATTRIBUTE_NAME)]
        public ButtonType ButtonType { get; set; }

        [HtmlAttributeName(BUTTON_SIZE_ATTRIBUTE_NAME)]
        public ButtonSize ButtonSize { get; set; }

        [HtmlAttributeName(BUTTON_OUTPUT_TAG_ATTRIBUTE_NAME)]
        public string _OutputTag { get; }

        public override string CssClass
        {
            get
            {
                string cssClass = $"btn btn-{ButtonType}";
                switch (ButtonSize)
                {
                    case ButtonSize.ExtraSmall:
                        cssClass += " btn-xs";
                        break;
                    case ButtonSize.Small:
                        cssClass += " btn-sm";
                        break;
                    case ButtonSize.Large:
                        cssClass += " btn-lg";
                        break;
                    case ButtonSize.Normal:
                    default:
                        break;
                }
                return cssClass;
            }
        }

        public override string OutputTag
        {
            get
            {
                return _OutputTag;
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
