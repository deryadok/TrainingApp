namespace TrainingApp.Shared.RequestModel
{
    public class ApiRequest<T>
    {
        public T Payload { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;

        public ApiRequest()
        {
        }

        public ApiRequest(T payload)
        {
            Payload = payload;
        }
    }
}
