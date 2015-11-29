using System.Collections.Generic;

namespace MEFDemo.Contracts
{
    public interface IDataSource
    {
        IList<Product> GetProducts();
    }
}