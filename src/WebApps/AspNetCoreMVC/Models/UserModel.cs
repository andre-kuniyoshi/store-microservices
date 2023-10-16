namespace AspNetCoreMVC.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Document { get; set; }
        public string Ddd { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public UserAddressModel Address { get; set; }
    }
}
