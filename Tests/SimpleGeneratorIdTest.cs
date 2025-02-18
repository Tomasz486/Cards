using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Cards.Tests
{
    [TestClass]
    public class SimpleGeneratorIdTest
    {
        [TestMethod]
        public void GenerateUniqueIdLengthTest()
        {
            var simpleGeneratorId = new SimpleGeneratorId();

            var newId = simpleGeneratorId.GenerateUniqueId();

            Assert.AreEqual(newId.Length, 32);
        }

        [TestMethod]
        public void CheckIfIdIsUniqueTest()
        {
            var simpleGeneratorId = new SimpleGeneratorId();

            var newId1 = simpleGeneratorId.GenerateUniqueId();
            var newId2 = simpleGeneratorId.GenerateUniqueId();

            Assert.AreNotEqual(newId1, newId2);
        }
    }
}
