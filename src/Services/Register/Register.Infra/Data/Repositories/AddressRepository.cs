using Register.Application.Domain.Entities;
using Register.Application.Interfaces;
using Register.Infra.Data.Context;

namespace Register.Infra.Data.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(RegistersDbContext context) : base(context)
        {
        }
    }
}
