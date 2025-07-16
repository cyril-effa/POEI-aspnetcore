using TDD_RomanNum;

namespace TDD_RomanNum_TESTS
{
    [TestClass]
    public sealed class RomanNumTests
    {
        [TestMethod]
        public void CreateConverter()
        {
            var converter = new RomanNum();

            Assert.IsNotNull(converter);
        }

        #region ConvertToRoman Tests

        [TestMethod]
        public void ConvertToRoman_1()
        {
            var converter = new RomanNum();
            var result = converter.ConvertToRoman(1);
            Assert.AreEqual("I", result);
        }
        
        [TestMethod]
        public void ConvertToRoman_7()
        {
            var converter = new RomanNum();
            var result = converter.ConvertToRoman(7);
            Assert.AreEqual("VII", result);
        }

        [TestMethod]
        public void ConvertToRoman_10()
        {
            var converter = new RomanNum();
            var result = converter.ConvertToRoman(10);
            Assert.AreEqual("X", result);
        }

        [TestMethod]
        public void ConvertToRoman_49()
        {
            var converter = new RomanNum();
            var result = converter.ConvertToRoman(49);
            Assert.AreEqual("XLIX", result);
        }

        [TestMethod]
        public void ConvertToRoman_847()
        {
            var converter = new RomanNum();
            var result = converter.ConvertToRoman(847);
            Assert.AreEqual("DCCCXLVII", result);
        }

        [TestMethod]
        public void ConvertToRoman_1053()
        {
            var converter = new RomanNum();
            var result = converter.ConvertToRoman(1053);
            Assert.AreEqual("MLIII", result);
        }

        [TestMethod]
        public void ConvertToRoman_1776()
        {
            var converter = new RomanNum();
            var result = converter.ConvertToRoman(1776);
            Assert.AreEqual("MDCCLXXVI", result);
        }

        [TestMethod]
        public void ConvertToRoman_2018()
        {
            var converter = new RomanNum();
            var result = converter.ConvertToRoman(2018);
            Assert.AreEqual("MMXVIII", result);
        }

        #endregion

        #region ConvertToNum Tests

        [TestMethod]
        public void ConvertToNum_1()
        {
            var converter = new RomanNum();
            var result = converter.ConvertToNum("I");
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void ConvertToNum_7()
        {
            var converter = new RomanNum();
            var result = converter.ConvertToNum("VII");
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void ConvertToNum_10()
        {
            var converter = new RomanNum();
            var result = converter.ConvertToNum("X");
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void ConvertToNum_49()
        {
            var converter = new RomanNum();
            var result = converter.ConvertToNum("XLIX");
            Assert.AreEqual(49, result);
        }

        [TestMethod]
        public void ConvertToNum_847()
        {
            var converter = new RomanNum();
            var result = converter.ConvertToNum("DCCCXLVII");
            Assert.AreEqual(847, result);
        }

        [TestMethod]
        public void ConvertToNum_1053()
        {
            var converter = new RomanNum();
            var result = converter.ConvertToNum("MLIII");
            Assert.AreEqual(1053, result);
        }

        [TestMethod]
        public void ConvertToNum_1776()
        {
            var converter = new RomanNum();
            var result = converter.ConvertToNum("MDCCLXXVI");
            Assert.AreEqual(1776, result);
        }

        [TestMethod]
        public void ConvertToNum_2018()
        {
            var converter = new RomanNum();
            var result = converter.ConvertToNum("MMXVIII");
            Assert.AreEqual(2018, result);
        }

        #endregion
    }
}
