namespace BookLibrary.Domain.Core.DTO.UserDTOs
{
    public class AllUsersDto
    {
        public int UserAmount { get; set; }
        public List<UserView> Users { get; set; }
    }

    public class UserView
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; }
    }
}
