using Core.Common;

namespace Register.Application.Domain.Entities
{
    public class User : BaseControlEntity
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Document { get; set; }
        public string Ddd { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public Address Address { get; set; }
    }
}
