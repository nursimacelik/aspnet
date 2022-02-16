using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.Core.Shared
{
    public interface ICRUDService<PK, MainDto, CreateDto, UpdateDto, ApplicationUser>
    {
        Task<ApplicationResult<MainDto>> Get(PK id, ApplicationUser applicationUser);
        Task<ApplicationResult<List<MainDto>>> GetAll(ApplicationUser applicationUser);
        Task<ApplicationResult<MainDto>> Create(CreateDto input, ApplicationUser applicationUser);
        Task<ApplicationResult<MainDto>> Update(UpdateDto input, ApplicationUser applicationUser);
        Task<ApplicationResult> Delete(PK id, ApplicationUser applicationUser);
    }
}