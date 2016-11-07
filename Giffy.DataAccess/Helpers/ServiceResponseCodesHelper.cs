using System;
using System.Linq.Expressions;

namespace Giffy.DataAccess.Helpers
{
    public static class ServiceResponseCodesHelper
    {
        public static readonly string ACTION_SUCCESS = GetResponseCode(() => ResponseCodeString.Action_Success);

        public static readonly string POST_GET_ERROR = GetResponseCode(() => ResponseCodeString.PostGet_Error);
        public static readonly string POST_UPDATE_ERROR = GetResponseCode(() => ResponseCodeString.PostUpdate_Error);

        // Uses lambda expressions to extract the name of the supplied property, which is the same as the error code.
        // Reference from Veebs
        private static string GetResponseCode<T>(Expression<Func<T>> exp)
        {
            return (((MemberExpression)(exp.Body)).Member).Name;
        }
    }
}
