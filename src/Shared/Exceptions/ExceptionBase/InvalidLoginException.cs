using System.Runtime.Serialization;

namespace Exceptions.ExceptionBase;

public class InvalidLoginException : TechChallengeException
{
    public InvalidLoginException() : base(ErrorMessages.LOGIN_INVALIDO) { }

    protected InvalidLoginException(SerializationInfo info,StreamingContext context) : base(info,context)
    {
    }
}