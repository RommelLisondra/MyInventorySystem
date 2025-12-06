using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.ServiceContract;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class WarehouseService : IWarehouseService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(WarehouseService));

        public WarehouseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WarehouseDto>> GetAll()
        {
            try
            {
                var warehouses = await _unitOfWork.WarehouseRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.Warehouse>, IEnumerable<WarehouseDto>>(warehouses);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }
        public async Task<PagedResponse<IEnumerable<WarehouseDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.WarehouseRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<WarehouseDto>>(result.Data);

                return new PagedResponse<IEnumerable<WarehouseDto>>
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

        public async Task<WarehouseDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.Warehouse, WarehouseDto>(await _unitOfWork.WarehouseRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateWarehouse(WarehouseDto warehouseDto, string createdBy)
        {
            try
            {
                var warehouses = Domain.Entities.Warehouse.Create(
                    _mapper.Map<WarehouseDto, Domain.Entities.Warehouse>(warehouseDto), createdBy);

                await _unitOfWork.WarehouseRepository.AddAsync(warehouses);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateWarehouse", ex);
                throw;
            }
        }

        public async Task UpdateWarehouse(WarehouseDto warehouseDto, string editedBy)
        {
            try
            {
                var warehouses = Domain.Entities.Warehouse.Update(
                    _mapper.Map<WarehouseDto, Domain.Entities.Warehouse>(warehouseDto), editedBy);

                await _unitOfWork.WarehouseRepository.UpdateAsync(warehouses);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateWarehouse", ex);
                throw;
            }
        }

        public async Task<IEnumerable<WarehouseDto>> GetWarehousesByCustNoAsync(string warehouseCode)
        {
            try
            {
                var warehouses = await _unitOfWork.WarehouseRepository.FindAsync(e => e.WareHouseCode == warehouseCode);
                return _mapper.Map<IEnumerable<WarehouseDto>>(warehouses);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetWarehousesByCustNoAsync", ex);
                throw;
            }
        }

        public async Task<WarehouseDto?> GetWarehouseByIdAsync(int id)
        {
            try
            {
                var warehouses = await _unitOfWork.WarehouseRepository.FindAsync(e => e.id == id);
                var warehouse = warehouses.FirstOrDefault();
                return warehouse == null ? null : _mapper.Map<WarehouseDto>(warehouse);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetWarehouseByIdAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<WarehouseDto>> SearchWarehousesAsync(string? keyword)
        {
            try
            {

                keyword = keyword?.ToLower() ?? string.Empty;

                var warehouses = await _unitOfWork.WarehouseRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.WareHouseCode ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Name ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<WarehouseDto>>(warehouses);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchWarehousesAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
