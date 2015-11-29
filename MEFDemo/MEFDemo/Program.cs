using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MEFDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataTransformer = new DataTransformer();
            var assemblyCatalog = new AssemblyCatalog(typeof (Program).Assembly);
            var dirCatalog = new DirectoryCatalog("destinations");
            var aggregateCatalog = new AggregateCatalog(assemblyCatalog, dirCatalog);
            var container = new CompositionContainer(aggregateCatalog);

            container.ComposeParts(dataTransformer);
            dataTransformer.Transform();
            Console.WriteLine("Job done..");
            Console.ReadLine();

        }
    }
}
