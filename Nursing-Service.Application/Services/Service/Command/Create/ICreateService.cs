using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;
using Nursing_Service.Domain.Entities.Service;

namespace Nursing_Service.Application.Services.Service.Command.Create
{
    public interface ICreateService
    {
        Task<BaseResultDTO<CreateServiceResultDTO>> ExcuteAsync(CreateServiceRequestInfo req);
    }

    public class CreateService : ICreateService
    {
        private IDataBaseContext _context;

        public CreateService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO<CreateServiceResultDTO>> ExcuteAsync(CreateServiceRequestInfo req)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(req.Name))
                    throw new NotImplementedException("نام سرویس نمیتواند خالی باشد.");
                if (req.Cost == 0)
                    throw new NotImplementedException("هزینه سرویس نمیتواند 0 باشد.");

                var service = new Domain.Entities.Service.Service
                {
                    Name = req.Name,
                    Cost = req.Cost,
                    MinDuration = req.MinDuration,
                };

                await _context.Services.AddAsync(service);

                await _context.SaveChangesAsync();

                return new BaseResultDTO<CreateServiceResultDTO>
                {
                    IsSuccess = true,
                    Message = "سرویس با موفقیت ایجاد شد.",
                    Data = new CreateServiceResultDTO
                    { 
                        Id = service.Id,
                    }
                };
            }
            catch (Exception ex)
            {
                return new BaseResultDTO<CreateServiceResultDTO>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}
