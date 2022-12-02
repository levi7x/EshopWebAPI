namespace EshopWebAPI.Models.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Subscribed { get; set; }
    }
}
