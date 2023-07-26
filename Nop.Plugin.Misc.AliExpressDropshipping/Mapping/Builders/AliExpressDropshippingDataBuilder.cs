using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Customers;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.AliExpress.Dropshipping.Domain;

namespace Nop.Plugin.Misc.AliExpress.Dropshipping.Mapping.Builders
{
    public class AliExpressDropshippingDataBuilder : NopEntityBuilder<AliExpressDropshippingData>
    {
        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            //map the primary key (not necessary if it is Id field)
            table.WithColumn(nameof(AliExpressDropshippingData.Id)).AsInt32().PrimaryKey()
            .WithColumn(nameof(AliExpressDropshippingData.AccessToken)).AsString()
            .WithColumn(nameof(AliExpressDropshippingData.RefreshToken)).AsString()
            .WithColumn(nameof(AliExpressDropshippingData.AccessTokenExpireTime)).AsString(50)
            .WithColumn(nameof(AliExpressDropshippingData.RefreshTokenExpireTime)).AsString(50)
            .WithColumn(nameof(AliExpressDropshippingData.AccessTokenValidTime)).AsString()
            .WithColumn(nameof(AliExpressDropshippingData.RefreshTokenValidTime)).AsString()
            .WithColumn(nameof(AliExpressDropshippingData.AccountEmail)).AsString(100);
        }
    }
}
