/*
 * Copyright (c) 2016 Billy Wolfington
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * https://github.com/Bwolfing/Bootstrap.AspNetCore.Mvc.TagHelpers
 *
 */

using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    public enum ModalSize
    {
        sm,
        normal,
        lg,
    }

    [HtmlTargetElement(TAG)]
    public class Modal : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string TAG = Global.PREFIX + "modal";
        public const string TITLE_ATTRIBUTE_NAME = "modal-title";
        public const string SIZE_ATTRIBUTE_NAME = "modal-size";
        public const string ANIMATION_ATTRIBUTE_NAME = "modal-animation";

        public override string CssClass
        {
            get
            {
                List<string> cssClasses = new List<string> { "modal" };
                if (Animation)
                {
                    cssClasses.Add("fade");
                }
                return string.Join(" ", cssClasses);
            }
        }

        [HtmlAttributeName(TITLE_ATTRIBUTE_NAME)]
        public string Title { get; set; }

        [HtmlAttributeName(SIZE_ATTRIBUTE_NAME)]
        public ModalSize Size { get; set; } = ModalSize.normal;

        [HtmlAttributeName(ANIMATION_ATTRIBUTE_NAME)]
        public bool Animation { get; set; } = true;
        #endregion
        #endregion

        #region Methods
        #region Public methods
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("role", "dialog");
            output.Content.AppendHtml(string.Format("<div class='{0}' role='document'>", ModalDialogClass()));
            await AppendModalContent(context, output);
            output.Content.AppendHtml("</div>"); // close modal-dialog
        }
        #endregion

        #region Private properties
        private string ModalDialogClass()
        {
            List<string> cssClasses = new List<string> { "modal-dialog" };
            switch (Size)
            {
                case ModalSize.sm:
                case ModalSize.lg:
                    cssClasses.Add($"modal-{Size}");
                    break;
                case ModalSize.normal:
                default:
                    break;
            }
            return string.Join(" ", cssClasses);
        }

        private async Task AppendModalContent(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = output.GetChildContentAsync();
            output.Content.AppendHtml("<div class='modal-content'>");
            AppendModalHeader(output);
            output.Content.AppendHtml(await childContent);
            output.Content.AppendHtml("</div>"); // close modal-content
        }
        private void AppendModalHeader(TagHelperOutput output)
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                return;
            }

            output.Content.AppendHtml("<div class='modal-header'>");
            output.Content.AppendHtml(Global.CloseButton("modal"));
            output.Content.AppendHtml(string.Format(
                "<h4 class='modal-title'>{0}</h4>",
                Title
            ));
            output.Content.AppendHtml("</div>");
        }
        #endregion
        #endregion
    }

    [HtmlTargetElement(TAG, ParentTag = Modal.TAG)]
    public class ModalBody : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string TAG = Global.PREFIX + "modal-body";

        public override string CssClass
        {
            get
            {
                return "modal-body";
            }
        }
        #endregion
        #endregion
    }

    [HtmlTargetElement(TAG, ParentTag = Modal.TAG)]
    public class ModalFooter : BootstrapTagHelperBase
    {
        #region Properties
        #region Public properties
        public const string TAG = Global.PREFIX + "modal-footer";

        public override string CssClass
        {
            get
            {
                return "modal-footer";
            }
        }
        #endregion
        #endregion
    }
}
