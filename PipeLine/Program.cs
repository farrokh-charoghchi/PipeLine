using System;
using System.Collections.Generic;

namespace PipeLine
{
    class Program
    {
        static void Main(string[] args)
        {
            string email = "farrokh.charoghchi@gmail.com";
            Func<object, object> getUserNameFunc = (object email) => ((string)email).Split('@')[0];
            Func<object, object> getUserNameLengthFunc = (object userName) => ((string)userName).Length;

            Func<object, object> GetlogResultFunc = (object length) => $"length is : {length} characters";



            Console.WriteLine("test1: Append multiple pre-writen Func separately");

            var emailNameLengthPipe_1 = new PipeLine()
                .Append<object, object>(getUserNameFunc)
                .Append<object, object>(getUserNameLengthFunc)
                .Append<object, object>(GetlogResultFunc);

            Console.WriteLine(emailNameLengthPipe_1.Run(email).ToString());



            Console.WriteLine("============================");



            Console.WriteLine("test2: Append multipple pre-writen Func at once");

            var emailNameLengthPipe_2 = new PipeLine().Append<object, object>(
                getUserNameFunc,
                getUserNameLengthFunc,
                GetlogResultFunc
            );

            Console.WriteLine(emailNameLengthPipe_2.Run(email).ToString());



            Console.WriteLine("============================");



            Console.WriteLine("test3: Append multiple inline Func separately");

            var emailNameLengthPipe_3 = new PipeLine()
                .Append<object, object>((object email) => ((string)email).Split('@')[0])
                .Append<object, object>((object userName) => ((string)userName).Length)
                .Append<object, object>((object length) => $"length is : {length} characters")
                ;

            Console.WriteLine(emailNameLengthPipe_3.Run(email).ToString());



            Console.WriteLine("============================");



            Console.WriteLine("test3: Append multiple inline Func at once");

            var emailNameLengthPipe_4 = new PipeLine().Append<object, object>(
                (object email) => ((string)email).Split('@')[0],
                (object userName) => ((string)userName).Length,
                (object length) => $"length is : {length} characters"
            );

            Console.WriteLine(emailNameLengthPipe_4.Run(email).ToString());

        }
    }

    public class PipeLine
    {
        private List<Func<object, object>> ops = new List<Func<object, object>>();
        public PipeLine Append<Tin, Tout>(Func<object, object> op)
        {
            ops.Add(op);
            return this;
        }
        public PipeLine Append<Tin, Tout>(
            Func<object, object> op1,
            Func<object, object> op2
            )
        {
            ops.Add(op1);
            ops.Add(op2);
            return this;
        }

        public PipeLine Append<Tin, Tout>(
            Func<object, object> op1,
            Func<object, object> op2,
            Func<object, object> op3
            )
        {
            ops.Add(op1);
            ops.Add(op2);
            ops.Add(op3);
            return this;
        }

        public object Run(object input)
        {
            object res = input;
            foreach (var op in ops)
            {
                res = op.Invoke(res);
            }
            return res;
        }
    }
}
