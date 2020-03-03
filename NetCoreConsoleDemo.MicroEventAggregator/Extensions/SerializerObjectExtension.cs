using Newtonsoft.Json;

namespace NetCoreConsoleDemo.MicroEventAggregator.Extensions
{
    public static class SerializerObjectExtension
    {
        public static string ToJson<T>(this T toSerialize)
        {
            return JsonConvert.SerializeObject(toSerialize);
        }
    }
}