using System.Collections.Generic;
using System.ComponentModel.Composition;
using MEFDemo.Contracts;

namespace MEFDemo
{
    [Export(typeof(IDataSource))]
    public class InMemoryDataSource : IDataSource
    {
        public IList<Product> GetProducts()
        {
            var products = new List<Product>();

            products.Add(new Product { Id = 2, Name = "Pen", Units = 40, Cost = 20 });
            products.Add(new Product { Id = 6, Name = "Hen", Units = 80, Cost = 50 });
            products.Add(new Product { Id = 9, Name = "Den", Units = 30, Cost = 40 });
            products.Add(new Product { Id = 3, Name = "Zen", Units = 50, Cost = 80 });
            products.Add(new Product { Id = 5, Name = "Ken", Units = 90, Cost = 10 });
            return products;
        } 
    }
}