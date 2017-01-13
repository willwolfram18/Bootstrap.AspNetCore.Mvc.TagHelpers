using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement(Global.TAG_PREFIX + "col")]
    public class Col : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        [HtmlAttributeName(COLUMN_WIDTH_ATTRIBUTE_PREFIX + "xs")]
        public int? ExtraSmallDeviceWidth { get; set; }

        [HtmlAttributeName(COLUMN_WIDTH_ATTRIBUTE_PREFIX + "sm")]
        public int? SmallDeviceWidth { get; set; }

        [HtmlAttributeName(COLUMN_WIDTH_ATTRIBUTE_PREFIX + "md")]
        public int? MediumDeviceWidth { get; set; }

        [HtmlAttributeName(COLUMN_WIDTH_ATTRIBUTE_PREFIX + "lg")]
        public int? LargeDeviceWidth { get; set; }

        public override string CssClass
        {
            get
            {
                List<string> cssClasses = new List<string>();
                AppendColumnWidth(cssClasses, "xs", ExtraSmallDeviceWidth);
                AppendColumnWidth(cssClasses, "sm", SmallDeviceWidth);
                AppendColumnWidth(cssClasses, "md", MediumDeviceWidth);
                AppendColumnWidth(cssClasses, "lg", LargeDeviceWidth);
                return string.Join(" ", cssClasses);
            }
        }
        #endregion

        #region Private properties
        private const string COLUMN_WIDTH_ATTRIBUTE_PREFIX = "col-";
        private const string COLUMN_CSS_CLASS_PATTERN = @"col-(xs|sm|md|lg)-\d{1,2}";
        #endregion
        #endregion

        #region Methods
        #region Public methods
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();
            output.Content.AppendHtml(content);
            output.TagName = OutputTag;
            RemoveExistingColumnWidths(output);
            AppendDefaultCssClass(output);
        }
        #endregion

        #region Private methods
        private void AppendColumnWidth(List<string> columnWidths, string columnSize, int? width)
        {
            if (width != null && IsWidthInRange(width.Value))
            {
                columnWidths.Add($"col-{columnSize}-{width.Value}");
            }
        }
        private bool IsWidthInRange(int width)
        {
            return Global.MIN_COLUMN_WIDTH <= width && width <= Global.MAX_COLUMN_WIDTH;
        }

        private void RemoveExistingColumnWidths(TagHelperOutput output)
        {
            if (output.Attributes.ContainsName("class"))
            {
                string cssClass = output.Attributes["class"].Value.ToString();
                cssClass = Regex.Replace(cssClass, COLUMN_CSS_CLASS_PATTERN, "", RegexOptions.IgnoreCase);
                output.Attributes.SetAttribute("class", cssClass);
            }
        }
        #endregion
        #endregion
    }
}
