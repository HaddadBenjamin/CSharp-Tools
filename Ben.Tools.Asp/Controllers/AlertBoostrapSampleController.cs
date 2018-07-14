using System.Web.Mvc;

namespace Ben.Tools.Asp.Samples
{
    public class AlertBoostrapSampleController : Controller
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