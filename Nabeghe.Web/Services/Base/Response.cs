﻿namespace Nabeghe.Web.Services.Base
{
    public class Response<T>
    {
        public string ValidationErrors { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
