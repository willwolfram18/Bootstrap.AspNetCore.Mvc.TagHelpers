using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    public abstract class BootstrapTagHelperBase : TagHelper, IBootstrapTagHelper
    {
        public abstract string CssClass { get; }

        public abstract string OutputTag { get; }

        public void AppendDefaultCssClass(TagHelperOutput output)
        {
            string cssClass = CssClass;
            if (output.Attributes.ContainsName("class"))
            {
                cssClass += " " + output.Attributes["class"].Value.ToString();
            }
            output.Attributes.SetAttribute("class", cssClass);
        }
    }
}
