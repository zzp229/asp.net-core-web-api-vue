using Model.Other;

namespace WebAPI.Config
{
    public class ResultHelper
    {
        public static ApiResult Success(object data)
        {
            return new ApiResult() { IsSuccess = true, Result = data, Msg = string.Empty };
        }
        public static ApiResult Error(string msg)
        {
            return new ApiResult() { IsSuccess = false, Result = null, Msg = msg };
        }
    }
}
