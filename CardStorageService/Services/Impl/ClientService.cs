using CardStorageServiceProtos;
using Grpc.Core;
using static CardStorageServiceProtos.ClientService;

namespace CardStorageService.Services.Impl
{
    public class ClientService : ClientServiceBase
    {
        #region Services

        private readonly IClientRepositoryService _clientRepositoryService;
        private readonly ILogger<ClientRepository> _logger;

        #endregion


        public ClientService(ILogger<ClientRepository> logger, IClientRepositoryService clientRepositoryService) 
        {
            _clientRepositoryService = clientRepositoryService;
            _logger = logger;
        }

        public override Task<CreateClientResponce> Create(CreateClientRequest request, ServerCallContext context)
        {
            try
            { 
            var cliendId = _clientRepositoryService.Create(new Data.Client
            {
                FirstName = request.FirstName,
                SurName = request.SurName,
                Patronumic = request.Patronymic
            });

            var responce2 = new CreateClientResponce
            {
                ClientId = cliendId,
                ErrorCode = 0,
                ErrorMessage = String.Empty,

            };

            return Task.FromResult(responce2);
        }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create client error.");
                var responce2 = new CreateClientResponce
                {
                    ClientId = -1,
                    ErrorCode = 912,
                    ErrorMessage = "Create client error.",
                };
                return Task.FromResult(responce2);
            }
            
        }

    }
}
