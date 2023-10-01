
namespace Inventory.Infra.Data.SQL
{
    internal static class SaleScripts
    {
        internal const string CreateTableAndInsertValues = @"
            CREATE EXTENSION IF NOT EXISTS ""uuid-ossp"";
            CREATE TABLE IF NOT EXISTS Sales (
                    Id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
                    ProductId UUID NOT NULL,
                    StartDate TIMESTAMP  NOT NULL,
                    EndDate TIMESTAMP  NOT NULL,
                    Price DECIMAL(10,2) NOT NULL,

                    Active BOOLEAN NOT NULL,
                    CreatedBy VARCHAR (50) NOT NULL,
                    CreatedDate TIMESTAMP  NOT NULL,
                    LastModifiedBy VARCHAR (50),
                    LastModifiedDate TIMESTAMP
            );
        
            INSERT INTO Sales (Id, ProductId, StartDate, EndDate, Price, Active, CreatedBy, CreatedDate, LastModifiedBy, LastModifiedDate)
                VALUES ('93dac714-0926-450a-be87-6af6ca022bab', '327cc2b9-603e-4196-8158-aa8b6570dc60', '2023-05-20 00:00:00', '2023-10-20 00:00:00', 100.00, true, 'Admin', '2023-05-20 00:00:00', null, null )
	            ON CONFLICT DO NOTHING;

            INSERT INTO Sales (Id, ProductId, StartDate, EndDate, Price, Active, CreatedBy, CreatedDate, LastModifiedBy, LastModifiedDate)
                VALUES ('0d35fd52-e569-4d0e-95b9-1966342e3cb8', '327cc2b9-603e-4196-8158-aa8b6570dc60', '2023-05-20 00:00:00', '2023-10-20 00:00:00', 100.00, true, 'Admin', '2023-05-20 00:00:00', null, null )
	            ON CONFLICT DO NOTHING; 
        ";
    }
}
