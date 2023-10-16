
namespace Register.API.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Document { get; set; }
        public string Ddd { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public bool IsAddressComplete { get; set; }
        public AddressDTO Address { get; set; }
    }
}
