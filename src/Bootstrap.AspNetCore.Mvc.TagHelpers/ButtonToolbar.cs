using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement(TAG)]
    [RestrictChildren(ButtonGroup.TAG)]
    public class ButtonToolbar : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string TAG = Global.TAG_PREFIX + "btn-toolbar";

        public override string CssClass
        {
            get
            {
                return "btn-toolbar";
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
            output.Attributes.SetAttribute("role", "toolbar");
            await base.ProcessAsync(context, output);
        }
        #endregion
        #endregion
    }
}
