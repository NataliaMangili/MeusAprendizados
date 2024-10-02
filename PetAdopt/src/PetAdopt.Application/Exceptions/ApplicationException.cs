namespace PetAdopt.Application.Exceptions;
public class ValidationExceptionErrorsBase : Exception
{
    public List<string> Errors { get; }

    public ValidationExceptionErrorsBase(List<string> errors)
        : base(FormatMessage(errors))
    {
        Errors = errors;
    }

    private static string FormatMessage(List<string> errors)
    {
        return $"Um ou mais erros de validação ocorreram: {string.Join(", ", errors)}";
    }
}
