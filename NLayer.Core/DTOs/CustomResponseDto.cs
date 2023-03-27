using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{


    public class CustomNoContentResponseDto
    {
        public List<String> Errors { get; set; }
        public int StatusCode { get; set; }

        public static CustomNoContentResponseDto Success(int statusCode)
        {
            return new CustomNoContentResponseDto
            {
                StatusCode = statusCode

            };
        }

        public static CustomNoContentResponseDto Fail(List<String> errors, int statusCode)
        {
            return new CustomNoContentResponseDto
            {

                StatusCode = statusCode,
                Errors = errors

            };
        }

        public static CustomNoContentResponseDto Fail(string error, int statusCode)
        {
            return new CustomNoContentResponseDto
            {

                StatusCode = statusCode,
                Errors = new List<String> { error }

            };
        }



    }



    public class CustomResponseDto<T>
    {
        public T Data { get; set; }
        public List<String> Errors { get; set; }
        public int StatusCode { get; set; }

        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T>
            {
               
                StatusCode = statusCode

            };
        }

        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T>
            {
                Data = data,
                StatusCode = statusCode

            };
        }

        public static CustomResponseDto<T> Fail(List<String> errors, int statusCode)
        {
            return new CustomResponseDto<T>
            {

                StatusCode = statusCode,
                Errors = errors

            };
        }

        public static CustomResponseDto<T> Fail(string error, int statusCode)
        {
            return new CustomResponseDto<T>
            {

                StatusCode = statusCode,
                Errors = new List<String> { error }

            };
        }



    }
}
