using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.AliExpress.Dropshipping.Domain;

namespace Nop.Plugin.Misc.AliExpress.Dropshipping.Migrations
{
    [NopMigration("2023/07/15 00:10:00:1687541", "Other.AliExpressDropshippingData base schema", MigrationProcessType.Installation)]
    public class SchemaMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.TableFor<AliExpressDropshippingData>();
        }

    }
}
