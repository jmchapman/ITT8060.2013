// Using F# functions and data from C#

using System;
using Lecture8Library;
using System.Globalization;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var rc = new Rect(50.0f, 60.0f, 300.0f, 200.0f);
            // Note that rc is immutable, you cannot change the values
            // once it is created. You can only create new copies with modified
            // values.
            var rc2 = rc.Deflate(20.0f, 10.0f);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-us");
            var msg = String.Format("{0:f}; {1:f}; {2:f}; {3:f}", rc2.Left, rc2.Top, rc2.Width, rc2.Height);
            Console.WriteLine(msg);
            // Using a static function from module MyModule
            Console.WriteLine(MyModule.Add(1, 2));
            Console.ReadLine();

        }
    }
}
