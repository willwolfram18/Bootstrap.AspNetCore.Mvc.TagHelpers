using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    public class DropdownContext
    {
        public List<IHtmlContent> DropdownItems { get; set; }

        public DropdownContext()
        {
            DropdownItems = new List<IHtmlContent>();
        }
    }

    [HtmlTargetElement(TAG, Attributes = TEXT_ATTRIBUTE_NAME)]
    public class Dropdown : BootstrapTagHelperBase
    {
        #region Properties
        #region Public Properties
        public const string TAG = Global.PREFIX + "dropdown";
        public const string TEXT_ATTRIBUTE_NAME = "title";

        public override string CssClass
        {
            get
            {
                return "dropdown";
            }
        }

        [HtmlAttributeName(TEXT_ATTRIBUTE_NAME)]
        public string DropdownText { get; set; }
        #endregion

        #region Private Properties
        private readonly DropdownContext _dropdownContext = new DropdownContext();
        #endregion
        #endregion

        #region Methods
        #region Public Methods
        
        #endregion
        #endregion
    }
}
