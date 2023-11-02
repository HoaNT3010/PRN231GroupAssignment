using Application.Services.Interfaces;
using Domain.Enums;
using Infrastructure.DTOs.Response;
using Infrastructure.DTOs.Response.Invoice;
using Infrastructure.DTOs.Response.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Operations related to customer's card
    /// </summary>
    [Route("api/v1/cards")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ILogger<CardsController> logger;
        private readonly ICardService cardService;

        public CardsController(ILogger<CardsController> logger, ICardService cardService)
        {
            this.logger = logger;
            this.cardService = cardService;
        }

        /// <summary>
        /// Get a list contains all wallets of a card based on given card's Id
        /// </summary>
        /// <param name="id">Id of card</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("{id}/wallets")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<List<WalletResponse>>))]
        public async Task<ActionResult<ResponseObject<List<WalletResponse>>>> GetCardWallets([FromRoute] int id)
        {
            var result = await cardService.GetCardWallets(id);
            return Ok(new ResponseObject<List<WalletResponse>>()
            {
                Status = ResponseStatus.Success.ToString(),
                Message = "Successfully retrieved list of card's wallet(s)",
                Data = result
            });
        }
    }
}
