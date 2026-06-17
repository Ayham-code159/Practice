namespace CRUDPractice.Responses
{
    public class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; set; }
    }

}
