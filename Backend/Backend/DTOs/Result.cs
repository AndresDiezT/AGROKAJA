namespace Backend.DTOs
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public T? Data { get; set; }
        public object? Extra { get; set; }

        public static Result<T> Ok(T data) => new() { Success = true, Data = data };

        public static Result<T> Fail(string error) => new() { Success = false, Error = error };

        public static Result<T> FailWithData(string message, object extra)
        {
            return new Result<T>
            {
                Success = false,
                Error = message,
                Extra = extra
            };
        }
    }
}
