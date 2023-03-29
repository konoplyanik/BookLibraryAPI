namespace BookLibrary.Domain.Core.DTO.UserDTOs
{
    public class GetUserDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; }
    }
}
