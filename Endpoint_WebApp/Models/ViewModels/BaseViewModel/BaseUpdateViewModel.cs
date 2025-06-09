namespace Endpoint_WebApp.Models.ViewModels.BaseViewModel
{
    public class BaseUpdateViewModel<TType>
    {
        public TType Id { get; set; }
    }

    public class BaseUpdateViewModel : BaseUpdateViewModel<ulong>
    {
    }
}
