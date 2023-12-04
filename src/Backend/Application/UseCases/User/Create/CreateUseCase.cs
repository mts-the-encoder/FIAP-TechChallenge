using Communication.Requests;
using Exceptions.ExceptionBase;
using Serilog;

namespace Application.UseCases.User.Create;

public class CreateUseCase
{
    public async Task Execute(UserRequest request)
    {
        Validate(request);

        //To do
    }

    private void Validate(UserRequest request)
    {
        var validator = new CreateValidator();
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors
                .Select(error => error.ErrorMessage).ToList();
            
            Log.ForContext("UserName","mts")
                .ForContext("UserId",1)
                .Error($"{errorMessages}");

            throw new ValidationErrorsException(errorMessages);
        }

    }
}