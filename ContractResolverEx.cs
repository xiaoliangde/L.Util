using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace L.Util
{
    public class ContractResolverEx<TAttribute> : DefaultContractResolver where TAttribute : Attribute
    {
        private Func<IList<JsonProperty>, IList<JsonProperty>> WhenCreateProperties { get; set; }

        public ContractResolverEx()
        {
            WhenCreateProperties = list => list.Where(t => t.AttributeProvider.GetAttributes(true).Any(g => g is TAttribute))
                .ToList();
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            //IList<JsonProperty> list = base.CreateProperties(type, memberSerialization)
            //    .Where(t => t.AttributeProvider.GetAttributes(true).Any(g => g is TAttribute))
            //    .ToList();
            return WhenCreateProperties == null ? base.CreateProperties(type, memberSerialization) : WhenCreateProperties?.Invoke(base.CreateProperties(type, memberSerialization));
        }

        protected override IList<JsonProperty> CreateConstructorParameters(ConstructorInfo constructor, JsonPropertyCollection memberProperties)
        {
            return base.CreateConstructorParameters(constructor, memberProperties);
        }
    }
}
