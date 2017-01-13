using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrap.AspNetCore.Mvc.TagHelpers
{
    public interface IBootstrapTagHelper : ITagHelper
    {
        string CssClass { get; }

        string OutputTag { get; }

        void AppendDefaultCssClass(TagHelperOutput output);
    }
}
