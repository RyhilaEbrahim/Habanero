using System;
using System.Configuration;
using Habanero.BO;
using Habanero.DB;
using Habanero.Testability;
using TestHabanero.BO;
using TestHabanero.Migrations;
using BORegistry = Habanero.BO.BORegistry;

namespace TestHabanero.Tests.Commons
{
    public class CarBuilder 
    {
        private readonly Car _car;
        public CarBuilder()
        {
            _car = BuildValid();
            _car.Make = RandomValueGen.GetRandomString();
            _car.Model = RandomValueGen.GetRandomString();
        }
        
        private BOTestFactory<T> CreateFactory<T>() where T : BusinessObject
        {
            BOBroker.LoadClassDefs();
            return new BOTestFactory<T>();
        }

      
        private Car BuildValid()
        {
            var car = CreateFactory<Car>()
                .SetValueFor(c => c.Model)
                .CreateValidBusinessObject();
            return car;
        }

        public CarBuilder WithMake(string make)
        {
            _car.Make = make;
            return this;
        }
        public CarBuilder WithNewId()
        {
            _car.CarId = Guid.NewGuid();
            return this;
        }
        
        public Car BuildSaved()
        {
            _car.Save();
            return _car;
        }
        
        public Car Build()
        {
            
            return _car;
        }
    }
}