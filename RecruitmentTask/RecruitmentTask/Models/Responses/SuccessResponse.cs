namespace RecruitmentTask.Models.Responses
{
    public class SuccessResponse<T> : ResponseBaseClass<T> where T : class
    {
        public T Result { get; set; }

        public SuccessResponse(T result)
        {
            Result = result;
            StatusCode = 200;
        }
    }
}
