using RassvetAPI.Models.ResponseModels;
using RassvetAPI.Util.UsefulExtensions;
using System;

namespace RassvetAPI.Util
{
    public class ResponseBuilder
    {
        public static BaseResponse<T> Create<T>(int code, T data, string errors = null) 
            => new() { Code = code, Data = data, Errors = errors };

        public static BaseResponse<string> Create(int code, string errors = null)
            => new() { Code = code, Data = null, Errors = errors };
    }
}
