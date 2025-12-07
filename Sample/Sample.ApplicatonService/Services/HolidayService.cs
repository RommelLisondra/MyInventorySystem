using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class HolidayService : IHolidayService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(HolidayService));

        public HolidayService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HolidayDto>> GetAll()
        {
            try
            {
                var holiday = await _unitOfWork.HolidayRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.Holiday>, IEnumerable<HolidayDto>>(holiday);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<HolidayDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.HolidayRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<HolidayDto>>(result.Data);

                return new PagedResponse<IEnumerable<HolidayDto>>
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

        public async Task<HolidayDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.Holiday, HolidayDto>(await _unitOfWork.HolidayRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateHoliday(HolidayDto holidayDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var holiday = Domain.Entities.Holiday.Create(
                    _mapper.Map<HolidayDto, Domain.Entities.Holiday>(holidayDto), createdBy);

                await _unitOfWork.HolidayRepository.AddAsync(holiday);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateHoliday", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateHoliday(HolidayDto holidayDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var holiday = Domain.Entities.Holiday.Update(
                    _mapper.Map<HolidayDto, Domain.Entities.Holiday>(holidayDto), editedBy);

                await _unitOfWork.HolidayRepository.UpdateAsync(holiday);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateHoliday", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<HolidayDto>> SearchHolidaysByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var holiday = await _unitOfWork.HolidayRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.HolidayName ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Description ?? string.Empty).ToLower(), $"%{keyword}%") 
                );

                return _mapper.Map<IEnumerable<HolidayDto>>(holiday);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchHolidaysByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
