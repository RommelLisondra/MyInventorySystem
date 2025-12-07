using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IExpenseCategoryService : IDisposable
    {
        Task<IEnumerable<ExpenseCategoryDto>> GetAll();
        Task<PagedResponse<IEnumerable<ExpenseCategoryDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<ExpenseCategoryDto> GetById(int id);
        Task CreateExpenseCategory(ExpenseCategoryDto ExpenseCategoryDto, string createdBy);
        Task UpdateExpenseCategory(ExpenseCategoryDto ExpenseCategoryDto, string editedBy);
        Task<IEnumerable<ExpenseCategoryDto>> SearchExpenseCategorysByKeywordAsync(string? keyword);
    }
}
