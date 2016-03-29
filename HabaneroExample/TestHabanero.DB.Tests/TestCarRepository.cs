using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using Habanero.BO;
using NSubstitute;
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
            var carRepository = CreateCarRepository();
            //---------------Assert Precondition----------------
            var cars = Broker.GetBusinessObjectCollection<Car>("");
            Assert.AreEqual(2, cars.Count);
            //---------------Execute Test ----------------------
            var results = carRepository.GetCars();
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
            var carRepository = CreateCarRepository();
            //---------------Assert Precondition----------------
            var cars = Broker.GetBusinessObjectCollection<Car>("");
            Assert.AreEqual(3, cars.Count);
            //---------------Execute Test ----------------------
            var results = carRepository.GetCars();
            //---------------Test Result -----------------------
            Assert.AreEqual(3, results.Count);
        }

        [Test]
        public void GetCarBy_GivenCarId_ShouldReturnCar()
        {
            //---------------Set up test pack-------------------
            var car = CreateSavedCar();
            var carRepository = CreateCarRepository();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = carRepository.GetCarBy(car.CarId);
            //---------------Test Result -----------------------
            Assert.AreEqual(car, results);
        }

        [Test]
        public void Save_ShouldSaveCarToRepo()
        {
            //---------------Set up test pack-------------------
            var newCar = new CarBuilder().BuildSaved();
            var userRepository = Substitute.For<ICarRepository>();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            userRepository.Save(newCar);
            //---------------Test Result -----------------------
            var cars = Broker.GetBusinessObjectCollection<Car>("");
            Assert.AreEqual(1, cars.Count);
            var car = cars[0];
            Assert.AreSame(newCar, car);
        }

        [Test]
        public void Update_GivenEditedPerson_ShouldUpdatePersonDetails()
        {
            //---------------Set up test pack-------------------
            var savedCar = CreateSavedCar();
            var repository = CreateCarRepository();
            var model = Mapper.Map<Car>(savedCar);
            const string make = "some make";
            model.Make = make;
            var carFromRepo = repository.GetCarBy(model.CarId);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            repository.Update(model, carFromRepo);
            //---------------Test Result -----------------------
            Assert.IsNotNull(carFromRepo);
            Assert.AreEqual(make, carFromRepo.Make);
        }

        [Test]
        public void Delete_GivenExistingCar_ShouldDeleteAndSave()
        {
            //---------------Set up test pack-------------------
            var car = CreateSavedCar();
            var userRepository = CreateCarRepository();
            //---------------Assert Precondition----------------
            var cars = Broker.GetBusinessObjectCollection<Car>("");
            Assert.AreEqual(1, cars.Count);
            //---------------Execute Test ----------------------
            userRepository.Delete(car);
            //---------------Test Result -----------------------
            Assert.AreEqual(0, cars.Count);
        }

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