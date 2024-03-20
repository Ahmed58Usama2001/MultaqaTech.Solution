using MultaqaTech.Core.Entities.ZoomDomainEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultaqaTech.Core.Services.Contract.ZoomMeetingContracts
{
    public interface IZoomMeetingCategoryService
    {
        Task<ZoomMeetingCategory?> CreateZoomMeetingCategoryAsync(ZoomMeetingCategory category);
        
     // Task<IReadOnlyList<ZoomMeetingCategory>> ReadAllAsync();

        Task<ZoomMeetingCategory?> ReadByIdAsync(int categoryId);

        Task<ZoomMeetingCategory?> UpdateZoomMeetingCategory(int categoryId, ZoomMeetingCategory category);

        Task<bool> DeleteZoomMeetingCategory(int categoryId);
    }
}
