using Quartz;
using Topshelf;

namespace _018Topshelf_log4net_quartz.net
{
    /// <summary>
    /// Topshelf
    /// 服务类，实现相应接口，定义服务启动和停止执行的操作
    /// </summary>
    public class MyServiceRunner : ServiceControl, ServiceSuspend
    {

        private readonly IScheduler scheduler = MyScheduler.GetMyScheduler().scheduler;


        #region ServiceControl接口需要实现的方法
        public bool Start(HostControl hostControl)
        {
            scheduler.Start();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            scheduler.Shutdown(false);
            return true;
        }
        #endregion

        #region ServiceSuspen接口需要实现的方法
        public bool Continue(HostControl hostControl)
        {
            scheduler.ResumeAll();
            return true;
        }

        public bool Pause(HostControl hostControl)
        {
            scheduler.PauseAll();
            return true;
        }
        #endregion


    }
}
