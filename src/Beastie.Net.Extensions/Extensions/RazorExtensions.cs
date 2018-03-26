using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Beastie.Net.Extensions.Controllers;

namespace Beastie.Net.Extensions.Extensions
{
    public static class RazorExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The render view to string.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="viewPath">
        /// The view path.
        /// </param>
        /// <param name="escapeHtml">
        /// The escape Html.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string RenderViewToString(this object model, string viewPath, bool escapeHtml = true)
        {
            var ctx = new HttpContextWrapper(HttpContext.Current);
            var routeData = new RouteData();
            var controllerContext = new ControllerContext(new RequestContext(ctx, routeData), new EmptyController());

            return RenderViewToString(model, viewPath, controllerContext, escapeHtml);
        }

        /// <summary>
        /// The render view to string.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="viewPath">
        /// The view path.
        /// </param>
        /// <param name="controllerContext">
        /// The controller Context.
        /// </param>
        /// <param name="escapeHtml">
        /// The escape Html.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string RenderViewToString(
            this object model,
            string viewPath,
            ControllerContext controllerContext,
            bool escapeHtml = true)
        {
            using (var st = new StringWriter())
            {
                var razor = new RazorView(controllerContext, viewPath, null, false, null);

                var viewContext = new ViewContext(
                    controllerContext,
                    razor,
                    new ViewDataDictionary(model),
                    new TempDataDictionary(),
                    st);

                razor.Render(viewContext, st);

                return escapeHtml ? st.ToString() : st.ToString().HtmlDecode();
            }
        }

        #endregion
    }
}