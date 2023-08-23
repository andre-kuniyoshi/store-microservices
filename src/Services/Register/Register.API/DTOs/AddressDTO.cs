
namespace Register.API.DTOs
{
    public class AddressDTO
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string? Number { get; set; }
        public string? Complement { get; set; }
        public string District { get; set; }
        public string CEP { get; set; }
        public string City { get; set; }
        public string UF { get; set; }
    }
}
