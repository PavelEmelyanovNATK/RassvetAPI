namespace RassvetAPI.Models.ResponseModels
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public int Code { get; set; }
        public string Errors { get; set; }
    }
}
