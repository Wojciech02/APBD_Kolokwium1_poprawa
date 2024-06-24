namespace KOLOKWIUM.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty; 
        public DateTime PublicationDate { get; set; }
        public Author Author { get; set; } = new Author(); 
        public Category Category { get; set; } = new Category();
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public int LibraryId { get; set; }
    }
}
