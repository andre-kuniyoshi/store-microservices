
namespace Inventory.Infra.Data.SQL
{
    internal static class ProductScripts
    {
        internal const string CreateTableAndInsertValues = @"
            CREATE EXTENSION IF NOT EXISTS ""uuid-ossp"";
            CREATE TABLE IF NOT EXISTS Products (
                    Id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
                    Quantity INT  NOT NULL,
                    Price DECIMAL(10,2) NOT NULL,

                    Active BOOLEAN NOT NULL,
                    CreatedBy VARCHAR (50) NOT NULL,
                    CreatedDate TIMESTAMP  NOT NULL,
                    LastModifiedBy VARCHAR (50),
                    LastModifiedDate TIMESTAMP
            );
        
            INSERT INTO Products (Id, Quantity, Price, Active, CreatedBy, CreatedDate, LastModifiedBy, LastModifiedDate)
                VALUES ('569e3fa7-eef9-48a2-9dd6-7e30a1187d32', 50, 950.00, true, 'Admin', '2023-05-20 00:00:00', null, null )
                ON CONFLICT DO NOTHING;

            INSERT INTO Products (Id, Quantity, Price, Active, CreatedBy, CreatedDate, LastModifiedBy, LastModifiedDate)
                VALUES ('6b8c6058-76e1-4ab4-bcdc-72bb2d00dc12', 50, 840.00, true, 'Admin', '2023-05-20 00:00:00', null, null )
                ON CONFLICT DO NOTHING;

            INSERT INTO Products (Id, Quantity, Price, Active, CreatedBy, CreatedDate, LastModifiedBy, LastModifiedDate)
                VALUES ('327cc2b9-603e-4196-8158-aa8b6570dc60', 50, 650.00, true, 'Admin', '2023-05-20 00:00:00', null, null )
                ON CONFLICT DO NOTHING; 

            INSERT INTO Products (Id, Quantity, Price, Active, CreatedBy, CreatedDate, LastModifiedBy, LastModifiedDate)
                VALUES ('8e774172-c954-4c22-a9f3-1374e2178cb0', 50, 470.00, true, 'Admin', '2023-05-20 00:00:00', null, null )
                ON CONFLICT DO NOTHING; 

            INSERT INTO Products (Id, Quantity, Price, Active, CreatedBy, CreatedDate, LastModifiedBy, LastModifiedDate)
                VALUES ('cce0e675-454a-4897-94a8-ff3c8a7976a5', 50, 380.00, true, 'Admin', '2023-05-20 00:00:00', null, null )
                ON CONFLICT DO NOTHING;

            INSERT INTO Products (Id, Quantity, Price, Active, CreatedBy, CreatedDate, LastModifiedBy, LastModifiedDate)
                VALUES ('ffbb2d4b-90b8-4e08-9954-6062a3c2ffd3', 50, 240.00, true, 'Admin', '2023-05-20 00:00:00', null, null )
                ON CONFLICT DO NOTHING; 
        ";
    }
}
