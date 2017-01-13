using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement(Global.TAG_PREFIX + "row")]
    public class Row : BootstrapTagHelperBase
    {
        public override string CssClass
        {
            get
            {
                return "row";
            }
        }

        public override string OutputTag
        {
            get
            {
                return "div";
            }
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();
            output.Content.AppendHtml(content);
            output.TagName = OutputTag;
            AppendDefaultCssClass(output);
        }
    }
}
