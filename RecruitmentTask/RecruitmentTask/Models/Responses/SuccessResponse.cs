namespace RecruitmentTask.Models.Responses
{
    public class SuccessResponse<T> : ResponseBaseClass<T>
    {
        public T Result { get; set; }

        public SuccessResponse(T result)
        {
            Result = result;
            StatusCode = 200;
        }
    }
}
