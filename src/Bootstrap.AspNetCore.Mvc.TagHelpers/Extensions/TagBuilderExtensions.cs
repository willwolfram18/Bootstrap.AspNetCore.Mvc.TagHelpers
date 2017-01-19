using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers.Extensions
{
    internal static class TagBuilderExtensions
    {
        internal static void AddAttributes(this TagBuilder tag, TagHelperAttributeList attributes)
        {
            foreach (var attribute in attributes)
            {
                if (tag.Attributes.ContainsKey(attribute.Name))
                {
                    tag.Attributes[attribute.Name] = tag.Attributes[attribute.Name] + " " + attribute.Value.ToString();
                }
                else
                {
                    tag.Attributes.Add(attribute.Name, attribute.Value.ToString());
                }
            }
        }

        internal static bool HasCssClass(this TagBuilder tag, string cssClass)
        {
            if (!tag.Attributes.ContainsKey("class"))
            {
                return false;
            }

            return tag.Attributes["class"].Contains(cssClass);
        }
    }
}
