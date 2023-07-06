using cms.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ecommerce.Helpers
{
    public class NewtonsoftSerialize
    {
	public static string SerializeData(object data)
	{
	    var settings = new JsonSerializerSettings
	    {
		ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
		ContractResolver = new CamelCasePropertyNamesContractResolver(),
		NullValueHandling = NullValueHandling.Ignore,
	    };

	    return JsonConvert.SerializeObject(data, settings);
	}
    }
}
