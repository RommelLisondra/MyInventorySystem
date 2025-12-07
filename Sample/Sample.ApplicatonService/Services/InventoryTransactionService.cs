using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class InventoryTransactionService : IInventoryTransactionService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(InventoryTransactionService));

        public InventoryTransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InventoryTransactionDto>> GetAll()
        {
            try
            {
                var inventoryTransaction = await _unitOfWork.InventoryTransactionRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.InventoryTransaction>, IEnumerable<InventoryTransactionDto>>(inventoryTransaction);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<InventoryTransactionDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.InventoryTransactionRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<InventoryTransactionDto>>(result.Data);

                return new PagedResponse<IEnumerable<InventoryTransactionDto>>
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

        public async Task<InventoryTransactionDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.InventoryTransaction, InventoryTransactionDto>(await _unitOfWork.InventoryTransactionRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateInventoryTransaction(InventoryTransactionDto inventoryTransactionDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var inventoryTransaction = Domain.Entities.InventoryTransaction.Create(
                    _mapper.Map<InventoryTransactionDto, Domain.Entities.InventoryTransaction>(inventoryTransactionDto), createdBy);

                await _unitOfWork.InventoryTransactionRepository.AddAsync(inventoryTransaction);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateInventoryTransaction", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateInventoryTransaction(InventoryTransactionDto InventoryTransactionDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var inventoryTransaction = Domain.Entities.InventoryTransaction.Update(
                    _mapper.Map<InventoryTransactionDto, Domain.Entities.InventoryTransaction>(InventoryTransactionDto), editedBy);

                await _unitOfWork.InventoryTransactionRepository.UpdateAsync(inventoryTransaction);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateInventoryTransaction", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<InventoryTransactionDto>> SearchInventoryTransactionsByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var inventoryTransaction = await _unitOfWork.InventoryTransactionRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.ItemDetailNo ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.WarehouseId.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.RecStatus ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<InventoryTransactionDto>>(inventoryTransaction);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchInventoryTransactionsByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
