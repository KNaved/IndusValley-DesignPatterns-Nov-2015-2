using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using MEFDemo.Contracts;

namespace MEFDemo
{
    [Export(typeof(IDataDestination))]
    public class CsvDestination : IDataDestination
    {
        public void Persist(IList<Product> products)
        {
            var file = new StreamWriter("products.csv");
            foreach (var product in products)
            {
                file.WriteLine("{0},{1},{2},{3}", product.Id, product.Name, product.Cost, product.Units);
            }
            file.Close();
        }
    }
}