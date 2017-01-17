using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    public enum AlertVariation
    {
        Success,
        Info,
        Warning,
        Danger,
    }

    [HtmlTargetElement(Global.PREFIX + "alert", Attributes = ALERT_VARIATION_ATTRIBUTE_NAME)]
    public class Alert : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string ALERT_VARIATION_ATTRIBUTE_NAME = "alert-variation";

        [HtmlAttributeName(ALERT_VARIATION_ATTRIBUTE_NAME)]
        public AlertVariation AlertVariation { get; set; }

        public override string CssClass
        {
            get
            {
                return $"alert alert-{AlertVariation.ToString().ToLower()}";
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
            output.Attributes.SetAttribute("role", "alert");
            await base.ProcessAsync(context, output);
        }
        #endregion
        #endregion
    }
}
