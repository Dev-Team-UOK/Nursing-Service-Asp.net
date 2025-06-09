using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;

namespace Nursing_Service.Application.Services.Service.Command.Update
{
    public interface IUpdateService
    {
        Task<BaseResultDTO> ExcuteAsync(UpdateServiceRequestInfo request);
    }

    public class UpdateService  : IUpdateService
    {
        private IDataBaseContext _context;

        public UpdateService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO> ExcuteAsync(UpdateServiceRequestInfo request)
        {
            try
            {
                if (request.Id == null || request.Id <= 0)
                {
                    return new BaseResultDTO
                    {
                        IsSuccess = false,
                        Message = "Invalid service ID."
                    };
                }

                var service = await _context.Services.FindAsync(request.Id);

                if (service == null)
                    throw new NotImplementedException("هیچ سرویسی با شناسه موردنظر یافت نشد.");

                if (String.IsNullOrWhiteSpace(request.Name) is not true)
                    service.Name = request.Name;
                if (request.Cost is not null)
                    service.Cost = request.Cost.Value;
                if (request.MinDuration is not null)
                    service.MinDuration = request.MinDuration.Value;

                await _context.SaveChangesAsync();

                return new BaseResultDTO
                {
                    IsSuccess = true,
                    Message = "سرویس با موفقیت بروزرسانی شد."
                };
            }
            catch (Exception ex)
            {
                return new BaseResultDTO
                {
                    IsSuccess = false,
                    Message = $"An error occurred while updating the service: {ex.Message}"
                };
            }
        }
    }
}
