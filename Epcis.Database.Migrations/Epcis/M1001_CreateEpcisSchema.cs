using System;
using FluentMigrator;

namespace Epcis.Database.Migrations.Epcis
{
    [Migration(1001)]
    // ReSharper disable once InconsistentNaming
    public class M1001_CreateEpcisSchema : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Schema(DatabaseConstants.Schemas.Epcis);
        }
    }
}