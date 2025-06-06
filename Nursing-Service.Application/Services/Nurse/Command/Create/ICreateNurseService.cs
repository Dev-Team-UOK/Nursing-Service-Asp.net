using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;
using Nursing_Service.Common.Extensions;
using Nursing_Service.Domain.Entities.Nurse;

namespace Nursing_Service.Application.Services.Nurse.Command.Create
{
    public interface ICreateNurseService
    {
        Task<BaseResultDTO<CreateNurseResultDTO>> ExcuteAsync(CreateNurseRequestInfo req);
    }

    public class CreateNurseService : ICreateNurseService
    {
        private IDataBaseContext _context;

        public CreateNurseService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO<CreateNurseResultDTO>> ExcuteAsync(CreateNurseRequestInfo req)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(req.NurseNumber))
                    throw new NotImplementedException("شماره پرستاری نمیتواند خالی باشد");
                if (req.DoService.Any() is false)
                    throw new NotImplementedException("پرستار میبایست حتما سرویسی ارائه دهد.");
                
                var passHasher = new PasswordHasher();
                
                var nurse = new Domain.Entities.Nurse.Nurse
                {
                    Email = req.Email,
                    Password = passHasher.HashPassword(req.Password),
                    PhoneNumber = req.PhoneNumber,
                    UserName = req.UserName,
                    CreatedDateTime = DateTime.Now,
                    FirstName = req.FirstName,
                    LastName = req.LastName,
                    NurseNumber = req.NurseNumber,
                    StartWorkingInCompany = req.StartWorkingInCompany,
                    SuperVisorId = req.SuperVisorId,
                    WorkHistoryInYear = req.WorkHistoryInYear,
                    Role = Domain.Entities.User.RoleEnum.Nurse
                };

                await _context.Nurses.AddAsync(nurse);

                foreach (var serviceId in req.DoService)
                {
                    await _context.NurseCanDoService.AddAsync(new NurseCanDoService
                    {
                        NurseId = nurse.Id,
                        ServiceId = serviceId
                    });
                }

                await _context.SaveChangesAsync();

                return new BaseResultDTO<CreateNurseResultDTO>
                {
                    IsSuccess = true,
                    Message = "پرستار با موفقیت ایجاد شد.",
                    Data = new CreateNurseResultDTO
                    { 
                         Id = nurse.Id,
                    }
                };
            }
            catch (Exception ex)
            {
                return new BaseResultDTO<CreateNurseResultDTO>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}
