using Communication.Requests;
using Communication.Responses;

namespace Application.UseCases.DashboardVariable;

public interface IDashboardVariableUseCase
{
    Task<DashboardVariableResponse> Execute(VariableDashboardRequest request);
}