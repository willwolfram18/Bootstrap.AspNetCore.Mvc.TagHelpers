using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    public enum AlertType
    {
        Success,
        Info,
        Warning,
        Danger,
    }

    [HtmlTargetElement(Global.TAG_PREFIX + "alert", Attributes = ALERT_TYPE_ATTRIBUTE_NAME)]
    public class Alert : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string ALERT_TYPE_ATTRIBUTE_NAME = "alert-type";

        [HtmlAttributeName(ALERT_TYPE_ATTRIBUTE_NAME)]
        public AlertType AlertType { get; set; }

        public override string CssClass
        {
            get
            {
                return $"alert alert-{AlertType.ToString().ToLower()}";
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
            output.TagName = OutputTag;
            output.Attributes.SetAttribute("role", "alert");
            AppendDefaultCssClass(output);
        }
        #endregion
        #endregion
    }
}
