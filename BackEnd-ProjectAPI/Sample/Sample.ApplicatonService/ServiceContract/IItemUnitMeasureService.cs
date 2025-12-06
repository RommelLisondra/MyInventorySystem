using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IItemUnitMeasureService : IDisposable
    {
        Task<IEnumerable<ItemUnitMeasureDto>> GetAll();
        Task<ItemUnitMeasureDto> GetById(int id);
        Task CreateItemUnitMeasure(ItemUnitMeasureDto ItemUnitMeasureDto, string createdBy);
        Task UpdateItemUnitMeasure(ItemUnitMeasureDto ItemUnitMeasureDto, string editedBy);
        Task<IEnumerable<ItemUnitMeasureDto>> GetItemUnitMeasureByunitCodeAsync(string unitCode);
        Task<ItemUnitMeasureDto?> GetItemUnitMeasureByIdAsync(int id);
        Task<IEnumerable<ItemUnitMeasureDto>> SearchItemUnitMeasureAsync(string? keyword);
        Task<PagedResponse<IEnumerable<ItemUnitMeasureDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
