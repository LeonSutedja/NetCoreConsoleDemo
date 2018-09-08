using Newtonsoft.Json;

namespace NetCoreConsoleDemo
{
    public static class SerializerObjectExtension
    {
        public static string ToJson<T>(this T toSerialize)
        {
            return JsonConvert.SerializeObject(toSerialize);
        }
    }
}