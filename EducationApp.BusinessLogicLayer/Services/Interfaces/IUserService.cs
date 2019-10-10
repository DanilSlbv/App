using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.BusinessLogicLayer.Models.Response;
using EducationApp.BusinessLogicLayer.Models.User;
namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseModel<UserModelItem>> GetAllAsync(int page);
        Task<UserModelItem> GetByIdAsync(string id);
        Task<UserModelItem> GetByEmailAsync(string userEmail);
        Task<BaseModel> RemoveAsync(string id);
        Task<BaseModel> EditAsync(UserModelItem userEditModel);
    }
}
