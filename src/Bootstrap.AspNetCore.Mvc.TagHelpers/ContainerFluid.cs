using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement(Global.PREFIX + "container-fluid")]
    public class ContainerFluid : Container
    {
        public override string CssClass
        {
            get
            {
                return "container-fluid";
            }
        }
    }
}
