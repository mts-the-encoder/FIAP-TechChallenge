namespace Communication.Responses;

public class FixedIncomeResponse
{
    public string Id { get; set; }
    public string Sender { get; set; }
    public double MinimumInvestment { get; set; }
    public int InvestmentFixedType { get; set; }
    public string Profitability { get; set; }
    public DateTime MaturityDate { get; set; }
    public int IR { get; set; }
}