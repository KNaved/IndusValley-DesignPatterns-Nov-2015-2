using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using MEFDemo.Contracts;

namespace XmlProductPersistor
{
    [Export(typeof(IDataDestination))]
    public class XmlProductDestinatior : IDataDestination
    {
        public void Persist(IList<Product> products)
        {
            new XElement("Products", products.Select(p => new XElement("Product",
                new XElement("Id", p.Id)
                , new XElement("Name", p.Name)
                , new XElement("Cost", p.Cost)
                , new XElement("Units", p.Units))))
            .Save("products.xml");
        }
    }
}
