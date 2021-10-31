using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

using Newtonsoft.Json;
using PeopleUtils;

namespace TestApp {
    class Program {
        private static IUnityContainer container;

        static void Main(string[] args) {
            try {
                InitializeContainer();

                //create manager object and load it with data
                PeoplesMan manager = container.Resolve<PeoplesMan>();
                //extract cats in required format
                Dictionary<string, List<string>> cats = manager.ExtractCats();

                //output cats in json format
                Console.WriteLine(JsonConvert.SerializeObject(cats, Formatting.Indented));
            } catch (Exception ex) {
                Console.WriteLine("Upssss, something went wrong " + ex.Message);
            }
        }

        /// <summary>
        /// Unity container is used to provide ability to switch between different data sources.
        /// Main App works of live Api whereas Unit Tests works of static data 
        /// </summary>
        private static void InitializeContainer() {
            container = new UnityContainer();
            container.LoadConfiguration();
            IDataProvider dataProvider = container.Resolve<IDataProvider>("DataFromWeb");
            container.RegisterInstance<IDataProvider>(dataProvider);
        }
    }
}
