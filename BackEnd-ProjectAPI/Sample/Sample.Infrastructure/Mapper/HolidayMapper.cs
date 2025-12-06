using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class HolidayMapper
    {
        public static entityframework.Holiday MapToEntityFramework(entities.Holiday entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Holiday entity cannot be null.");

            var mapped = new entityframework.Holiday
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                HolidayName = entity.HolidayName ?? string.Empty,
                HolidayDate = entity.HolidayDate,
                CompanyId = entity.CompanyId,
                BranchId = entity.BranchId,
                IsRecurring = entity.IsRecurring,
                Description = entity.Description,
                CreatedDateTime = entity.CreatedDateTime,
                ModifiedDateTime = entity.ModifiedDateTime,
                //Company
                //Branch
            };

            return mapped;
        }

        public static entities.Holiday MapToEntity(entityframework.Holiday holiday)
        {
            if (holiday == null) return null!;

            return new entities.Holiday
            {
                id = holiday.Id,         // default = 0 kung int
                HolidayName = holiday.HolidayName ?? string.Empty,
                HolidayDate = holiday.HolidayDate,
                CompanyId = holiday.CompanyId,
                BranchId = holiday.BranchId,
                IsRecurring = holiday.IsRecurring,
                Description = holiday.Description,
                CreatedDateTime = holiday.CreatedDateTime,
                ModifiedDateTime = holiday.ModifiedDateTime,
                //Company
                //Branch
            };
        }
    }
}
