using MS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MS.Web.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            var result = new StringBuilder();

            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                var linkElement = new TagBuilder("a");
                linkElement.MergeAttribute("href", pageUrl(i));
                linkElement.InnerHtml = $"{i}";

                if (i == pagingInfo.CurrentPage)
                {
                    linkElement.AddCssClass("selected");
                    linkElement.AddCssClass("btn-primary");
                }

                linkElement.AddCssClass("btn btn-default");

                result.Append(linkElement.ToString());
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}