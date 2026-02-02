using System.ComponentModel.DataAnnotations;

namespace TestApp.Core.Entities;

public class Contact
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public DateTime DateOfBirth { get; set; }
    
    public bool Married { get; set; }
    
    [Required]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;
    
    [Required]
    [Range(0, double.MaxValue)]
    public decimal Salary { get; set; }
}