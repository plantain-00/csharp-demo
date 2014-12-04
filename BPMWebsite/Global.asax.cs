using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

using Autofac;
using Autofac.Integration.Mvc;

using BPM.Image.net;
using BPM.Service;

namespace BPMWebsite
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        public static List<ProcessModel> Process = new List<ProcessModel>();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var builder = new ContainerBuilder();
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<HomeService>().As<IHomeService>();
            builder.RegisterType<ProcessService>().As<IProcessService>();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            CurveStyle.DefaultPen = new Pen(Color.Black, 1)
                                    {
                                        DashStyle = DashStyle.Custom,
                                        DashPattern = new float[]
                                                      {
                                                          5,
                                                          5
                                                      }
                                    };
            var process = new ProcessModel(Protocols.WORK_ASSIGNMENT, 2, 5, "开始");
            process.StartNode.PointTo("分配任务", 1, 0.5f).PointTo("财务部", 2, 0).PointTo("查看完成情况", 3, 0.5f).PointTo("结束", 4, 0.5f);
            process["分配任务"].PointTo("化验室", 2, 1).AttachTo("查看完成情况");
            process.StartNode.Style.Shape = ProcessNodeShape.Ellipse;
            process["结束"].Style.Shape = ProcessNodeShape.Ellipse;
            Process.Add(process);
        }

        public static void Set(ProcessModel process, string start, string end)
        {
            var arrowModel = process[start, end];
            arrowModel.Style.CurveStyle.Pen = new Pen(Brushes.Red);
            arrowModel.Style.HeadStyle.Brush = Brushes.Red;
        }

        public static void Set(ProcessModel process, string node)
        {
            process[node].Style.WordsStyle.Brush = Brushes.Red;
        }

        public static void Reset(ProcessModel process, string start, string end)
        {
            var arrowModel = process[start, end];
            arrowModel.Style.CurveStyle.Pen = new Pen(Brushes.Black);
            arrowModel.Style.HeadStyle.Brush = Brushes.Black;
        }

        public static void Reset(ProcessModel process, string node)
        {
            process[node].Style.WordsStyle.Brush = Brushes.Black;
        }
    }
}