using Data.Context;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CharacterRepository : IRepository<Character>
{
    private ApplicationDbContext db;

    public CharacterRepository(ApplicationDbContext context)
    {
        db = context;
    }

    public async Task<Character> Get(Guid id)
    {
        return await db.Characters.FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<IEnumerable<Character>> GetAll()
    {
        return await db.Characters.ToListAsync();
    }

    public async Task Add(Character entity)
    {
        await db.Characters.AddAsync(entity);
    }

    public void Delete(Character entity)
    {
        db.Characters.Remove(entity);
    }

    public void Update(Character entity)
    {
        db.Characters.Update(entity);
    }
}