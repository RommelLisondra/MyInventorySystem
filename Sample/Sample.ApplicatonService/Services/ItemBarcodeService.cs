using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class ItemBarcodeService : IItemBarcodeService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ItemBarcodeService));

        public ItemBarcodeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemBarcodeDto>> GetAll()
        {
            try
            {
                var itemBarcode = await _unitOfWork.ItemBarcodeRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.ItemBarcode>, IEnumerable<ItemBarcodeDto>>(itemBarcode);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<ItemBarcodeDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.ItemBarcodeRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<ItemBarcodeDto>>(result.Data);

                return new PagedResponse<IEnumerable<ItemBarcodeDto>>
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

        public async Task<ItemBarcodeDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.ItemBarcode, ItemBarcodeDto>(await _unitOfWork.ItemBarcodeRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateItemBarcode(ItemBarcodeDto itemBarcodeDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var itemBarcode = Domain.Entities.ItemBarcode.Create(
                    _mapper.Map<ItemBarcodeDto, Domain.Entities.ItemBarcode>(itemBarcodeDto), createdBy);

                await _unitOfWork.ItemBarcodeRepository.AddAsync(itemBarcode);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateItemBarcode", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateItemBarcode(ItemBarcodeDto itemBarcodeDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var itemBarcode = Domain.Entities.ItemBarcode.Update(
                    _mapper.Map<ItemBarcodeDto, Domain.Entities.ItemBarcode>(itemBarcodeDto), editedBy);

                await _unitOfWork.ItemBarcodeRepository.UpdateAsync(itemBarcode);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateItemBarcode", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ItemBarcodeDto>> SearchItemBarcodesByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var itemBarcode = await _unitOfWork.ItemBarcodeRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.Barcode ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Description ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<ItemBarcodeDto>>(itemBarcode);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchItemBarcodesByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
