using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookAPI.Models
{
    public class Book { 
    public Book()
    {
    }

    [Key]
    public int Id { get; set; }
    [Column(TypeName = "nvarchar(100)")]
    [Required]
    public string Name { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    [Required]
    public string Genere { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    [Required]
    public string PublisherName { get; set; }

}
}
