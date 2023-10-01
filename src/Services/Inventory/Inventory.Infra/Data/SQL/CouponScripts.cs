
namespace Inventory.Infra.Data.SQL
{
    internal static class CouponScripts
    {
        internal const string CreateTableAndInsertValues = @"
            CREATE EXTENSION IF NOT EXISTS ""uuid-ossp"";
            CREATE TABLE IF NOT EXISTS Coupons (
                    Id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
                    Code VARCHAR (50) NOT NULL,
                    Description VARCHAR (50) NOT NULL,
                    Quantity INT NOT NULL,
                    PercentageDiscount DECIMAL(2,2) NOT NULL,
                    ExpirationDate TIMESTAMP NOT NULL,

                    Active BOOLEAN NOT NULL,
                    CreatedBy VARCHAR (50) NOT NULL,
                    CreatedDate TIMESTAMP  NOT NULL,
                    LastModifiedBy VARCHAR (50),
                    LastModifiedDate TIMESTAMP
            );
        
        ";
    }
}
