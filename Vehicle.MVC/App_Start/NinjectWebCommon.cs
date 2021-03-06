[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Vehicle.MVC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Vehicle.MVC.App_Start.NinjectWebCommon), "Stop")]

namespace Vehicle.MVC.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Vehicle.Service.Interfaces;
    using Vehicle.Service.Classes;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
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
            kernel.Bind<IVehicleMakeService>().To<VehicleMakeService>();
            kernel.Bind<IVehicleModelService>().To<VehicleModelService>();
            kernel.Bind<IMakeSort>().To<MakeSort>().WithConstructorArgument("sortOrder") ;
            kernel.Bind<IModelSort>().To<ModelSort>().WithConstructorArgument("sortOrder");
            kernel.Bind<IMakeFilter>().To<MakeFilter>().WithConstructorArgument("searchString");
            kernel.Bind<IModelFilter>().To<ModelFilter>().WithConstructorArgument("searchString");
            kernel.Bind<IMakePage>().To<MakePage>().WithConstructorArgument("page");
            kernel.Bind<IModelPage>().To<ModelPage>().WithConstructorArgument("page");
            kernel.Load(new AutoMapperConfig.AutoMapperModule());
        }
    }
}