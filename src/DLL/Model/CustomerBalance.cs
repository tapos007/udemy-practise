using System;
using System.ComponentModel.DataAnnotations;

namespace DLL.Model
{
    public class CustomerBalance
    {
        public long CustomerBalanceId { get; set; }
        public string  Email { get; set; }
        public Decimal Balance { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}