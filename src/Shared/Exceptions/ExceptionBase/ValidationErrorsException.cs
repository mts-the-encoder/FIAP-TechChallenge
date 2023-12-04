namespace Exceptions.ExceptionBase;

public class ValidationErrorsException : TechChallengeException
{
    public List<string> ErrorMessages { get; set; }

    public ValidationErrorsException(List<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}