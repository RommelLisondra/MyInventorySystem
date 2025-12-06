using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class InventoryBalanceService : IInventoryBalanceService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(InventoryBalanceService));

        public InventoryBalanceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InventoryBalanceDto>> GetAll()
        {
            try
            {
                var inventoryBalance = await _unitOfWork.InventoryBalanceRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.InventoryBalance>, IEnumerable<InventoryBalanceDto>>(inventoryBalance);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<InventoryBalanceDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.InventoryBalanceRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<InventoryBalanceDto>>(result.Data);

                return new PagedResponse<IEnumerable<InventoryBalanceDto>>
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

        public async Task<InventoryBalanceDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.InventoryBalance, InventoryBalanceDto>(await _unitOfWork.InventoryBalanceRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateInventoryBalance(InventoryBalanceDto inventoryBalanceDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var inventoryBalance = Domain.Entities.InventoryBalance.Create(
                    _mapper.Map<InventoryBalanceDto, Domain.Entities.InventoryBalance>(inventoryBalanceDto), createdBy);

                await _unitOfWork.InventoryBalanceRepository.AddAsync(inventoryBalance);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateInventoryBalance", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateInventoryBalance(InventoryBalanceDto inventoryBalanceDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var inventoryBalance = Domain.Entities.InventoryBalance.Update(
                    _mapper.Map<InventoryBalanceDto, Domain.Entities.InventoryBalance>(inventoryBalanceDto), editedBy);

                await _unitOfWork.InventoryBalanceRepository.UpdateAsync(inventoryBalance);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateInventoryBalance", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<InventoryBalanceDto>> SearchInventoryBalancesByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var inventoryBalance = await _unitOfWork.InventoryBalanceRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.ItemDetailNo ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.WarehouseId.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.RecStatus ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<InventoryBalanceDto>>(inventoryBalance);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchInventoryBalancesByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
