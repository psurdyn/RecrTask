namespace RecruitmentTask.Models.Responses
{
    public abstract class ResponseBaseClass<T> where T : class
    {
        public int StatusCode { get; set; }
    }
}
