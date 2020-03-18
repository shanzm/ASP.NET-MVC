using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _012_任务调度框架_Quartz.NET
{
    public class TestJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() => Console.WriteLine($"{DateTime.Now}+执行任务1了……"));
        }
    }
}