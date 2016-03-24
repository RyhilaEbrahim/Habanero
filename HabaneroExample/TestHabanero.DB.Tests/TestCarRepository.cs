using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using TestHabanero.BO;
using TestHabanero.BO.Tests.Util;
using TestHabanero.Tests.Commons;
using TestHabenaro.Db.Interfaces;

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

        [Test]
        public void GetCars_GivenOneCar_ShouldReturnCar()
        {
            //---------------Set up test pack-------------------
            var car = new CarBuilder().WithNewId().Build();
            var userRepository = Substitute.For<ICarRepository>();
            var cars = new List<Car> { car };
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            userRepository.GetCars().Returns(cars);
            //---------------Test Result -----------------------
            Assert.AreEqual(1, cars.Count);
            var actual = cars.First();
            Assert.AreSame(car, actual);
        }

        [Test]
        public void GetCars_GivenTwoCars_ShouldReturnCar()
        {
            //---------------Set up test pack-------------------
            var car1 = new CarBuilder().WithNewId().Build();
            var car2 = new CarBuilder().WithNewId().Build();
            
            var userRepository = Substitute.For<ICarRepository>();
            var cars = new List<Car> {car1, car2};
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            userRepository.GetCars().Returns(cars);
            //---------------Test Result -----------------------
            Assert.AreEqual(2, cars.Count);

        }

        [Test]
        public void GetCars_GivenThreeCars_ShouldReturnCar()
        {
            //---------------Set up test pack-------------------
            var car1 = new CarBuilder().WithNewId().Build();
            var car2 = new CarBuilder().WithNewId().Build();
            var car3 = new CarBuilder().WithNewId().Build();
            var userRepository = Substitute.For<ICarRepository>();
            var cars = new List<Car> { car1,car2,car3 };
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            userRepository.GetCars().Returns(cars);
            //---------------Test Result -----------------------
            Assert.AreEqual(3, cars.Count);
           
        }

        [Test]
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
           userRepository.Received().Update(car,car);
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
        }

       
    }
}