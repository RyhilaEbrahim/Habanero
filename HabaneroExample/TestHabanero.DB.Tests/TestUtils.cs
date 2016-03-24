using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Habanero.BO;
using Habanero.DB;

namespace TestHabanero.BO.Tests.Util
{
    class TestUtils
    {
        public static void SetupFixture()
        {

            Habanero.BO.BORegistry.DataAccessor = new DataAccessorInMemory();
            BOBroker.LoadClassDefs();
        }
    }
}
