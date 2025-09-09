namespace StudentManagerAPI.Data.DTO.Shared;

public class ResultHandler<T> 
{
    public bool Success { get; private set; }
    public string ErrorMessage { get; private set; }
    public T Data { get; private set; }

    public static ResultHandler<T> Ok(T data)
        => new ResultHandler<T> { Success = true, Data = data };
    
    public static ResultHandler<T> Fail(string errorMessage)
        => new  ResultHandler<T> { Success = false, ErrorMessage = errorMessage };
}