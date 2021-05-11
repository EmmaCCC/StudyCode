using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Common;
using Model;

namespace ConsoleApp
{
    class Program
    {
        private static bool flag = true;
        static void Main(string[] args)
        {


            Timer timer = new Timer(2000);
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;

            while (flag)
            {
                Console.WriteLine("耗时操作");
            }
            Console.WriteLine("操作停止");
            Console.ReadKey();

            //var dt = new DataTable();
            //dt.Columns.Add(new DataColumn("姓名"));
            //dt.Columns.Add(new DataColumn("年龄"));
            //dt.Columns.Add(new DataColumn("Gender"));
            //dt.Rows.Add("zhangsan", 2, "男");
            //dt.Rows.Add("lisi", 1, "女");

            //var persons = ExcelTools.Import<Person>(dt);

            //var bytes = ExcelTools.Export(persons);
            //File.WriteAllBytes("1.xlsx", bytes);
            //using (FileStream fs = new FileStream("F:/1.xlsx", FileMode.Open))
            //{

            //    var list = ExcelTools.Import<Person>(fs, fs.Name,
            //        context =>
            //        {
            //            return 0;
            //        },
            //        exception =>
            //        {
            //             return true;
            //        }
            //     );
            //}
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            flag = false;
        }

        public static bool DoWithTimeout<T>(Func<T> func, int timeout, out T result)
        {
            Exception ex = null;
            result = default(T);
            T res = default(T);
            CancellationTokenSource cts = new CancellationTokenSource();
            Task task = Task.Run(() =>
            {
                try
                {
                    using (cts.Token.Register(Thread.CurrentThread.Abort))
                    {
                        res = func();
                    }
                }
                catch (Exception e)
                {
                    if (!(e is ThreadAbortException))
                        ex = e;
                }
            }, cts.Token);
            bool done = task.Wait(timeout);
            if (ex != null)
                throw ex;
            if (done)
                result = res;
            else
                cts.Cancel();
            return done;
        }
    }
}
