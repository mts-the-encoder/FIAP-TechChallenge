using Communication.Enum;

namespace Communication.Responses;

public class VariableIncomeDashboardResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public InvestmentVariableType Type { get; set; }
}