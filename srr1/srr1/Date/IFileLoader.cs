using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srr1.Date
{
    public interface IFileLoader
    {
        IAsyncEnumerable<T> LoadStreamAsync<T>(
            string filePath,
            [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default);
    }
}
