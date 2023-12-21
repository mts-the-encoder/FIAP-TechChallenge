namespace Communication.Requests;

public class VariableIncomeRequest
{
    public string Sender { get; set; }
    public string Name { get; set; }
    public double MinimumInvestment { get; set; }
    public int InvestmentVariableType { get; set; }
    public double Dividends { get; set; }
    public int Sector { get; set; }
}