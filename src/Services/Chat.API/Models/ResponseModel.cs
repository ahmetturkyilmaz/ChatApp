using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Chat.API.Models
{
    public class ResponseModel<T>
    {
        public string HttpStatus { get; set; }

        public T Data { get; set; }
    }
}