namespace RecruitmentTask.Models.Responses
{
    public abstract class ResponseBaseClass<T>
    {
        public int StatusCode { get; set; }
    }

    public abstract class ResponseBaseClass
    {
        public int StatusCode { get; set; }
    }
}
