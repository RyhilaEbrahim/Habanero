using Castle.Windsor;
using TestHabanero.Bootstrap;
using TestHabanero.BO;

namespace TestHabanero.DB.Tests
{
    public class TestUtils
    {
        private static IWindsorContainer _container;
        public static IWindsorContainer Container { get { return _container; } }

        public static void SetupFixture()
        {
            BOBroker.LoadClassDefs();
            ConfigureContainer();
        }
        public static void ConfigureContainer()
        {
            var diBootstrapper = new WindsorBootstrapper(isUnitTestEnvironment: true);
            _container = diBootstrapper.BootstrapForTests();
        }
    }
}
