using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Application.DTOs;

namespace TaxCalculation.Application.Mapping
{
    //public class ApiResponse<T>
    //{
    //    public bool Success { get; set; }

    //    public string Message { get; set; }

    //    public T? Data { get; set; }

    //    public List<string>? Errors { get; set; }

    //    public int StatusCode { get; set; }

    //    public static ApiResponse<T> Ok(
    //        T data,
    //        string message = "Request successful",
    //        int statusCode = 200)
    //    {
    //        return new ApiResponse<T>
    //        {
    //            Success = true,
    //            Message = message,
    //            Data = data,
    //            StatusCode = statusCode
    //        };
    //    }

    //    public static ApiResponse<T> Fail(
    //        string message,
    //        int statusCode = 400,
    //        List<string>? errors = null)
    //    {
    //        return new ApiResponse<T>
    //        {
    //            Success = false,
    //            Message = message,
    //            StatusCode = statusCode,
    //            Errors = errors
    //        };
    //    }
    //}
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public static ApiResponse<T> Ok(
            T data,
            string message = "Success",
            int statusCode = 200)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                StatusCode = statusCode,
                Data = data,
                Errors = null
            };
        }

        public static ApiResponse<T> Fail(
            string message,
            int statusCode = 400,
            List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                StatusCode = statusCode,
                Data = default,
                Errors = errors
            };
        }

    }
}
