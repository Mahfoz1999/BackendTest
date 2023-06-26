using BackendTest_DTO.Student;
using FluentValidation;

namespace BackendTest.FluentValidation
{
    public class StudentFormDtoValidator : AbstractValidator<StudentFormDto>
    {
        public StudentFormDtoValidator()
        {
            RuleFor(x => x.StudentReg).NotNull().WithMessage("Registration number is required").Length(1, 10)
                .WithMessage("Registration number must be between 1 and 10 characters");
            RuleFor(x => x.FullName).NotNull().WithMessage("Full Name is required").Length(1, 50).WithMessage("Full Name must be between 1 and 50 characters.");
        }
    }
}
