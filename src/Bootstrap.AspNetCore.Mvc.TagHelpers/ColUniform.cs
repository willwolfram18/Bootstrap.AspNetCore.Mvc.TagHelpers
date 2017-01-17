using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement(Global.PREFIX + "col-uniform", Attributes = COLUMN_WIDTH_ATTRIBUTE)]
    public class ColUniform : BootstrapTagHelperBase
    {
        #region Properties
        #region Public Properties
        public const string COLUMN_WIDTH_ATTRIBUTE = "col-width";

        [HtmlAttributeName(COLUMN_WIDTH_ATTRIBUTE)]
        public int ColumnWidth { get; set; }

        public override string CssClass
        {
            get
            {
                if (!IsWidthInRange())
                {
                    return "";
                }
                return $"col-xs-{ColumnWidth} col-sm-{ColumnWidth} col-md-{ColumnWidth} col-lg-{ColumnWidth}";
            }
        }
        #endregion

        #region Private properties
        private const string COLUMN_CSS_CLASS_PATTERN = @"col-(xs|sm|md|lg)-\d{1,2}";
        #endregion
        #endregion

        #region Methods
        #region Public methods
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();
            output.Content.AppendHtml(content);
            output.TagMode = TagMode.StartTagAndEndTag;
            RemoveExistingColumnWidths(output);
            await base.ProcessAsync(context, output);
        }
        #endregion

        #region Private methods
        private bool IsWidthInRange()
        {
            return Global.MIN_COLUMN_WIDTH <= ColumnWidth && ColumnWidth <= Global.MAX_COLUMN_WIDTH;
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
