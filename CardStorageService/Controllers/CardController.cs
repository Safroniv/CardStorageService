using CardStorageService.Data;
using CardStorageService.Models;
using CardStorageService.Models.Requests;
using CardStorageService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardStorageService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        #region Services

        private readonly ILogger<CardController> _logger;
        private readonly ICardRepositoryService _cardRepositoryService;

        #endregion

        #region Constructor

        public CardController(ILogger<CardController> logger,
            ICardRepositoryService cardRepositoryService)
        {
            _logger = logger;
            _cardRepositoryService = cardRepositoryService;
        }

        #endregion
        [HttpPost("create")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateCardRequest request)
        {
            try
            {
                var cardId = _cardRepositoryService.Create(new Card
                {
                    ClientId =request.ClientId,
                    CardNo = request.CardNo,
                    ExpDate = request.ExpDate,
                    CVV2 = request.CVV2
                });
                return Ok(new CreateCardResponse
                {
                        CardId = cardId.ToString()
                });
            }
            catch (Exception e) 
            {
                _logger.LogError(e, "Get cards error.");
                return Ok(new GetCardResponce
                { 
                    ErrorCode = 1012,
                    ErrorMessage = "Get cards error."
                });
            }
        }

        [HttpGet("get-by-client-id")]
        [ProducesResponseType(typeof(GetCardResponce), StatusCodes.Status200OK)]

        public IActionResult GetByClientId([FromQuery] string clientId)
        {
            try
            {
                var cards = _cardRepositoryService.GetByClientId(clientId);
                return Ok(new GetCardResponce
                {
                    Cards = cards.Select(card => new CardDto
                    {
                        CardNo = card.CardNo,
                        CVV2 = card.CVV2,
                        Name = card.Name,
                        ExpDate = card.ExpDate.ToString("MM/yy"),
                    }).ToList()
                });
            }
            catch (Exception e) 
            {
                _logger.LogError(e, "Get cards error.");
                return Ok(new GetCardResponce
                { ErrorCode = 1013,
                ErrorMessage = "Get cards error."
                });
            }
        }

    }
}
