using Application.ErrorHandlers;
using Application.Services.Interfaces;
using AutoMapper;
using Infrastructure.Data;
using Infrastructure.DTOs.Response.Wallet;
using Microsoft.Extensions.Logging;

namespace Application.Services.Implementations
{
    public class CardService : ICardService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<CardService> logger;
        private readonly IMapper mapper;

        public CardService(IUnitOfWork unitOfWork, ILogger<CardService> logger, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<List<WalletResponse>> GetCardWallets(int cardId)
        {
            if (cardId <= 0)
            {
                throw new BadRequestException("Invalid card's Id");
            }
            var card = await unitOfWork.CardRepository.GetCardWithWallets(cardId);
            if(card == null)
            {
                throw new NotFoundException($"Cannot find card with Id {cardId}");
            }
            return mapper.Map<List<WalletResponse>>(card.Wallets);
        }
    }
}
