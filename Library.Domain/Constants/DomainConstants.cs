using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Constants
{
    public static class DomainConstants
    {
        public const int MaxBorrowDays = 14;
        public const decimal LateFeePerDay = 1.5m;
        public const decimal MaxDiscountPercent = 0.25m; // 25%
    }
}
