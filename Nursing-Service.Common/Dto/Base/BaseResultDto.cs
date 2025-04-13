namespace Nursing_Service.Common.Dto.Base
{
    public class BaseResultDto
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }

    public class BaseResultDto<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
    }
}
