using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement(TAG)]
    public class Jumbotron : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string TAG = Global.TAG_PREFIX + "jumbotron";

        public override string CssClass
        {
            get
            {
                return "jumbotron";
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
            await base.ProcessAsync(context, output);
        }
        #endregion
        #endregion
    }
}
