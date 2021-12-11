using System;
using System.IO;
using System.Timers;
using Topshelf;

namespace _018Topshelf
{
    public class MyService : ServiceControl
    {
        //这里我使用Timer类实现定时任务，但是应该会出现时间误差，若是需要一直执行累计起来的误差会很大
        //可以配合Quartz.net实现定时任务

        //注意使用的System.Timers.Timer命名空间下的Timer类
        //不是System.Thread.Timers
        readonly Timer _timer;
        public MyService()
        {
            _timer = CreateTimer(1000, TargetAction);
        }


        public bool Start(HostControl hostControl)
        {
            _timer.Start();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _timer.Stop();
            return true;
        }


        #region 定时器
        /// <summary>
        /// 创建并初始化定时器
        /// </summary>
        /// <param name="intervalSecond">间隔事件（毫秒）</param>
        /// <param name="targetAction">执行的方法</param>
        /// <param name="isAutoReset">是否一直执行</param>
        /// <returns></returns>
        public Timer CreateTimer(int intervalSecond, Action<object, ElapsedEventArgs> targetAction, bool isAutoReset = true)
        {
            //设置定时间隔(毫秒为单位)
            Timer timer = new System.Timers.Timer(intervalSecond);
            //设置执行一次（false）还是一直执行(true)
            timer.AutoReset = isAutoReset;
            //设置是否执行System.Timers.Timer.Elapsed事件
            timer.Enabled = true;
            //绑定Elapsed事件
            timer.Elapsed += new System.Timers.ElapsedEventHandler(targetAction);
            return timer;

            //简洁配置方式
            //timer = new Timer(1000) { AutoReset = true };
            //timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is time of {0}.", DateTime.Now);
        }


        /// <summary>
        /// 定时器执行的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TargetAction(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("It is time of {0}.", DateTime.Now);
            WriteLog($"It is time of { DateTime.Now}.", "MyLog.txt");
        }


        /// <summary>
        /// 写入日志文件
        /// </summary>
        /// <param name="logText"></param>
        public void WriteLog(string logText, string fileName)
        {
            string logPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, fileName);
            using (StreamWriter stream = new StreamWriter(logPath, true))
            {
                stream.WriteLine(logText);
            }
        }
        #endregion
    }
}


