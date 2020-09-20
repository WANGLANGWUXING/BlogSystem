using BlogSystem.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.IBLL
{
    public interface IAdminsBll
    {
        Task<int> AddAdminsAsync(string email, string password, string nickname, string photo,string image, Guid rolesId);


        Task<int> EditAdminsAsync(Guid id, string email, string password, string nickname, string photo,string image, Guid rolesId);

        Task<int> DeleteAdminsAsync(Guid id);

        Task<AdminsDto> LoginAsync(string email, string password);

        Task<List<AdminsDto>> GetAllAdminsAsync();

        Task<List<AdminsDto>> GetAdminsByNickName(string nickName);

        Task<bool> IsExistsAsync(string email);

        Task<AdminsDto> GetAdminsByEmail(string email);

        Task<AdminsDto> GetAdminsById(Guid id);

        Task<List<AdminsDto>> GetAdminsByRoles(Guid rId);



    }
}
