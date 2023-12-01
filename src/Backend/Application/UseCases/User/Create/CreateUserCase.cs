using Communication.Requests;
using Serilog;

namespace Application.UseCases.User.Create;

public class CreateUserCase
{
    public Task Execute(UserRequest request)
    {
        return null;
    }

    private void Validate(UserRequest request)
    {
        var validator = new CreateValidator();
        var result = validator.Validate(request);

        Log.Information($"Test => {result}", result);
    }
}