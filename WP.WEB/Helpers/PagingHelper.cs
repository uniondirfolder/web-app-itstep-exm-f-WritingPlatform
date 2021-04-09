

using System;
using System.Text;
using System.Web.Mvc;
using WP.WEB.Models;

namespace WP.WEB.Helpers
{
    public static class PagingHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfo pg, Func<int,string> pageUrl) 
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i <= pg.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pg.PageNumber) 
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}