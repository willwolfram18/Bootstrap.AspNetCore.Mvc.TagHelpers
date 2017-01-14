using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement(TAG)]
    [RestrictChildren(Button.TAG)]
    public class ButtonGroup : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string TAG = Global.TAG_PREFIX + "btn-group";
        public const string GROUP_SIZE_ATTRIBUTE_NAME = "btn-group-size";

        [HtmlAttributeName(GROUP_SIZE_ATTRIBUTE_NAME)]
        public ButtonSize ButtonGroupSize { get; set; } = ButtonSize.normal;

        public override string CssClass
        {
            get
            {
                StringBuilder cssClass = new StringBuilder("btn-group");
                switch (ButtonGroupSize)
                {
                    case ButtonSize.xs:
                    case ButtonSize.sm:
                    case ButtonSize.lg:
                        cssClass.Append($" btn-group-{ButtonGroupSize}");
                        break;
                    case ButtonSize.normal:
                    default:
                        break;
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
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("role", "group");
            await base.ProcessAsync(context, output);
        }
        #endregion
        #endregion
    }
}
