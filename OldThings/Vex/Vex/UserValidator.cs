using FluentValidation;

using Vex.DbModels;

namespace Vex
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Birthday);
            RuleFor(user => user.EmployeeId);
            RuleFor(user => user.Email).NotEmpty().EmailAddress();
            RuleFor(user => user.MobilePhone).NotEmpty();
            RuleFor(user => user.EmergencyContact).NotEmpty();
            RuleFor(user => user.EmergencyPhone).NotEmpty();
            RuleFor(user => user.OfficeAddress).NotEmpty();
            RuleFor(user => user.DeliveryAddress).NotEmpty();
            RuleFor(user => user.WorkingLocationId).NotEmpty();
            RuleFor(user => user.SectorId).NotEmpty();
            RuleFor(user => user.BusinessFunlocId).NotEmpty();
            RuleFor(user => user.CostCenterId).NotEmpty();

            RuleFor(user => user.ChineseName).NotEmpty().When(user => string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName));
        }
    }
}