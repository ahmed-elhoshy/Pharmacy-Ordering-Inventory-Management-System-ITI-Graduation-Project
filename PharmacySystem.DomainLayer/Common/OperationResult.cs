using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.DomainLayer.Common
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; private set; }
        public string? ErrorMessage { get; private set; }
        public T? Data { get; private set; }

        private OperationResult(bool isSuccess, T? data, string? errorMessage)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
        }

        public static OperationResult<T> Success(T data)
        {
            return new OperationResult<T>(true, data, null);
        }

        public static OperationResult<T> Failure(string errorMessage)
        {
            return new OperationResult<T>(false, default, errorMessage);
        }
    }
}
