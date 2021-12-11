using log4net;
using Quartz;

namespace _018Topshelf_log4net_quartz.net
{

    /// <summary>
    /// Quartz.net
    /// 任务类，定义我们需要调度的任务
    /// </summary>
    public class MyJob : IJob
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(MyJob));
        public void Execute(IJobExecutionContext context)
        {
            //todo:你所期望实现定时执行的操作

            //计入日志
            _logger.InfoFormat("任务执行成功");
        }
    }
}
