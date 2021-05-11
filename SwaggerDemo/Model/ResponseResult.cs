using System;

namespace Model
{
    public class ResponseResult
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ResponseResult(object data) : this(0, null, data)
        {

        }

        public ResponseResult(string message, object data) : this(0, message, data)
        {

        }

        public ResponseResult(int code, string message, object data)
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }

        public static ResponseResult Success(object data)
        {
            return new ResponseResult(data);
        }
        public static ResponseResult Error(string message, object data)
        {
            return new ResponseResult(1, message, data);
        }
    }

    public class ResponseResult<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

}
