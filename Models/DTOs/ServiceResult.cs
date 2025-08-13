namespace LibraryTask.Models.DTOs
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public T Result { get; set; }

        public static ServiceResult<T> Ok(T? result) =>
            new ServiceResult<T> { Success = true, Result = result };

        public static ServiceResult<T> Fail(string error) =>
            new ServiceResult<T> { Success = false, ErrorMessage = error };

    }
}
