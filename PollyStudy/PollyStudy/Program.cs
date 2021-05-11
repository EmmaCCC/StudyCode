using Polly;
using System;
using System.IO;
using System.Net.Http;

namespace PollyStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.定义条件 2.定义处理方式 3 执行

            var policy = Policy
                .Handle<ArgumentException>() //定义条件
                .Retry(3); //重试处理方式

            #region 定义处理异常

            //单个条件的异常
            Policy.Handle<ArgumentException>(ex => ex.ParamName == "string");

            //多个异常类型
            Policy.Handle<ArgumentException>(ex => ex.ParamName == "string")
                .Or<FieldAccessException>()
                .Or<IndexOutOfRangeException>();

            //内部异常

            Policy.Handle<ArgumentException>().OrInner<FileNotFoundException>();

            //用返回结果来限定
            Policy.HandleResult<HttpResponseMessage>(r => r.StatusCode == System.Net.HttpStatusCode.NotFound);
            #endregion

            //1.重试
            #region 重试Retry
            Policy.Handle<ArgumentException>().Retry();
            //重试n次
            Policy.Handle<ArgumentException>().Retry(3);
            //重试n次，加上回调
            Policy.Handle<ArgumentException>().Retry(3, (ex, retryCount) =>
             {
                //记日志
            });

            //永远重试
            Policy.Handle<ArgumentException>().RetryForever((ex) =>
            {
                //记日志
            });

            //等待之后重试,等待1,2,3秒后重试
            Policy.Handle<ArgumentException>().WaitAndRetry(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(3),
            });

            //等待重试回调
            Policy.Handle<ArgumentException>().WaitAndRetry(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(3),
            },(ex,timeSpan,context)=> { });
            #endregion

            //2.熔断

            #region 熔断CircuitBreak
            //如果超过2次异常，就停止两分钟不在请求，之后再恢复请求
            Policy.Handle<ArgumentException>().CircuitBreaker(2, TimeSpan.FromMinutes(1));

            //熔断回复

            Policy.Handle<ArgumentException>().CircuitBreaker(2,
                TimeSpan.FromMinutes(1),
                (ex, timeSpan) =>
                {
                    //onbreak
                },
                () =>
                {
                    //onreet
                });

            #endregion

            //回退

            #region 回退Fallback
            Policy.Handle<ArgumentException>().Fallback(()=> {

            });

            #endregion

            //超时

            #region 超时Timeout

            Policy.Timeout(30, (context, timespan, task) =>
            {

            });
            #endregion

            //舱壁:限制某一个操作的最大并发执行量。比如限制为12

            #region 舱壁
            //当请求执行操作被拒绝的时候，执行回调
            Policy.Bulkhead(12, context => { });
            #endregion

            #region 组合条件
            var timeout = Policy.Timeout(30);
            var retry = Policy.Handle<ArgumentException>().Retry(3);

            var wrap = Policy.Wrap(timeout, retry);

            wrap.Execute(() => { });
            #endregion

            policy.Execute(() =>
            {
                Console.WriteLine("请求api");
                System.Threading.Thread.Sleep(2000);
                throw new ArgumentException("参数错误");
            });

            Console.ReadKey();
        }
    }
}


