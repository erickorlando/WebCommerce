namespace WebCommerce.Dto.Response;

public class BaseResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } 
}

public class BaseResponseGeneric<T> : BaseResponse
{
    public T Data { get; set; }
}