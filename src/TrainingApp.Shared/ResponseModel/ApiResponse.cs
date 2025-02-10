namespace TrainingApp.Shared.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}
