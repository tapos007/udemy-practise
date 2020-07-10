using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DLL.DBContext;
using DLL.Model;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public interface ICustomerBalanceRepository: IRepositoryBase<CustomerBalance>
    {
        Task MustUpdateBalanceAsync(string email, decimal amount);
    }

    public class CustomerBalanceRepository : RepositoryBase<CustomerBalance>, ICustomerBalanceRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerBalanceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task MustUpdateBalanceAsync(string email, decimal amount)
        {
            var customerBalance =
                await _context.CustomerBalances.FirstOrDefaultAsync(x => x.Email == "tapos.aa@gmail.com");
            customerBalance.Balance += amount;
            bool isUpdated = false;
            do
            {
                try
                {
                    
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        isUpdated = true;
                    };
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        if(!(entry.Entity is CustomerBalance)) continue;
                        var databaseEntry = entry.GetDatabaseValues();
                        var databaseValues = (CustomerBalance) databaseEntry.ToObject();
                        databaseValues.Balance += amount;

                        entry.OriginalValues.SetValues(databaseEntry);
                        entry.CurrentValues.SetValues(databaseValues);
                    }
                }
            } while (!isUpdated);


        }
    }
}