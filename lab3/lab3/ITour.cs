using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public interface ITour
    {
        string Country { get; }
        int Days { get; }
        double Price { get; }
        int PeopleCount { get; }
        bool IsBooked { get; }

        double CalculateTotalPrice();
        string Book();
    }
}
