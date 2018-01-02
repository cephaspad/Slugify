using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CephasPAD.Utilities.Slugify.Tests
{
    [TestClass]
    public class SlugifyTest
    {
        [TestMethod]
        public void Slugify_GeneralTest()
        {
            Assert.AreEqual("hello-world", "Hello World".Slugify());
            Assert.AreEqual("hello_world", "Hello World".Slugify("_"));
        }
    }
}