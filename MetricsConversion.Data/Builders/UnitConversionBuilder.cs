using System;
using System.Collections.Generic;
using System.Text;
using MetricsConversion.Data.Builders;
using FluentMigrator.Builders.Create.Table;
using MetricsConversion.Domain.Conversions;

namespace MetricsConversion.Data.Builders
{
    public class UnitConversionBuilder : EntityBuilder<UnitConversion>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(UnitConversion.FromMetric)).AsString(255).NotNullable()
                .WithColumn(nameof(UnitConversion.MetricKey)).AsString(255).NotNullable()
                .WithColumn(nameof(UnitConversion.RateOperator)).AsString(255).NotNullable()
                .WithColumn(nameof(UnitConversion.SecondRate)).AsDecimal().NotNullable()
                .WithColumn(nameof(UnitConversion.ToMetric)).AsString(255).NotNullable()
                .WithColumn(nameof(UnitConversion.Rate)).AsDecimal().NotNullable();
        }
    }
}