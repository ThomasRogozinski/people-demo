using System.Collections.Generic;
using System.Configuration;

using IO.Swagger.Api;
using IO.Swagger.Model;
using Newtonsoft.Json;

namespace PeopleUtils {
    /// <summary>
    /// This interface and DataFromString is only required for the purpose of Unit Test
    /// </summary>
    public interface IDataProvider {
        List<Person> LoadPeople();
    }

    /// <summary>
    /// RESTFull Api client implementation
    /// </summary>
    public class DataFromWeb : IDataProvider {
        private static string basePath = ConfigurationManager.AppSettings["BasePath"];
        /// <summary>
        /// Loads list of people from API
        /// </summary>
        /// <returns>A list of people and their attributes</returns>
        public List<Person> LoadPeople() {
            return new PeopleApi(basePath).GetPeople();
        }
    }

    /// <summary>
    /// Sample implementation of the static data provider for the purpose of Unit Test.
    /// It could've been data loaded from file, constructed programmatically, etc...
    /// </summary>
    public class DataFromString : IDataProvider {
        /// <summary>
        /// Loads list of people from hardcoded string
        /// </summary>
        /// <returns>A list of people and their attributes</returns>
        public List<Person> LoadPeople() {
            return JsonConvert.DeserializeObject<List<Person>>("[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"GarfieldFromString\",\"type\":\"Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]},{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"}]},{\"name\":\"Steve\",\"gender\":\"Male\",\"age\":45,\"pets\":null},{\"name\":\"Fred\",\"gender\":\"Male\",\"age\":40,\"pets\":[{\"name\":\"Tom\",\"type\":\"Cat\"},{\"name\":\"Max\",\"type\":\"Cat\"},{\"name\":\"Sam\",\"type\":\"Dog\"},{\"name\":\"Jim\",\"type\":\"Cat\"}]},{\"name\":\"Samantha\",\"gender\":\"Female\",\"age\":40,\"pets\":[{\"name\":\"TabbyFromString\",\"type\":\"Cat\"}]}]");
        }
    }
}
