using FluentValidation;

namespace DG.Haer.Business
{
    public class ContactViewModelValidator : AbstractValidator<ContactViewModel>
    {
        public ContactViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(3, 100)
                .WithMessage("Nazwa kontaktu musi mieć od 3 do 100 znaków");

            RuleFor(x => x.ContactType)
                .Must(ValidContactType)
                .WithMessage("Typ kontaktu nie jest prawidłowy");

            RuleFor(x => x.Experience)
                .NotEmpty()
                .WithMessage("Doświadczenie nie zostało podane");

            RuleFor(x => x.Salary)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Wynagrodzenie nie zostało podane");
        }

        private bool ValidContactType(byte contactType)
        {
            return contactType == 0 || contactType == 1;
        }
    }
}
