using FluentValidation;
using TestApi.Core.Entity;

namespace TestApi.Core.Validators
{
    public class PersonValidator:AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().Length(1, 254);
            RuleFor(x => x.FirstName).NotEmpty().Length(1, 150);
            RuleFor(x => x.LastName).NotEmpty().Length(1, 150);
            RuleFor(x => x.Address).NotNull();
        }
        
    }

    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.AddressLine1).NotEmpty().MaximumLength(150);
            RuleFor(x => x.CountryIsoCode).NotEmpty();
            RuleFor(x => x.AddressLine2).MaximumLength(150);
            RuleFor(x => x.AddressLine3).MaximumLength(150);
            RuleFor(x => x.City).MaximumLength(40);
            RuleFor(x => x.Postcode).MaximumLength(60);
            RuleFor(x => x.State).MaximumLength(60);
        }
    }

    public class OrganisationValidator : AbstractValidator<Organisation>
    {
        public OrganisationValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(120);
            RuleFor(x => x.Address).NotNull();
            
        }
    }

    public class RegistrationRequestValidator : AbstractValidator<Registration>
    {
        public RegistrationRequestValidator()
        {
            RuleFor(x => x.Person).NotNull();
            RuleFor(x => x.RegistrationDate).Cascade(CascadeMode.Stop).NotEmpty().Must(x=>x!=default);
        }
    }
}