using BlogSystem.Dtos;
using BlogSystem.IBLL;
using BlogSystem.IDAL;
using BlogSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BLL
{
    public class AdminsBll : IAdminsBll
    {
        private IAdminsDal _dal;

        public AdminsBll(IAdminsDal dal)
        {
            _dal = dal;
        }

        public async Task<int> AddAdminsAsync(string email, string password, string nickname, string photo, string images, Guid rolesId)
        {
            return await _dal.AddAsync(new Admins()
            {
                Email = email,
                Password = password,
                NickName = nickname,
                Photo = photo,
                Images = images,
                RolesId = rolesId
            });
        }

        public async Task<int> DeleteAdminsAsync(Guid id)
        {
            var data = await _dal.QueryAsync(id);
            if (data == null) return -2;
            return await _dal.DeleteAsync(data);
        }

        public async Task<int> EditAdminsAsync(Guid id, string email, string password, string nickname, string photo, string images, Guid rolesId)
        {
            var data = await _dal.QueryAsync(id);
            if (data == null) return -2;
            data.Email = email;
            data.Password = password;
            data.NickName = nickname;
            if (images != null)
            {
                data.Photo = photo;
                data.Images = images;
            }
            data.RolesId = rolesId;
            data.UpdateTime = DateTime.Now;
            return await _dal.EditAsync(data);

        }

        public async Task<AdminsDto> GetAdminsByEmail(string email)
        {
            return await _dal
                .Query(r => r.Email.Contains(email))
                .Select(r => new AdminsDto()
                {
                    Id = r.Id,
                    Email = r.Email,
                    Password = r.Password,
                    NickName = r.NickName,
                    RolesId = r.RolesId,
                    Photo = r.Photo,
                    Images = r.Images,
                    UpdateTime = r.UpdateTime
                })
                .FirstOrDefaultAsync();
        }

        public async Task<AdminsDto> GetAdminsById(Guid id)
        {
            var data = await _dal.QueryAsync(id);
            if (data != null)
            {
                return new AdminsDto()
                {
                    Id = data.Id,
                    Email = data.Email,
                    Password = data.Password,
                    NickName = data.NickName,
                    Photo = data.Photo,
                    Images = data.Images,
                    RolesId = data.RolesId,
                    UpdateTime = data.UpdateTime
                };
            }

            return null;

        }

        public async Task<List<AdminsDto>> GetAdminsByNickName(string nickName)
        {
            return await _dal
                .Query(r => r.NickName.Contains(nickName))
                .OrderByDescending(r => r.UpdateTime)
                .Select(r => new AdminsDto()
                {
                    Id = r.Id,
                    Email = r.Email,
                    Password = r.Password,
                    NickName = r.NickName,
                    RolesId = r.RolesId,
                    Photo = r.Photo,
                    Images = r.Images,
                    UpdateTime = r.UpdateTime
                })
                .ToListAsync();
        }

        public async Task<List<AdminsDto>> GetAdminsByRoles(Guid rId)
        {
            return await _dal
               .Query(r => r.RolesId == rId)
               .OrderByDescending(r => r.UpdateTime)
               .Select(r => new AdminsDto()
               {
                   Id = r.Id,
                   Email = r.Email,
                   Password = r.Password,
                   NickName = r.NickName,
                   RolesId = r.RolesId,
                   Photo = r.Photo,
                   Images = r.Images,
                   UpdateTime = r.UpdateTime
               })
               .ToListAsync();
        }

        public async Task<List<AdminsDto>> GetAllAdminsAsync()
        {
            return await _dal.Query()
                .Select(r => new AdminsDto()
                {
                    Id = r.Id,
                    Email = r.Email,
                    Password = r.Password,
                    NickName = r.NickName,
                    RolesId = r.RolesId,
                    Photo = r.Photo,
                    Images = r.Images,
                    UpdateTime = r.UpdateTime
                }).ToListAsync();
        }

        public async Task<bool> IsExistsAsync(string email)
        {
            return await _dal.IsExistsAsync(a => a.Email.Equals(email));
        }

        public async Task<AdminsDto> LoginAsync(string email, string password)
        {
            return await _dal
                 .Query(m => m.Email.Equals(email) && m.Password.Equals(password))
                 .Select(r => new AdminsDto()
                 {
                     Id = r.Id,
                     Email = r.Email,
                     Password = r.Password,
                     NickName = r.NickName,
                     RolesId = r.RolesId,
                     Photo = r.Photo,
                     Images = r.Images,
                     UpdateTime = r.UpdateTime
                 }).FirstOrDefaultAsync();
        }
    }
}
