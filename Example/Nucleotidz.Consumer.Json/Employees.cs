using Newtonsoft.Json;

namespace com.nucleotidz.rifle
{
    public class Employees
    {
        [JsonRequired] // use Newtonsoft.Json annotations
        [JsonProperty("name")]
        public string Name { get; set; }
    }
    
}
namespace com.nucleotidz.rifle.key
{
    public class EmployeeKey
    {
        [JsonRequired] // use Newtonsoft.Json annotations
        [JsonProperty("Id")]
        public string Id { get; set; }
    }
}