using Communication.Requests;

namespace Application.UseCases.User.Create;

public interface ICreateUseCase
{
    Task Execute(UserRequest request);
}