using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Chat.API.Models
{
    public class ResponseModel<T>
    {
        public int HttpStatus { get; set; }

        public T Data { get; set; }

        public ResponseModel(HttpStatusCode httpStatus, T data)
        {
            HttpStatus = (int) httpStatus;
            Data = data;
        }
    }
}