using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Errors
{
    public class ErrorMessage
    {
        public bool error { get; set; }
        public string message { get; set; } = string.Empty;
    }
}