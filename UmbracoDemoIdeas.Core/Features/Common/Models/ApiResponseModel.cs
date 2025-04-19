namespace UmbracoDemoIdeas.Core.Features.Common.Models;
public class ApiResponseModel<T>
{
    public Exception? Error { get; set; }
    public T? Content { get; set; }
    public bool IsSuccess { get => Error is not null; }
}
