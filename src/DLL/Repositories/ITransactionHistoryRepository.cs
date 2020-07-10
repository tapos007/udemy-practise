using System.Collections.Generic;
using System.Threading.Tasks;
using DLL.DBContext;
using DLL.Model;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public interface ITransactionHistoryRepository: IRepositoryBase<TransactionHistory>
    {
      
    }

    public class TransactionHistoryRepository : RepositoryBase<TransactionHistory>, ITransactionHistoryRepository
    {
        public TransactionHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}