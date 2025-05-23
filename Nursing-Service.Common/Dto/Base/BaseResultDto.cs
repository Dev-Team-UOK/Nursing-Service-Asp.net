namespace Nursing_Service.Common.Dto.Base
{
    public class BaseResultDTO
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }

    public class BaseResultDTO<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
    }
}
