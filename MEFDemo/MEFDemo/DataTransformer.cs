using System.ComponentModel.Composition;
using MEFDemo.Contracts;

namespace MEFDemo
{
    public class DataTransformer
    {
        [Import]
        public IDataSource DataSource { get; set; }

        [ImportMany]
        public IDataDestination[] DataDestinations { get; set; }

        public void Transform()
        {
            var products = DataSource.GetProducts();
            foreach (var dataDestination in DataDestinations)
            {
                dataDestination.Persist(products);    
            }
            
        }
    }
}