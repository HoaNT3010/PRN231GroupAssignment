using Application.Services.Interfaces;
using Domain.Enums;
using Infrastructure.DTOs.Request.Wallet;
using Infrastructure.DTOs.Response;
using Infrastructure.DTOs.Response.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Operations related to wallets
    /// </summary>
    [Route("api/v1/wallets")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly ILogger<WalletsController> logger;
        private readonly IWalletService walletService;

        public WalletsController(ILogger<WalletsController> logger, IWalletService walletService)
        {
            this.logger = logger;
            this.walletService = walletService;
        }

        /// <summary>
        /// Recharged a wallet balance by cash with a specific amount of money
        /// </summary>
        /// <param name="walletId">Id of wallet</param>
        /// <param name="cashRequest">Data for recharge request</param>
        /// <returns></returns>
        [HttpPost]
        [Route("recharge/{walletId}/cash")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<WalletRechargeBalanceResponse>))]
        public async Task<ActionResult<ResponseObject<WalletRechargeBalanceResponse>>> RechargeWalletBalanceWithCash([FromRoute] int walletId, [FromBody] WalletBalanceRechargeCashRequest cashRequest)
        {
            logger.LogInformation("Process recharge wallet [Id: {walletId}] balance with cash", walletId);
            var result = await walletService.RechargeBalanceWithCash(walletId, cashRequest);
            return Ok(new ResponseObject<WalletRechargeBalanceResponse>()
            {
                Status = ResponseStatus.Success.ToString(),
                Message = $"Wallet balance has been recharged",
                Data = result
            });
        }

    }
}
