using Library.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Policies
{
    public class DiscountPolicy
    {
        public decimal ApplyDiscount(decimal price, decimal discountPercent)
        {
            if (discountPercent > DomainConstants.MaxDiscountPercent)
                discountPercent = DomainConstants.MaxDiscountPercent;

            return price - (price * discountPercent);
        }
    }
}
