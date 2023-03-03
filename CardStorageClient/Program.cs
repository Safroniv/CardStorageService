using CardStorageServiceProtos;
using Grpc.Net.Client;
using static CardStorageServiceProtos.CardService;
using static CardStorageServiceProtos.ClientService;

namespace CardStorageClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppContext.SetSwitch("System.Net.Http.SocketHttpHandler.Http2UnencryptedSupport", true);

            //CardServiceClient
            //ClientServiceClient

            using var channel = GrpcChannel.ForAddress("http://localhost:5001");

            ClientServiceClient clientService = new (channel);

            var createClientResponce = clientService.Create(new CardStorageServiceProtos.CreateClientRequest
            {
                FirstName = "Иван",
                SurName = "Сафронов",
                Patronymic = "TEST"

            });

            Console.WriteLine($"Client {createClientResponce.ClientId} created successfully.");

            CardServiceClient cardService = new (channel);

            var getByClientIdResponce = cardService.GetByClientId(new CardStorageServiceProtos.GetByClientIdRequest
            {
                ClientId = 1
            });

            foreach(var card in getByClientIdResponce.Cards) 
            {
                Console.WriteLine($"{card.CardNo}; {card.Name}; {card.CVV2}; {card.ExpDate}");
            }
            
            Console.ReadLine();
        }
    }
}