using System;
using System.Threading.Tasks;
using DLL.DBContext;

namespace DLL.Repositories
{
    public interface IUnitOfWork
    {
        IDepartmentRepository DepartmentRepository { get; }
        IStudentRepository StudentRepository { get; }
        ICourseRepository CourseRepository { get; }
        ICourseStudentRepository CourseStudentRepository { get; }
        ICustomerBalanceRepository CustomerBalanceRepository { get; }
        ITransactionHistoryRepository TransactionHistoryRepository { get; }
        Task<bool> SaveCompletedAsync();
    }

    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        private IDepartmentRepository _departmentRepository;
        private IStudentRepository _studentRepository;
        private ICourseRepository _courseRepository;
        private ICourseStudentRepository _courseStudentRepository;
        private ICustomerBalanceRepository _customerBalanceRepository;
        private ITransactionHistoryRepository _transactionHistoryRepository;


        public IDepartmentRepository DepartmentRepository =>
            _departmentRepository ??= new DepartmentRepository(_context);
        public IStudentRepository StudentRepository =>
            _studentRepository ??= new StudentRepository(_context);
        
        public ICourseRepository CourseRepository =>
            _courseRepository ??= new CourseRepository(_context);
        
        public ICourseStudentRepository CourseStudentRepository =>
            _courseStudentRepository ??= new CourseStudentRepository(_context);
        
        public ICustomerBalanceRepository CustomerBalanceRepository =>
            _customerBalanceRepository ??= new CustomerBalanceRepository(_context);
        
        public ITransactionHistoryRepository TransactionHistoryRepository =>
            _transactionHistoryRepository ??= new TransactionHistoryRepository(_context);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }

        public async Task<bool> SaveCompletedAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}