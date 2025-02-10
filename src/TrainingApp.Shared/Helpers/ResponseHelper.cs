using TrainingApp.Shared.Response;

namespace TrainingApp.Shared.Helpers
{
    public static class ResponseHelper<T>
    {
        public static ApiResponse<T> GetResponse(T data, bool success)
        {
            return new ApiResponse<T>()
            {
                Data = data,
                Success = success
            };
        }

        public static ApiResponse<T> GetResponse(T data, bool success, string message)
        {
            return new ApiResponse<T>()
            {
                Data = data,
                Success = success,
                SuccessMessage = message
            };
        }

        public static ApiResponse<T> GetResponse(bool success, string message)
        {
            return new ApiResponse<T>()
            {
                Success = success, 
                SuccessMessage = message
            };
        }

        public static ApiResponse<T> GetResponse(T data, bool success, string successMessage, string errorMessage)
        {
            if (success) 
                return new ApiResponse<T>()
                {
                    Data = data,
                    Success = success,
                    SuccessMessage = successMessage
                };
            else
                return new ApiResponse<T>()
                {
                    Success = success,
                    ErrorMessage = errorMessage
                };
        }



        public static ApiResponse<T> GetResponse(bool success, string successMessage, string errorMessage)
        {
            if (success)
                return new ApiResponse<T>()
                {
                    Success = success,
                    SuccessMessage = successMessage
                };
            else
                return new ApiResponse<T>()
                {
                    Success = success,
                    ErrorMessage = errorMessage
                };
        }
    }
}
