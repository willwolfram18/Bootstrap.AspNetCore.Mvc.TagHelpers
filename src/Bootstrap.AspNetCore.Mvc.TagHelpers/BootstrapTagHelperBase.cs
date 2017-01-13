using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    public abstract class BootstrapTagHelperBase : TagHelper, IBootstrapTagHelper
    {
        #region Properties
        #region Public properties
        public abstract string CssClass { get; }

        [HtmlAttributeName(OUTPUT_TAG_ATTRIBUTE_NAME)]
        public virtual string OutputTag { get; set; } = "div";
        #endregion

        #region Protected properties
        protected const string OUTPUT_TAG_ATTRIBUTE_NAME = Global.TAG_PREFIX + "output-tag";
        #endregion
        #endregion


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
