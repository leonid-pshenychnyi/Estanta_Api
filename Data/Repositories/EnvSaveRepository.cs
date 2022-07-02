using Data.Context;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class EnvSaveRepository : IRepository<EnvSave>
{
    private ApplicationDbContext db;

    public EnvSaveRepository(ApplicationDbContext context)
    {
        db = context;
    }
    
    public async Task<EnvSave> Get(Guid id)
    {
        return await db.EnvSaves.FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<IEnumerable<EnvSave>> GetAll()
    {
        return await db.EnvSaves.ToListAsync();
    }

    public async Task Add(EnvSave entity)
    {
        await db.EnvSaves.AddAsync(entity);
    }

    public void Delete(EnvSave entity)
    {
        db.EnvSaves.Remove(entity);
    }

    public void Update(EnvSave entity)
    {
        db.EnvSaves.Update(entity);
    }
}