using System.ComponentModel.DataAnnotations;

namespace Data.Models;

public class EnvSave
{
    [Key]
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string Path { get; set; }
}