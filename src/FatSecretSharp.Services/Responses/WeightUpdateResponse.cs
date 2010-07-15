using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FatSecretSharp.Services.Responses
{
    public class ValueWrapper
    {
        public int value { get; set; }
    }

    public class WeightUpdateResponse
    {
        public ValueWrapper success { get; set; }

        public bool IsSuccess
        {
            get
            {
                return success == null ? false : success.value == 1;
            }
        }
    }
}
