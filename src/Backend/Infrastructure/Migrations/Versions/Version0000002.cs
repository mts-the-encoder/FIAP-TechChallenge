using FluentMigrator;

namespace Infrastructure.Migrations.Versions;

[Migration((int)VersionsNumber.InvestmentTable,"Investments table")]
public class Version0000002 : Migration
{
    public override void Up()
    {
        CreateFixedIncome();
        CreateVariableIncome();
    }

    public override void Down()
    {
    }

    private void CreateFixedIncome()
    {
        var table = VersionBase.CreateTableWithDefaultColumns(Create.Table("FixedIncome"));

        table
            .WithColumn("Sender").AsString(100).NotNullable()
            .WithColumn("MinimumInvestment").AsDouble().NotNullable()
            .WithColumn("InvestmentFixedType").AsInt16().NotNullable()
            .WithColumn("Profitability").AsString(100).NotNullable()
            .WithColumn("MaturityDate").AsDate().NotNullable()
            .WithColumn("IR").AsInt16().NotNullable();
    }

    private void CreateVariableIncome()
    {
        var table = VersionBase.CreateTableWithDefaultColumns(Create.Table("VariableIncome"));

        table
            .WithColumn("Sender").AsString(100).NotNullable()
            .WithColumn("Name").AsString(20).NotNullable()
            .WithColumn("MinimumInvestment").AsDouble().NotNullable()
            .WithColumn("InvestmentVariableType").AsInt64().NotNullable()
            .WithColumn("Dividends").AsDouble().NotNullable()
            .WithColumn("Sector").AsInt16().NotNullable();
    }
}