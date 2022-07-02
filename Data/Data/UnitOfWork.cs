using Data.Context;
using Data.Interfaces;
using Data.Models;
using Data.Repositories;

namespace Data.Data;

public class UnitOfWork : IDisposable
{
    private ApplicationDbContext _dbContext = new();
    
    private IRepository<Character> _characterRepository;
    private IRepository<EnvSave> _envSaveRepository;

    public IRepository<Character> Characters => _characterRepository ??= new CharacterRepository(_dbContext);
    public IRepository<EnvSave> EnvSaves => _envSaveRepository ??= new EnvSaveRepository(_dbContext);

    public void Save()
    {
        _dbContext.SaveChanges();
    }
 
    private bool disposed = false;
 
    public virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            this.disposed = true;
        }
    }
 
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}