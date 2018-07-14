using System.Text;
using System.Web.Mvc;

namespace Ben.Tools.Asp.Extensions
{
    /// <summary>
    /// Les extensions HtmlHelper sont appelables depuis votre razor et permet de structurer votre code razor réutilisable.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString AlertBootstrap(this HtmlHelper html, string message, AlertType alertType)
        {
            if (string.IsNullOrWhiteSpace(message))
                return MvcHtmlString.Empty;

            var stringBuilder = new StringBuilder();

            stringBuilder.Append("<div data-alert class='alert-box" + alertType.ToString().ToLower() + "'>");
            stringBuilder.Append(message);
            stringBuilder.Append("<a href='#' class='close'>&times;</a>");
            stringBuilder.Append("</div>");

            return new MvcHtmlString(stringBuilder.ToString());
        }

    }
}
