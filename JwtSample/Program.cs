namespace JwtSample
{
    internal class Program
    {
        //Https://jwt.io/
        static void Main(string[] args)
        {
            Console.WriteLine("Enter user name:");
            string userName = Console.ReadLine();
            Console.WriteLine("Enter user namepassword");
            string userPassword = Console.ReadLine();
            UserService userService = new UserService();
            string token = userService.Authentication(userName, userPassword);
            Console.WriteLine(token);
            Console.ReadKey(true);
        }
    }
}