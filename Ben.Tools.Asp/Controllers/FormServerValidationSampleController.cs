using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Ben.Tools.Asp.Controllers;
using Ben.Tools.Asp.Extensions;
using Ben.Tools.Asp.Helpers;

namespace Ben.Tools.Asp.Samples
{
    public class FormServerValidationSampleController : AController
    {
        [HttpGet]
        public ActionResult Index()
        {
            // Petit plus avoir 2 viewModels :
            // - Un reçu par votre formulaire.
            // - Un envoyé par votre formulaire.
            var viewModel = new FormServerValidationSampleViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SendForm(FormServerValidationSampleViewModel viewModelEdited)
        {
            if (!ModelState.IsValid)
                return ErrorPage(ValidatorHelper.GetValidationMessage(viewModelEdited));

            // continue your life.
        }

        [HttpGet]
        public ActionResult ErrorPage(string message)
        {
            var viewModel = new DefaultModel()
            {
                AlertMessage = message,
                AlertType = AlertType.Danger
            };

            return View(viewModel);
        }
    }

    public class FormServerValidationSampleViewModel : DefaultModel, IValidatableObject
    {
        public string Field { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Peut être fait avec un RequiredAttribute, mais l'objectif est de montrer l'utilisation de IValidatableObject.
            if (string.IsNullOrWhiteSpace(Field))
                yield return new ValidationResult("Le champ est vide.");

            if (Field.Contains("a"))
                yield return new ValidationResult("Votre chaîne ne doit pas contenir de lettre 'a'.");
        }
    }
}