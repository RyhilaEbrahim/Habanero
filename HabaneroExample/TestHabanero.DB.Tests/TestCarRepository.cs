using Habanero.BO;
using NUnit.Framework;
using TestHabanero.BO;
using TestHabanero.Tests.Commons;
using TestHabenaro.Db.Interfaces;
using BORegistry = Habanero.BO.BORegistry;

namespace TestHabanero.DB.Tests
{
    [TestFixture]
    public class TestCarRepository
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            TestUtils.SetupFixture();
        }

        [SetUp]
        public void Setup()
        {
            BORegistry.DataAccessor = new DataAccessorInMemory();
        }

        [Test]
        public void GetCars_GivenNoCars_ShouldReturnEmptyCollection()
        {
            //---------------Set up test pack-------------------
            var carRepository = CreateCarRepository();
            //---------------Assert Precondition----------------
            var cars = Broker.GetBusinessObjectCollection<Car>("");
            Assert.AreEqual(0, cars.Count);
            //---------------Execute Test ----------------------
            var results = carRepository.GetCars();
            //---------------Test Result -----------------------
            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public void GetCars_GivenOneCar_ShouldReturnCar()
        {
            //---------------Set up test pack-------------------
            CreateSavedCar();
            var carRepository = CreateCarRepository();
            //---------------Assert Precondition----------------
            var cars = Broker.GetBusinessObjectCollection<Car>("");
            Assert.AreEqual(1, cars.Count);
            //---------------Execute Test ----------------------
            var results = carRepository.GetCars();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, results.Count);
        }

        [Test]
        public void GetCars_GivenTwoCars_ShouldReturnCars()
        {
            //---------------Set up test pack-------------------
            CreateSavedCar();
            CreateSavedCar();
            var userRepository = CreateCarRepository();
            //---------------Assert Precondition----------------
            var cars = Broker.GetBusinessObjectCollection<Car>("");
            Assert.AreEqual(2, cars.Count);
            //---------------Execute Test ----------------------
            var results = userRepository.GetCars();
            //---------------Test Result -----------------------
            Assert.AreEqual(2, results.Count);
        }

        [Test]
        public void GetCars_GivenManyCars_ShouldReturnCars()
        {
            //---------------Set up test pack-------------------
            CreateSavedCar();
            CreateSavedCar();
            CreateSavedCar();
            var userRepository = CreateCarRepository();
            //---------------Assert Precondition----------------
            var cars = Broker.GetBusinessObjectCollection<Car>("");
            Assert.AreEqual(3, cars.Count);
            //---------------Execute Test ----------------------
            var results = userRepository.GetCars();
            //---------------Test Result -----------------------
            Assert.AreEqual(3, results.Count);
        }

       /* [Test]
        public void GetCarBy_GivenCarId_ShouldReturnCar()
        {
            //---------------Set up test pack-------------------
            var car = new CarBuilder().WithNewId().Build();
            var userRepository = Substitute.For<ICarRepository>();
            var cars = new List<Car> { car };
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            userRepository.GetCarBy(car.CarId).Returns(car);
            //---------------Test Result -----------------------
            Assert.AreEqual(1, cars.Count);
            var actual = cars.First();
            Assert.AreSame(car, actual);
        }

        [Test]
        public void Save_GivenNewCar_ShouldSave()
        {
            //---------------Set up test pack-------------------
            var car = new CarBuilder().WithNewId().Build();
            var userRepository = Substitute.For<ICarRepository>();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            userRepository.Save(car);
            //---------------Test Result -----------------------
            userRepository.Received().Save(car);
        }

        [Test]
        public void Update_GivenExistingCar_ShouldUpdateAndSave()
        {
            //---------------Set up test pack-------------------
            var car = new CarBuilder().WithNewId().Build();
            var userRepository = Substitute.For<ICarRepository>();
            userRepository.GetCarBy(car.CarId).Returns(car);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            userRepository.Update(car, car);
            //---------------Test Result -----------------------
            userRepository.Received().Update(car, car);
        }

        [Test]
        public void Delete_GivenExistingCar_ShouldDeleteAndSave()
        {
            //---------------Set up test pack-------------------
            var car = new CarBuilder().WithNewId().Build();
            var userRepository = Substitute.For<ICarRepository>();
            userRepository.GetCarBy(car.CarId).Returns(car);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            userRepository.Delete(car);
            //---------------Test Result -----------------------
            userRepository.Received().Delete(car);
        }*/

        public static Car CreateSavedCar()
        {
            return new CarBuilder().BuildSaved();
        }

        private ICarRepository CreateCarRepository()
        {
            return TestUtils.Container.Resolve<ICarRepository>();
        }
    }
}