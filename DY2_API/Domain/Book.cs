namespace DY2_API.Domain
{
    public class Book
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Author Author { get; set; }
        public string? Publisher { get; set; }
        public Member? RentedTo { get; set; }
        public int AuthorId { get; set; }
        public int? RentedToId { get; set; }
    }
}
