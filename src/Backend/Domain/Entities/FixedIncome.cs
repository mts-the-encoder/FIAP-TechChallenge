using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("FixedIncome")]
public class FixedIncome : EntityBase
{
    public string Sender { get; set; }
    public double MinimumInvestment { get; set; }
    public InvestmentFixedType InvestmentFixedType { get; set; }
    public string Profitability { get; set; }
    public DateTime MaturityDate { get; set; }
    public int IR { get; set; }
    public long UserId { get; set; }
}