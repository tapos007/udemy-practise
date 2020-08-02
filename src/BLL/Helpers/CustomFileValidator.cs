using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Resources;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;

namespace BLL.Helpers
{
    public class CustomFileValidator : AsyncValidatorBase
    {
        private readonly IFileValidate _fileValidate;

        public CustomFileValidator(IFileValidate fileValidate) : base("{ErrorMessage}")
        {
            _fileValidate = fileValidate;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context,
            CancellationToken cancellation)
        {
            var fileToValidate = context.PropertyValue as IFormFile;


            var (valid, errorMessage) =  _fileValidate.ValidateFile(fileToValidate);

            if (valid) return true;
            context.MessageFormatter.AppendArgument("ErrorMessage", errorMessage);
            return false;

        }
    }
}