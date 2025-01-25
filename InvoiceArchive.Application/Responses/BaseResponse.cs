namespace InvoiceArchive.Application.Responses
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public IList<string> Errors { get; set; }
        public SortedListResponse? Sorting { get; set; }

        // Static Factory Method
        public static BaseResponse<T> Success(T data, int statusCode)
        {
            return new BaseResponse<T> { Data = data, StatusCode = statusCode, IsSuccess = true };
        }
        public static BaseResponse<T> Success(T data, int statusCode, SortedListResponse sorting)
        {
            return new BaseResponse<T> { Data = data, StatusCode = statusCode, IsSuccess = true, Sorting = sorting };
        }
        public static BaseResponse<T> Success(int statusCode)
        {
            return new BaseResponse<T> { Data = default, StatusCode = statusCode, IsSuccess = true };
        }
        public static BaseResponse<T> Fail(string error, int statusCode)
        {
            return new BaseResponse<T> { Data = default, StatusCode = statusCode, IsSuccess = false, Errors = new List<string>() { error } };
        }
        public static BaseResponse<T> Fail(IList<string> errors, int statusCode)
        {
            return new BaseResponse<T> { Data = default, StatusCode = statusCode, IsSuccess = false, Errors = errors };
        }
    }
}
