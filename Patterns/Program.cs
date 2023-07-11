namespace Patterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var obj = SimpeSingleton.Instance;
            var obj2 = SimpeSingleton.Instance;

            obj2.Counter++;

            // SimpeSingleton simpeSingleton = new SimpeSingleton();

        }
    }

    class SimpeSingleton
    {
        private static SimpeSingleton _instance;

        SimpeSingleton()
        {

        }

        public int Counter { get; set; } = 1;

        public static SimpeSingleton Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SimpeSingleton();
                return _instance;
            }
        }
    }
}