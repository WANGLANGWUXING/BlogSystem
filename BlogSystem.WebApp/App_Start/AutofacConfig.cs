using Autofac;
using Autofac.Integration.Mvc;
using BlogSystem.BLL;
using BlogSystem.DAL;
using BlogSystem.IBLL;
using BlogSystem.IDAL;
using System.Reflection;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace BlogSystem.WebApp.App_Start
{
    public class AutofacConfig
    {
        public static void Register()
        {
            // 配置autofac 的基本信息
            //(1) 创建容器
            var builder = new ContainerBuilder();

            //(2) 进行依赖注入的注册
            //builder.RegisterType<RolesDal>().As<IRolesDal>();
            //builder.RegisterType<RolesBll>().As<IRolesBll>();
            // 通过反射实现直接配置 程序集，一个语句实现所有的内容
            Assembly dal = Assembly.Load("BlogSystem.DAL");//通过反射找到对应的dal层
            builder.RegisterAssemblyTypes(dal).AsImplementedInterfaces();//通过容器注册反射得到类型，并且与接口层进行关联
            Assembly bll = Assembly.Load("BlogSystem.BLL");
            builder.RegisterAssemblyTypes(bll).AsImplementedInterfaces();


            //(3) 注册控制器
            builder.RegisterControllers(typeof(AutofacConfig).Assembly);
            //(4) 构建
            var container = builder.Build();
            //(5) 实现
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}