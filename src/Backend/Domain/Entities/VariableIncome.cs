using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities;

[Table("VariableIncome")]
public class VariableIncome : EntityBase
{
    public string Sender { get; set; }
    public string Name { get; set; }
    public double MinimumInvestment { get; set; }
    public InvestmentVariableType InvestmentVariableType { get; set; }
    public double Dividends { get; set; }
    public Sector Sector { get; set; }
    public long UserId { get; set; }
}