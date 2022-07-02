using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<Character> Characters { get; set; }
    public DbSet<EnvSave> EnvSaves { get; set; }
}