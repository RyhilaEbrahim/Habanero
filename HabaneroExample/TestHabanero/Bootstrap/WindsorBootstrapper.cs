using System;
using System.Reflection;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TestHabenaro.Db.Interfaces;
using TestHabenaro.DB;

namespace TestHabanero.Bootstrap
{
    public class WindsorBootstrapper : IDisposable
    {
        private readonly bool _isUnitTestEnvironment;
        private IWindsorContainer _container;

        public WindsorBootstrapper(bool isUnitTestEnvironment = false)
        {
            _isUnitTestEnvironment = isUnitTestEnvironment;
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public void Bootstrap()
        {
            CreateWindsorContainer();
            RegisterControllers();
            RegisterHelpers();
            SetupControllerFactory();
        }

        public IWindsorContainer BootstrapForTests()
        {
            CreateWindsorContainer();
            RegisterControllersForTest();
            RegisterHelpers();
            return _container;
        }

        private void CreateWindsorContainer()
        {
            _container = new WindsorContainer();
            _container.Register(
                Component
                    .For<IWindsorContainer>()
                    .Instance(_container)
                );
        }

        private void SetupControllerFactory()
        {
            _container.Register(
                Component
                    .For<IControllerFactory>()
                    .ImplementedBy<WindsorControllerFactory>()
                    .LifeStyle.Singleton
                );

            var controllerFactory = _container.Resolve<IControllerFactory>();
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        private void RegisterControllers()
        {
            _container.Register(
                AllTypes
                    .FromAssembly(Assembly.GetExecutingAssembly())
                    .BasedOn<IController>()
                    .LifestylePerWebRequest()
                );
        }

        private void RegisterControllersForTest()
        {
            _container.Register(
                AllTypes
                    .FromAssembly(Assembly.GetExecutingAssembly())
                    .BasedOn<IController>()
                    .LifestyleTransient()
                );
        }

        private void RegisterHelpers()
        {
            _container.Register(Component.For<ICarRepository>().ImplementedBy<CarRepository>().LifestyleTransient());
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}