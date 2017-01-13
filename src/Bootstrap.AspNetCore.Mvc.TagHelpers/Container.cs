using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement(Global.TAG_PREFIX + "container")]
    public class Container : BootstrapTagHelperBase
    {
        public override string CssClass
        {
            get
            {
                return "container";
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
