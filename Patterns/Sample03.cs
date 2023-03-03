using System.Reflection;

namespace Patterns
{
    internal class Sample03
    {
        static void Main(string[] args)
        {
            var obj1 = ProductFactory.Create<SampleProduct1>();
            var obj2 = ProductFactory.Create<SampleProduct2>();

            //ProductFactory.Create(typeof(SampleProduct2));

            Console.ReadKey();
        }
    }
    public abstract class Product
    {
        public abstract void PostConstructor();

    }
    public class SampleProduct1 : Product
    {
        public SampleProduct1() { }
        public override void PostConstructor()
        {
            Console.WriteLine("Product1 creted.");
        }
    }
    public class SampleProduct2 : Product
    {
        public SampleProduct2() { }
        public override void PostConstructor()
        {
            Console.WriteLine("Product2 creted.");
        }
    }
    public static class ProductFactory
    {

        public static T Create<T>() where T : Product, new()
        {
            var t = new T();
            t.PostConstructor();
            return t;
        }

    }

    //public static Product Create(Type t)
    //{

    //    var assembly = Assembly.GetAssembly(typeof(Sample03));
    //    return (Product)assembly.CreateInstance(t.FullName);
    //}

}
