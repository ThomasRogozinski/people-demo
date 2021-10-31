using System.Collections.Generic;
using System.Linq;

using IO.Swagger.Model;

namespace PeopleUtils {
    /// <summary>
    /// People's Manager is a utility class used to extract various type information out of List of People
    /// </summary>
    public class PeoplesMan {
        private List<Person> people;

        /// <summary>
        /// Load list of people from registered data source
        /// </summary>
        public PeoplesMan(IDataProvider dataProvider) {
            this.people = dataProvider.LoadPeople();
        }

        /// <summary>
        /// Extracts information about all available cats for gender
        /// </summary>
        /// <returns>A dictionary where key is Gender and value is a list of cat's names in alphabetical order</returns>
        public Dictionary<string, List<string>> ExtractCats() {
            return people
                .Where(p => p.Pets != null)                                                                     //filter out people having null pets array 
                .SelectMany(p => p.Pets, (parent, child) => new { parent.Gender, child.Name, child.Type })      //flatten array of people to array of pet object
                .Where(c => c.Type == "Cat")                                                                    //filter out pets which are not Cats
                .GroupBy(c => c.Gender)                                                                         //group by gender
                .OrderByDescending(g => g.Key)                                                                  //just to get Males before Females like in the sample output presented
                .ToDictionary(g => g.Key, g => g.Select(i => i.Name).OrderBy(i => i).ToList() )                 //remap object by creating dictionary where key is Gender and value list of Cats
                ;
        }
    }
}
