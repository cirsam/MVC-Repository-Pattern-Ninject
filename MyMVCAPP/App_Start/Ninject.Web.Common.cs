[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MyMVCAPP.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MyMVCAPP.App_Start.NinjectWebCommon), "Stop")]

namespace MyMVCAPP.App_Start
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using MyMVCAPP.DataModels;
    using MyMVCAPP.Repository;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            var getclasses = Assembly.GetExecutingAssembly().GetTypes().Where(a=>a.Namespace=="MyMVCAPP.DataModels");

            getclasses.ToList().ForEach(type=>{
                Type getObjectNameByNameSpace = Type.GetType(type.FullName);
                Type createGenericInterfaceOfObj = typeof(IRepository<>).MakeGenericType(getObjectNameByNameSpace);
                Type createGenericTypeOfObj = typeof(Repository<>).MakeGenericType(getObjectNameByNameSpace);

                kernel.Bind(createGenericInterfaceOfObj).To(createGenericTypeOfObj);
            });
        }
    }


}