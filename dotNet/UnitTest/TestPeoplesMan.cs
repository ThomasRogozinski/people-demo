using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

using PeopleUtils;

namespace UnitTest {
    [TestClass]
    public class TestPeoplesMan {

        private IUnityContainer container;
        public TestPeoplesMan() {
            container = new UnityContainer();
            container.LoadConfiguration();
            IDataProvider dataProvider = container.Resolve<IDataProvider>("DataFromString");
            container.RegisterInstance<IDataProvider>(dataProvider);
        }

        [TestMethod]
        public void TestCatsExtraction() {
            PeoplesMan manager = container.Resolve<PeoplesMan>();
            Dictionary<string, List<string>> cats = manager.ExtractCats();

            //Verify that Extractor worked as expected
            Assert.IsTrue(cats["Male"].Count == 4 && cats["Female"].Count == 2, "Number of Cats belonging to Male and Female");
            Assert.IsTrue(cats["Male"][0] == "GarfieldFromString" && cats["Male"][1] == "Jim" && cats["Male"][2] == "Max" && cats["Male"][3] == "Tom", "Names of Cats belonging to Male");
            Assert.IsTrue(cats["Female"][0] == "Garfield" && cats["Female"][1] == "TabbyFromString", "Names of Cats belonging to Female");
        }
    }
}
