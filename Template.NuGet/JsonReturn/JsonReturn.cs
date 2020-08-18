using System;

namespace Template.NuGet
{
    [Serializable]
    public class JsonReturn
    {
        public JsonReturn()
        {
            Status = ResultStatus.OK;
        }

        public JsonReturn(ResultStatus status)
        {
            Status = status;
        }

        public JsonReturn(ResultStatus status, string code, string msg)
        {
            Status = ResultStatus.OK;
            Status = status;
            Code = code;
            Msg = msg;
        }

        public static JsonReturn<T> CreateResult<T>(T data) => CreateResult<T>(ResultStatus.OK, data);

        public static JsonReturn<T> CreateResult<T>(ResultStatus status) => CreateResult<T>(status, default(T));

        public static JsonReturn<T> CreateResult<T>(ResultStatus status, T data) => new JsonReturn<T>(status) { Data = data };

        public static JsonReturn CreateResult(string code = null, string msg = null) => CreateResult(ResultStatus.OK, code, msg);

        public static JsonReturn CreateResult(ResultStatus status, string code = null, string msg = null) => new JsonReturn(status)
        {
            Code = code,
            Msg = msg
        };

        // Properties
        public ResultStatus Status { get; set; }

        public string Code { get; set; }

        public string Msg { get; set; }
    }
    public class JsonReturn<T> : JsonReturn
    {
        // Methods
        public JsonReturn()
        {
        }

        public JsonReturn(ResultStatus status) : base(status)
        {
        }

        public JsonReturn(ResultStatus status, T data) : base(status)
        {
            Data = data;
        }

        // Properties
        public T Data { get; set; }
    }
}
