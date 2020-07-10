using System;
using System.Threading.Tasks;
using DLL.Model;
using DLL.Repositories;

namespace BLL.Services
{
    public interface ITransactionService
    {
        Task FinancialTransaction();
    }

    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task FinancialTransaction()
        {
            var rand = new Random();
            var amount = rand.Next(1000);
            var transaction = new TransactionHistory()
            {
                Amount = amount
            };

           

            await _unitOfWork.TransactionHistoryRepository.CreateAsync(transaction);
            if ( await  _unitOfWork.SaveCompletedAsync())
            {
                await _unitOfWork.CustomerBalanceRepository.MustUpdateBalanceAsync("tapos.aa@gmail.com", amount);
            };
            
            
        }
    }
}