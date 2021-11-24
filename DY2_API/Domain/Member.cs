namespace DY2_API.Domain
{
    public class Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }   
        public string Email { get; set; }   
        public List<Book> RentedBooks { get; set; }
    }
}
