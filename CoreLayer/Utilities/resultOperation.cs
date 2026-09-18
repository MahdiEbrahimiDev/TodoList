using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities
{

    public class resultOperation
    {
        public enum StatusType
        {
            Success = 0,
            Error = 1
        }


        public StatusType Status { get; set; }

        public string Message { get; set; }


        public bool IsSuccess
        {
            get
            {
                return Status == StatusType.Success;
            }
        }


        public static resultOperation Success(string message)
        {
            return new resultOperation
            {
                Status = StatusType.Success,
                Message = message
            };
        }


        public static resultOperation Error(string message)
        {
            return new resultOperation
            {
                Status = StatusType.Error,
                Message = message
            };
        }
    }
}
