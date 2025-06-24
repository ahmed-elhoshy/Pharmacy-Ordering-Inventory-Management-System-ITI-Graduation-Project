using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.DomainLayer.Common
{
    public class CustomResponse<T>
    {
        public string Message { get; set; }
        public T Result { get; set; }

        public CustomResponse(string message, T result)
        {
            Message = message;
            Result = result;
        }
    }
}
