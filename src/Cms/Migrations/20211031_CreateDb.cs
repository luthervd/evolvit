using FluentMigrator;

namespace Cms.Migrations
{
    [Migration(202110311146)]
    public class CreateDb : Migration
    {
        
        public override void Down()
        {
            
        }

        public override void Up()
        {
            Create.Table(MigrationGlobalConsts.ContentAreaTable)
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("name").AsString(256).NotNullable()
                .WithColumn("description").AsString().NotNullable()
                .WithColumn("created").AsDateTime().WithDefaultValue(SystemMethods.CurrentDateTime).NotNullable();

            Create.Table(MigrationGlobalConsts.ContentTable)
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("name").AsString(256)
                .WithColumn("description").AsString()
                .WithColumn("data").AsCustom("JSON")
                .WithColumn("created").AsDateTime().WithDefaultValue(SystemMethods.CurrentDateTime).NotNullable();

            Create.Table(MigrationGlobalConsts.ContentTemplate)
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString()
                .WithColumn("created").AsDateTime().WithDefaultValue(SystemMethods.CurrentDateTime).NotNullable();

            Create.Table(MigrationGlobalConsts.ContentField)
              .WithColumn("id").AsInt32().PrimaryKey().Identity()
              .WithColumn("template_id").AsInt32().ForeignKey(MigrationGlobalConsts.ContentTemplate,"id")
              .WithColumn("name").AsString()
              .WithColumn("type").AsString();

        }
    }
}
