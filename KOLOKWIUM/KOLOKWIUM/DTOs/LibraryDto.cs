using System;
using System.Collections.Generic;
namespace KOLOKWIUM.DTOs
{
    public class LibraryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; 
        public string Address { get; set; } = string.Empty; 
        public List<BookDto> Books { get; set; } = new List<BookDto>();
    }
}
