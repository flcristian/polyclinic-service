using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace polyclinic_service.Users.Model;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Gender { get; set; }
    
    [Required]
    public int Age { get; set; }
    
    [Required]
    public string Phone { get; set; }
    
    [Required]
    public UserType Type { get; set; }
}