using System.Collections.Generic;

namespace MEFDemo.Contracts
{
    public interface IDataDestination
    {
        void Persist(IList<Product> products);
    }
}