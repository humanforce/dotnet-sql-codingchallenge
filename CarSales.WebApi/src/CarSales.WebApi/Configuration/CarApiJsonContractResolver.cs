using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace CarSales.WebApi
{
    public class CarApiJsonContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (member.GetCustomAttribute<InversePropertyAttribute>() != null)
            {
                property.ShouldSerialize =
                    instance => false;
            }

            return property;
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return string.Empty;

            return $"{char.ToLower(propertyName[0])}{propertyName.Substring(1)}";
        }
    }
}
