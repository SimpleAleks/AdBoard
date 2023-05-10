using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AdBoard.Contracts.Attributes;

/// <summary>
/// Проверяет расширение у файла
/// </summary>
public class FileExtensionAttribute : ValidationAttribute
{
    private readonly string[] _extensions;

    /// <summary>
    /// Инициализирует атрибут
    /// </summary>
    /// <param name="extensions">Разрешенные расширения файла</param>
    public FileExtensionAttribute(string[] extensions) => _extensions = extensions;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return ValidationResult.Success;
        var file = value as IFormFile;
        
        var extension = Path.GetExtension(file.FileName); 
        return !_extensions.Contains(extension.ToLower()) ? 
            new ValidationResult(GetErrorMessage()) : 
            ValidationResult.Success;
    }
    
    private string GetErrorMessage() => "Extension of file not allowed";
}