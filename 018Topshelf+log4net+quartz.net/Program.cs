using System;
using System.IO;
using Topshelf;

namespace _018Topshelf_log4net_quartz.net
{
    #region 说明
    /// <summary>
    /// 1. nuget
    ///    Topshelf
    ///    Topshelf.Log4net
    ///    Topshelf.Quartz
    /// 2. 关于Quartz.net 
    ///     首先定义MyJob，实现IJob接口
    ///     定义MyScheduler，封装调度器
    /// 3. 关于Log4net
    ///     定义单独的配置文件log4net.config(注意右键-->属性-->复制到输出目录：始终复制)
    /// 4. 关于Topshelf
    ///     定义MyServiceRunner,实现相应接口
    ///     在Main入库函数中启动
    /// </summary>
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            //这里读取log4net.config
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            HostFactory.Run(x =>
            {
                x.UseLog4Net();

                x.Service<MyServiceRunner>();

                x.SetDescription("服务描述");
                x.SetDisplayName("服务显示名称");
                x.SetServiceName("服务名称");

                x.EnablePauseAndContinue();
            });
        }

    }
}

