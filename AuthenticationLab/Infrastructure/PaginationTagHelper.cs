using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using AuthenticationLab.Models.ViewModels;

namespace AuthenticationLab.Infrastructure
{
    //PAGINATION WORKS

    [HtmlTargetElement("div", Attributes = "page-blah")]

    public class PaginationTagHelper : TagHelper
    {
        //Dynamically create page links
        private IUrlHelperFactory uhf;

        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }

        // Seperate from ViewContext
        public PageInfo PageBlah { get; set; }

        public string PageAction { get; set; }

        public string PageClass { get; set; }

        public bool PageClassesEnabled { get; set; }

        public string PageClassNormal { get; set; }

        public string PageClassSelected { get; set; }

        public string PageClassLinks { get; set; }

        public override void Process(TagHelperContext thc, TagHelperOutput tho)
        {


            IUrlHelper uh = uhf.GetUrlHelper(vc);



            TagBuilder final = new TagBuilder("div");

            int startPage;
            int endPage;
            if (PageBlah.TotalPages > 1)
            {
                if (PageBlah.TotalPages <= PageBlah.LinksPerPage)
                {
                    startPage = 1;
                    endPage = PageBlah.TotalPages;
                }
                else
                {
                    if (PageBlah.CurrentPage + PageBlah.LinksPerPage - 1 > PageBlah.TotalPages)
                    {
                        startPage = PageBlah.CurrentPage - ((PageBlah.CurrentPage + PageBlah.LinksPerPage - 1)
                            - PageBlah.TotalPages);
                        endPage = (PageBlah.CurrentPage + PageBlah.LinksPerPage - 1) -
                            ((PageBlah.CurrentPage + PageBlah.LinksPerPage - 1) - PageBlah.TotalPages);
                    }
                    else
                    {
                        if (PageBlah.LinksPerPage != 2)
                        {
                            startPage = PageBlah.CurrentPage - (PageBlah.LinksPerPage / 2);
                            if (startPage < 1)
                            {
                                startPage = 1;
                            }
                            endPage = startPage + PageBlah.LinksPerPage - 1;
                        }
                        else
                        {
                            startPage = PageBlah.CurrentPage;
                            endPage = PageBlah.CurrentPage + PageBlah.LinksPerPage - 1;
                        }

                    }

                }
                TagBuilder labelDiv;
                labelDiv = new TagBuilder("div");
                labelDiv.AddCssClass(PageClass);
                labelDiv.InnerHtml.Append($"Showing {PageBlah.CurrentPage} of { PageBlah.TotalPages}");
                final.InnerHtml.AppendHtml(labelDiv);
                TagBuilder linkDiv = new TagBuilder("div");
                linkDiv.InnerHtml.AppendHtml(GeneratePageLinks("First", 1));
                for (int i = startPage; i <= endPage; i++)
                {
                    linkDiv.InnerHtml.AppendHtml(GeneratePageLinks(i.ToString(), i));
                }

                linkDiv.InnerHtml.AppendHtml(GeneratePageLinks("Last", PageBlah.TotalPages));
                linkDiv.AddCssClass(PageClassLinks);
                final.InnerHtml.AppendHtml(linkDiv);
                tho.Content.AppendHtml(final.InnerHtml);
            }
        }

        private TagBuilder GeneratePageLinks(string iterator, int pageNum)
        {



            string url;
            TagBuilder tag;
            tag = new TagBuilder("a");
            url = PageBlah.UrlParams.Replace("-", pageNum.ToString());
            tag.Attributes["href"] = url;

            tag.AddCssClass(PageClass);
            if (iterator != "First" && iterator != "Last")
            {
                tag.AddCssClass(Convert.ToInt32(iterator) == PageBlah.CurrentPage ? PageClassSelected : PageClassNormal);
            }
            else
            {
                tag.AddCssClass(pageNum == PageBlah.CurrentPage ? PageClassSelected : PageClassNormal);
            }
            tag.InnerHtml.Append(iterator.ToString());
            return tag;

        }




    }
}
