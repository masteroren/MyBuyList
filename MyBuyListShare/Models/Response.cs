using System;
using System.Runtime.Serialization;

namespace MyBuyListShare.Models
{
    public enum ResponseCodes
    {
        SUCCESS = 0,
        FAIL = 1
    }

    public class Response
    {
        public ResponseCodes code;
    }

    [DataContract]
    public class SuccessResponse<T>: Response
    {
        public T results;
        public object metaData;

        public SuccessResponse()
        {
            code = ResponseCodes.SUCCESS;
        }
    }

    [DataContract]
    public class FailureResponse : Response
    {
        public Exception exception;

        public FailureResponse()
        {
            code = ResponseCodes.FAIL;
        }
    }
}