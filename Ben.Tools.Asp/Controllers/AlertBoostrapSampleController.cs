using System.Web.Mvc;
using Ben.Tools.Asp.Controllers;

namespace Ben.Tools.Asp.Samples
{
    public class AlertBoostrapSampleController : AController
    {
        public ActionResult Index()
        {
            var viewModel = new AlertBootstrapSampleViewModel();

            return View(viewModel);
        }
    }

    public class AlertBootstrapSampleViewModel : ADefaultModel
    {

    }
}