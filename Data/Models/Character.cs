using System.ComponentModel.DataAnnotations;

namespace Data.Models;

public class Character
{
    [Key]
    public Guid Id { get; set; }
    public string Elements { get; set; }
    public string Key { get; set; }
}