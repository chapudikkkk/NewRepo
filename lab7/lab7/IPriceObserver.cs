using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    internal interface IPriceObserver
    {
        void OnPriceChanged(double price);
    }
}
