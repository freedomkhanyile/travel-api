using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Api.Models.Common
{
    public class DataResults<T>
    {
        public T[] Data { get; set; }
        public int Total { get; set; }
    }

    public class DataResults
    {
        public object Data { get; set; }
        public int Total { get; set; }
    }
}
