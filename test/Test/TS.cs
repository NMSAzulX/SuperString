using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    public class TS
    {
        const int count = 10000;
        public TS()
        {

        }

        [Benchmark(Description = "测试StringBuilder")]
        public void Test_StringBuilder()
        {
            StringBuilder b_str = new StringBuilder();
            //for (int i = 0; i < count; i += 1)
            //{
                b_str.Append("tttt");
           // }
        }
        [Benchmark(Description = "测试StringJoin")]
        public void Test_StringJoin()
        {
            string str = string.Empty;
            //for (int i = 0; i < count; i += 1)
            //{
                str = String.Join("",str, "tttt");
           // }
        }
        [Benchmark(Description = "测试SuperString")]
        public void Test_EString()
        {
            EString e_str = string.Empty;
            //for (int i = 0; i < count; i += 1)
            //{
                e_str += "tttt";
            //}
        }
    }
}
