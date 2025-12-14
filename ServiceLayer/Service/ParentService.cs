using RepositoryLayer.AppDbContext;
using RepositoryLayer.Entity;
using RepositoryLayer.RepositoryInterface;
using ServiceLayer.DTO;
using ServiceLayer.DTO.RequestDTO;
using ServiceLayer.ServiceInterFace;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class ParentService : IParentService
    {
        private readonly IParentRepository _parentRepository;
        private readonly IAuthRepository _authRepo;
        private readonly IEmailService _email;

        public ParentService(
            IParentRepository parentRepository,
            IAuthRepository authRepo,
            IEmailService email)
        {
            _parentRepository = parentRepository;
            _authRepo = authRepo;
            _email = email;
        }

        public async Task<Parent> AddParentAsync(ParentRequestDTO dto)
        {
            var (user, password) =
                await _authRepo.CreateUserAuto(dto.Email, "Parent");

            var parent = new Parent
            {
                ParentName = dto.ParentName,
                PhoneNo = dto.PhoneNo,
                Address = dto.Address,
                Email = dto.Email,
                StudentID = dto.StudentID,
                UserID = user.UserID
            };

            await _parentRepository.AddParent(parent); // ✅ repository

            await _email.SendEmailAsync(new EmailDTO
            {
                To = dto.Email,
                Subject = "SMART SMS - Parent Login",
                Body = $"Username: {user.UserName}, Password: {password}"
            });

            return parent;
        }




        public async Task<IEnumerable<Parent>> GetAllParentsAsync()
        {
            return await _parentRepository.GetAllParents();
        }

        public async Task<Parent?> GetParentByIdAsync(Guid id)
        {
            return await _parentRepository.GetParentById(id);
        }

        public async Task<Parent?> UpdateParentAsync(Guid id, ParentRequestDTO request)
        {
            var existing = await _parentRepository.GetParentById(id);
            if (existing == null) return null;

            existing.ParentName = request.ParentName;
            existing.PhoneNo = request.PhoneNo;
            existing.Address = request.Address;
            existing.Email = request.Email;
            existing.StudentID = request.StudentID;

            // ❌ DO NOT CHANGE UserID
            return await _parentRepository.UpdateParent(existing);
        }


        public async Task<bool> DeleteParentAsync(Guid id)
        {
            return await _parentRepository.DeleteParent(id);
        }

       
    }
}
