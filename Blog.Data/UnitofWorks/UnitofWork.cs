using Blog.Data.Context;
using Blog.Data.Reposities.Abstractions;
using Blog.Data.Reposities.Concretes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.UnitofWorks
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDbContext dbContext;

        public UnitofWork(AppDbContext dbContext) { 
        
            this.dbContext = dbContext;
        }

        public async ValueTask DisposeAsync()
        {
            await dbContext.DisposeAsync();
        }

        public int save()
        {
            return dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        IRepository<T> IUnitofWork.GetRepository<T>()
        {
            return new Repository<T>(dbContext);
        }
    }
}
