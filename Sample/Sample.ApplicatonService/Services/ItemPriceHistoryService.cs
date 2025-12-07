using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.Services
{
    public class ItemPriceHistoryService : IItemPriceHistoryService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ItemPriceHistoryService));

        public ItemPriceHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemPriceHistoryDto>> GetAll()
        {
            try
            {
                var itemPriceHistory = await _unitOfWork.ItemPriceHistoryRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.ItemPriceHistory>, IEnumerable<ItemPriceHistoryDto>>(itemPriceHistory);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<ItemPriceHistoryDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.ItemPriceHistoryRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<ItemPriceHistoryDto>>(result.Data);

                return new PagedResponse<IEnumerable<ItemPriceHistoryDto>>
                {
                    Data = dtoData,
                    PageNumber = result.PageNumber,
                    PageSize = result.PageSize,
                    TotalRecords = result.TotalRecords,
                    TotalPages = result.TotalPages
                };
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAllPaged", ex);
                throw;
            }
        }

        public async Task<ItemPriceHistoryDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.ItemPriceHistory, ItemPriceHistoryDto>(await _unitOfWork.ItemPriceHistoryRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateItemPriceHistory(ItemPriceHistoryDto itemPriceHistoryDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var itemPriceHistory = Domain.Entities.ItemPriceHistory.Create(
                    _mapper.Map<ItemPriceHistoryDto, Domain.Entities.ItemPriceHistory>(itemPriceHistoryDto), createdBy);

                await _unitOfWork.ItemPriceHistoryRepository.AddAsync(itemPriceHistory);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateItemPriceHistory", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateItemPriceHistory(ItemPriceHistoryDto itemPriceHistoryDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var itemPriceHistory = Domain.Entities.ItemPriceHistory.Update(
                    _mapper.Map<ItemPriceHistoryDto, Domain.Entities.ItemPriceHistory>(itemPriceHistoryDto), editedBy);

                await _unitOfWork.ItemPriceHistoryRepository.UpdateAsync(itemPriceHistory);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateItemPriceHistory", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ItemPriceHistoryDto>> SearchItemPriceHistorysByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var itemPriceHistory = await _unitOfWork.ItemPriceHistoryRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like(e.ItemId.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.BranchId.ToString().ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<ItemPriceHistoryDto>>(itemPriceHistory);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchItemPriceHistorysByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
