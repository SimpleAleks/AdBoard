using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AdBoard.Contracts.Attributes;

/// <summary>
/// Проверяет наличие расширений у массива файлов
/// </summary>
public class FilesExtensionAttribute : ValidationAttribute
{
    private readonly string[] _extensions;

    /// <summary>
    /// Инициализирует атрибут
    /// </summary>
    /// <param name="extensions">Разрешенные расширения файла</param>
    public FilesExtensionAttribute(string[] extensions) => _extensions = extensions;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return ValidationResult.Success;
        var files = value as IEnumerable<IFormFile>;
        foreach (var file in files)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!_extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult(GetErrorMessage());
            }
        }
        
        return ValidationResult.Success;
    }
    
    private string GetErrorMessage() => "Extension of file not allowed";
}