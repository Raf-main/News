
using System.ComponentModel.DataAnnotations;

namespace PRAS.Testovoe.Main.Attributes;

public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions;
    public AllowedExtensionsAttribute(params string[] extensions)
    {
        _extensions = extensions;
    }
    
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;

        if (file != null)
        {
            var extension = Path.GetExtension(file.FileName);

            if(extension == null)
            {
                return new ValidationResult(GetErrorMessage());
            }

            if (!_extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult(GetErrorMessage());
            }
        }
        
        return ValidationResult.Success;
    }

    private string GetErrorMessage()
    {
        return $"This photo extension is not allowed";
    }
}