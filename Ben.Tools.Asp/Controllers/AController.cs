using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Ben.Tools.Asp.Controllers
{
    public abstract class AController
    {
        protected IEnumerable<string> ValidationMessages
        {
            get
            {
                var errors = new List<string>();

                ModelState.Values.Where(value => value.Errors.Any)
                                 .ToList()
                                 .ForEach((error) =>
                {
                    errors.AddRange(error.Errors.Select(e => e.ErrorMessage));
                });

                return errors;
            }
        }

        public bool IsValid => ModelState.IsValid;

        protected string ValidationMessage => string.Join(Environment.NewLine, ValidationMessages);
    }
}
