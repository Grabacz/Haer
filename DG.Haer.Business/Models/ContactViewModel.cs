using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace DG.Haer.Business
{
    public class ContactViewModel : ViewModel, IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Experience { get; set; }
        public decimal Salary { get; set; }
        public byte ContactType { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            var validator = new ContactViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(x => new ValidationResult(x.ErrorMessage, new[] { x.PropertyName }));
        }
    }
}
