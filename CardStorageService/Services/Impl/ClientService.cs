using CardStorageServiceProtos;
using Grpc.Core;
using static CardStorageServiceProtos.ClientService;

namespace CardStorageService.Services.Impl
{
    public class ClientService : ClientServiceBase
    {
        #region Services

        private readonly IClientRepositoryService _clientRepositoryService;

        #endregion


        public ClientService(IClientRepositoryService clientRepositoryService) 
        {
            _clientRepositoryService = clientRepositoryService;
        }

        public override Task<CreateClientResponce> Create(CreateClientRequest request, ServerCallContext context)
        {
            var cliendId = _clientRepositoryService.Create(new Data.Client
            {
                FirstName = request.FirstName,
                SurName = request.SurName,
                Patronumic = request.Patronymic
            });

            var responce = new CreateClientResponce
            {
                ClientId = cliendId
            };



            return Task.FromResult(responce);
        }

    }
}
