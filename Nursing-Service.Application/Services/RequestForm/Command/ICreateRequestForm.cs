using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;

namespace Nursing_Service.Application.Services.RequestForm.Command
{
    public interface ICreateRequestForm
    {
        Task<BaseResultDTO> ExcuteAsync(CreateRequestFormRequestInfo req);
    }

    public class CreateRequestForm : ICreateRequestForm
    {
        private readonly IDataBaseContext _context;

        public CreateRequestForm(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO> ExcuteAsync(CreateRequestFormRequestInfo req)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(req.FullName))
                    throw new NotImplementedException("نام کامل بیمار نمیتواند خالی باشد.");
                if (String.IsNullOrWhiteSpace(req.PhoneNumber))
                    throw new NotImplementedException("شماره همراه بیمار نمیتواند خالی باشد.");
                if (String.IsNullOrWhiteSpace(req.Address))
                    throw new NotImplementedException("آدرس نمیتواند خالی باشد.");

                var rf = new Domain.Entities.RequestForm.RequestForm
                {
                    FullName = req.FullName,
                    PhoneNumber = req.PhoneNumber,
                    Address = req.Address,
                    Name = req.ServiceName
                };

                await _context.RequestForms.AddAsync(rf);

                await _context.SaveChangesAsync();

                return new BaseResultDTO
                {
                    IsSuccess = true,
                    Message = "فرم درخواست با موفقیت ثبت شد."
                };
            }
            catch (Exception ex)
            {
                return new BaseResultDTO
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
