using Communication.Requests;
using Serilog;

namespace Application.UseCases.User.Create;

public class CreateUseCase
{
    public Task Execute(UserRequest request)
    {
        return null;
    }

    private void Validate(UserRequest request)
    {
        var validator = new CreateValidator();
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage);
            Log.Information($"Test => {result}",result);
        }

    }
}