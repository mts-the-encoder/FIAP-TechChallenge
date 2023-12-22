using Communication.Enum;

namespace Communication.Requests;

public class VariableDashboardRequest
{
    public string NameOrSender { get; set; }
    public InvestmentVariableType? Type { get; set; }
}