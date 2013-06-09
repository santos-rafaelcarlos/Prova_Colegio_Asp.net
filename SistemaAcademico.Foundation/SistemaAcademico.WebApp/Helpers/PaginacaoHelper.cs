using System;
using System.Text;
using System.Web.Mvc;
using Presentation.WebApp.Models.Comum;

namespace Presentation.WebApp.Helpers
{
    public static class PaginacaoHelper
    {
        public static MvcHtmlString PaginasLinks(this HtmlHelper htmlHelper, InformacaoDePaginacao informacaoDePaginacao,
                                              Func<int, string> url)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 1; i <= informacaoDePaginacao.TotalDePaginas; i++)
            {
                TagBuilder tagBuilder = new TagBuilder("a");
                tagBuilder.MergeAttribute("href", url(i));
                tagBuilder.InnerHtml = string.Format(" {0} ", i);

                if (i == informacaoDePaginacao.PaginaAtual)
                    tagBuilder.AddCssClass("selected");
                builder.AppendFormat("{0} ", tagBuilder.ToString());
            }

            return MvcHtmlString.Create(builder.ToString());
        }
    }
}