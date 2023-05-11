using NUnit.Framework;

namespace Nevter.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void TestTest(){
            Assert.AreEqual(10, 10);
        }

        [Test]
        public void TestTest2(){
            Assert.AreEqual(10, 9);
        }
    }
}