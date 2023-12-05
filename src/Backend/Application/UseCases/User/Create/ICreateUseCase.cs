using Communication.Requests;
using Communication.Responses;

namespace Application.UseCases.User.Create;

public interface ICreateUseCase
{
    Task<UserResponse> Execute(UserRequest request);
}