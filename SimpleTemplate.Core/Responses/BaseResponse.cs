using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTemplate.Application.Responses
{
    public class BaseResponse 
    {
        public BaseResponse()
        {
        }

        public BaseResponse(string message)
        {
            Message = message;
        }

        public BaseResponse(string message, bool success)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string>? ValidationErrors { get; set; }
    }
}
