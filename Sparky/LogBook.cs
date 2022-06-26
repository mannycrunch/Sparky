using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public interface IlogBook
    {
        void Message(string message);

        bool LogToDb(string message);

        bool LogBalanceAfterWithdrawal(int blanceAfterWithdrawal);
        string MessageWithReturnStr(string message);
        bool LogWithOutputResult(string str, out string outputStr);
    }

    public class LogBook : IlogBook
    {
        public bool LogBalanceAfterWithdrawal(int blanceAfterWithdrawal)
        {
            if(blanceAfterWithdrawal >= 0)
            {
                Console.WriteLine("Success");
                return true;
            }
            Console.WriteLine("Failure");
            return false;
        }

        public bool LogToDb(string message)
        {
            Console.WriteLine(message);
            return true;
        }

        public bool LogWithOutputResult(string str, out string outputStr)
        {
            outputStr = "Hello " + str;
            return true;
        }

        public void Message(string message)
        {
            Console.WriteLine(message);
        }
        public string MessageWithReturnStr(string message)
        {
            Console.WriteLine(message);
            return message.ToLower();
        }
    }

    //public class LogBookFake : IlogBook
    //{
    //    public void Message(string message)
    //    {
    //    }
    //}
}
