﻿using Communication.Enum;

namespace Communication.Requests;

public class FixedIncomeRequest
{
    public string Sender { get; set; }
    public double MinimumInvestment { get; set; }
    public InvestmentFixedType InvestmentFixedType { get; set; }
    public string Profitability { get; set; }
    public DateTime MaturityDate { get; set; }
    public int IR { get; set; }
}