using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGTIT.Checador.Models
{
    class OperationMatchingResult<T>
    {
        public bool IsSuccess { get; }
        public MatchingStatus Status { get; }
        public T Data { get; }

        private OperationMatchingResult(bool isSuccess, MatchingStatus s, T data = default)
        {
            IsSuccess = isSuccess;
            Status = s;
            Data = data;
        }

        public static OperationMatchingResult<T> Success(T data) => new OperationMatchingResult<T>(true, MatchingStatus.FOUND, data);
        public static OperationMatchingResult<T> Failure(MatchingStatus status) => new OperationMatchingResult<T>(false, status);
        public static OperationMatchingResult<T> Failure(MatchingStatus status, T data) => new OperationMatchingResult<T>(false, status, data);
    }

    public enum MatchingStatus
    {
        NOT_FOUND,
        INACTIVE,
        BAD_AREA,
        FOUND
    }
}
