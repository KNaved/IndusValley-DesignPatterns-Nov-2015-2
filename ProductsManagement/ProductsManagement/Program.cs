using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Collections;

namespace ProductsManagement
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int Units { get; set; }
        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}", this.Id, this.Name, this.Cost, this.Units);
        }
    }
    public interface ICompareProduct
    {
        int Compare(Product p1, Product p2);
    }
    public interface ICompare<in T>
    {
        int Compare(T p1, T p2);
    }

    public delegate int CompareProductDelegate(Product p1, Product p2);
    public delegate int CompareDelegate<T>(T p1, T p2);

    //public class CompareProductById : ICompareProduct
    public class CompareProductById : ICompare<Product>
    {
        public int Compare(Product p1, Product p2)
        {
            if (p1.Id < p2.Id) return -1;
            if (p1.Id == p2.Id) return 0;
            return 1;
        }
    }

    public class CompareProductByUnits : ICompare<Product> //ICompareProduct
    {
        public int Compare(Product p1, Product p2)
        {
            if (p1.Units < p2.Units) return -1;
            if (p1.Units == p2.Units) return 0;
            return 1;
        }
    }

    public interface IFilterProduct
    {
        bool IsSatisfiedBy(Product product);
    }

    public interface IFilter<in T>
    {
        bool IsSatisfiedBy(T item);
    }

    public class AffordableProductCriteria : IFilter<Product> //IFilterProduct
    {
        public bool IsSatisfiedBy(Product product)
        {
            return product.Cost < 50;
        }
    }
    //public class CostlyProductCriteria : IFilterProduct
    //{
    //    public bool IsSatisfiedBy(Product product)
    //    {
    //        var afforedableProductCriteria = new AffordableProductCriteria();
    //        return !afforedableProductCriteria.IsSatisfiedBy(product);
    //    }
    //}

    public class NotCriteria : IFilter<Product> //IFilterProduct
    {
        IFilter<Product> _filter;
        public NotCriteria(IFilter<Product> filter)
        {
            this._filter = filter;
        }
        public bool IsSatisfiedBy(Product product)
        {
            return !this._filter.IsSatisfiedBy(product);
        }
    }

    public class AndCriteria : IFilter<Product> //IFilterProduct
    {
        public IFilter<Product> _leftCriteria;
        public IFilter<Product> _rightCriteria;
        public AndCriteria(IFilter<Product> leftCriteria, IFilter<Product> rightCriteria)
        {
            this._leftCriteria = leftCriteria;
            this._rightCriteria = rightCriteria;
        }
        public bool IsSatisfiedBy(Product product)
        {
            return this._leftCriteria.IsSatisfiedBy(product) && this._rightCriteria.IsSatisfiedBy(product);
        }
    }

    public class OrCriteria : IFilter<Product>
    {
        public IFilter<Product> _leftCriteria;
        public IFilter<Product> _rightCriteria;
        public OrCriteria(IFilter<Product> leftCriteria, IFilter<Product> rightCriteria)
        {
            this._leftCriteria = leftCriteria;
            this._rightCriteria = rightCriteria;
        }
        public bool IsSatisfiedBy(Product product)
        {
            return this._leftCriteria.IsSatisfiedBy(product) || this._rightCriteria.IsSatisfiedBy(product);
        }
    }



    public class OverStockedProductCriteria : IFilter<Product>
    {
        public bool IsSatisfiedBy(Product product)
        {
            return product.Units > 50;
        }
    }

    //public class LessStockedProductCriteria : IFilterProduct
    //{
    //    public bool IsSatisfiedBy(Product product)
    //    {
    //        var overStockedProductCriteria = new OverStockedProductCriteria();
    //        return !overStockedProductCriteria.IsSatisfiedBy(product);
    //    }
    //}

    public class MyCollection<T> : IEnumerable, IEnumerator
    {

        private ArrayList list = new ArrayList();
        public void Add(T item)
        {
            list.Add(item);
        }
        public void Remove(T item)
        {
            list.Remove(item);
        }
        public int Count
        {
            get
            {
                return list.Count;
            }
        }
        public T GetByIndex(int index)
        {
            return (T)list[index];
        }

        public T this[int index]
        {
            get
            {
                return (T)list[index];
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        int index = -1;
        public object Current
        {
            get { return list[index]; }
        }

        public bool MoveNext()
        {
            ++index;
            if (index < list.Count) return true;
            Reset();
            return false;
        }

        public void Reset()
        {
            index = -1;
        }

        public void Sort(ICompare<T> itemComparer)
        {

            for (var i = 0; i < list.Count - 1; i++)
                for (var j = i + 1; j < list.Count; j++)
                {
                    var left = (T)(list[i]);
                    var right = (T)(list[j]);
                    if (itemComparer.Compare(left, right) > 0)
                    {
                        var temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
        }

        public void Sort(CompareDelegate<T> compareItem)
        {

            for (var i = 0; i < list.Count - 1; i++)
                for (var j = i + 1; j < list.Count; j++)
                {
                    var left = (T)(list[i]);
                    var right = (T)(list[j]);
                    if (compareItem(left, right) > 0)
                    {
                        var temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
        }

        public MyCollection<T> Filter(IFilter<T> filter)
        {
            var result = new MyCollection<T>();
            foreach (var item in list)
            {
                var tItem = (T)item;
                if (filter.IsSatisfiedBy(tItem))
                    result.Add(tItem);
            }
            return result;
        }

        public void ForEach(FnBlockDelegate<T> block)
        {
            foreach (var item in list)
            {
                var tItem = (T)item;
                block(tItem);
            }
        }


    }

    class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PubDate { get; set; }
        public decimal Cost { get; set; }
        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}", this.Id, this.Title, this.PubDate, this.Cost);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            //var products = new ProductsCollection();
            var products = new MyCollection<Product>();

            products.Add(new Product { Id = 2, Name = "Pen", Units = 40, Cost = 20 });
            products.Add(new Product { Id = 6, Name = "Hen", Units = 80, Cost = 50 });
            products.Add(new Product { Id = 9, Name = "Den", Units = 30, Cost = 40 });
            products.Add(new Product { Id = 3, Name = "Zen", Units = 50, Cost = 80 });
            products.Add(new Product { Id = 5, Name = "Ken", Units = 90, Cost = 10 });
            Utils.Print("Default List", () => products.ForEach(p => Console.WriteLine(p)));


            Utils.Print("After sorting", () =>
            {
                products.Sort(new CompareProductById());
                products.ForEach(p => Console.WriteLine(p));
            });


            Utils.Print("After sorting By Units", () =>
            {
                products.Sort(new CompareProductByUnits());
                products.ForEach(p => Console.WriteLine(p));
            });

            Console.WriteLine("Affordable Products");
            Console.WriteLine("===============================");
            var affordableProducts = products.Filter(new AffordableProductCriteria());
            foreach (var product in affordableProducts)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine();

            Console.WriteLine("Costly Products");
            Console.WriteLine("===============================");
            var costlyProductCriteria = new NotCriteria(new AffordableProductCriteria());
            var costlyProducts = products.Filter(costlyProductCriteria);
            foreach (var product in costlyProducts)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine();

            Console.WriteLine("Overstocked Products");
            Console.WriteLine("===============================");
            var overStockedProducts = products.Filter(new OverStockedProductCriteria());
            foreach (var product in overStockedProducts)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine();

            Console.WriteLine("Less Stocked Products");
            Console.WriteLine("===============================");
            var lessStockedProductCriteria = new NotCriteria(new OverStockedProductCriteria());
            var lessStockedProducts = products.Filter(lessStockedProductCriteria);
            foreach (var product in lessStockedProducts)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine();

            Console.WriteLine("Overstocked AND Affordable Products");
            Console.WriteLine("===============================");
            var overStockedCostlyProductCriteria = new AndCriteria(new OverStockedProductCriteria(), new AffordableProductCriteria());
            var overStockedCostlyProducts = products.Filter(overStockedCostlyProductCriteria);
            foreach (var product in overStockedCostlyProducts)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine();

            Console.WriteLine("Overstocked OR Affordable Products");
            Console.WriteLine("===============================");
            var overStockedOrCostlyProductCriteria = new OrCriteria(new OverStockedProductCriteria(), new AffordableProductCriteria());
            var overStockedOrCostlyProducts = products.Filter(overStockedOrCostlyProductCriteria);
            foreach (var product in overStockedOrCostlyProducts)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine();

            Console.WriteLine("Using Delegates");
            Console.WriteLine();
            Console.WriteLine("Sorting by Cost [using delegates]");
            Console.WriteLine("===============================");
            //products.Sort(Program.CompareProductByCost);
            //products.Sort(delegate(Product p1, Product p2)
            //{
            //    if (p1.Cost < p2.Cost) return -1;
            //    if (p1.Cost == p2.Cost) return 0;
            //    return 1;
            //});
            //products.Sort((p1, p2) =>
            //{
            //    if (p1.Cost < p2.Cost) return -1;
            //    if (p1.Cost == p2.Cost) return 0;
            //    return 1;
            //});
            //products.Sort((p1, p2) =>
            //{
            //    return Math.Sign(p1.Cost - p2.Cost);
            //});
            //products.Sort((p1, p2) => Math.Sign(p1.Cost - p2.Cost));
            products.Sort(ProductSortOrder.ByCost);

            foreach (var product in products)
                Console.WriteLine(product);

            Console.ReadLine();
        }
        //public static int CompareProductByCost(Product p1, Product p2) {
        //    if (p1.Cost < p2.Cost) return -1;
        //    if (p1.Cost == p2.Cost) return 0;
        //    return 1;
        //}
    }

    public static class ProductSortOrder
    {
        /*public static CompareProductDelegate ById = (p1, p2) => Math.Sign(p1.Id - p2.Id);
        public static CompareProductDelegate ByUnits = (p1, p2) => Math.Sign(p1.Units - p2.Units);
        public static CompareProductDelegate ByCost = (p1, p2) => Math.Sign(p1.Cost - p2.Cost);*/

        public static CompareDelegate<Product> ById = (p1, p2) => Math.Sign(p1.Id - p2.Id);
        public static CompareDelegate<Product> ByUnits = (p1, p2) => Math.Sign(p1.Units - p2.Units);
        public static CompareDelegate<Product> ByCost = (p1, p2) => Math.Sign(p1.Cost - p2.Cost);
    }

    public delegate void FnBlockDelegate();

    public delegate void FnBlockDelegateForProduct(Product product);
    public delegate void FnBlockDelegate<in T>(T item);
    class Utils
    {
        public static void Print(string title, FnBlockDelegate block)
        {
            Console.WriteLine(title);
            Console.WriteLine("===============================");
            block();
            Console.WriteLine();

        }

    }
}
