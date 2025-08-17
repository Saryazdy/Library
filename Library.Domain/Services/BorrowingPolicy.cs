using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Services
{
    public class BorrowingPolicy
    {
        private const int MaxBorrowDays = 14;
        private const decimal LateFeePerDay = 1.5m;

        public decimal CalculateLateFee(DateTime borrowDate, DateTime returnDate)
        {
            var totalDays = (returnDate - borrowDate).Days;

            if (totalDays <= MaxBorrowDays)
                return 0;

            var overdueDays = totalDays - MaxBorrowDays;
            return overdueDays * LateFeePerDay;
        }
    }
}
