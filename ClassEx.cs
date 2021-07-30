using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LZ4;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace L.Util
{
    public static class ClassEx
    {
        static readonly Type EnumType = typeof(System.Enum);
        public static bool HaveEventHandler<T>(this T instance, string eventName, string methodName)
        {
            return typeof(T).GetEvent(eventName).GetOtherMethods().Any();
        }
        public static TModel ToEntity<TModel>(this NameValueCollection map,string prefix = null) where TModel : new()
        {
            var inst = new TModel();
            var properties = inst.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var pi in properties)
            {
                var si = pi.GetSetMethod();
                if (si == null) continue;

                var strVal = map[string.IsNullOrEmpty(prefix)? pi.Name:$"{prefix}{pi.Name}"];
                if (string.IsNullOrEmpty(strVal)) continue;

                pi.SetValue(inst,
                    pi.PropertyType.IsSubclassOf(EnumType)
                        ? Enum.Parse(pi.PropertyType, $"{strVal}")
                        : Convert.ChangeType(strVal, pi.PropertyType), null);
            }

            return inst;
        }

        public static TModel ToEntity<TModel>(this IDictionary<string,object> map, string prefix = null) where TModel : new()
        {
            var inst = new TModel();
            var properties = inst.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var pi in properties)
            {
                var si = pi.GetSetMethod();
                if (si == null) continue;

                var val = map[string.IsNullOrEmpty(prefix) ? pi.Name : $"{prefix}{pi.Name}"];
                if (val == null) continue;

                pi.SetValue(inst,val, null);
            }

            return inst;
        }

        public static TOther ToType<TOther>(this object current) 
        {
            return (TOther)current;
        }

        public static string FormatValue(this object value)
        {
            if (value == null) return "null";
            if (value is string)
                return string.Format("'{0}'", value).Replace("\r\n", "\\n");
            return string.Format(CultureInfo.InvariantCulture, "{0}", value);
        }

        public static TOther AsType<TOther>(this object current) where TOther : class
        {
            return current as TOther;
        }

        public static Dictionary<string,object> ToMap<TModel>(this TModel obj, string prefix = null) where TModel : new()
        {
            var properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            return properties.ToDictionary(pi => string.IsNullOrEmpty(prefix) ? pi.Name : $"{prefix}{pi.Name}", pi => (pi is IConvertible) ?pi.GetValue(obj):obj.ToMap(prefix));
        }

        /// <summary>
        /// 目标类可以是子类
        /// </summary>
        /// <param name="sourceBase"></param>
        /// <param name="destinueObj"></param>
        public static TBean CopyTo<TBean>(this object sourceBase, TBean destinueObj)
        {
            if (sourceBase == null || destinueObj == null)return destinueObj;
            var sType = sourceBase.GetType();
            var dType = destinueObj.GetType();
            //if (!sType.Equals(dType) && !dType.IsSubclassOf(sType)) return destinueObj;
            PropertyInfo[] sProperties = null;
            if (sType == dType || dType.IsSubclassOf(sType)) sProperties = sType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            else if(sType.IsSubclassOf(dType)) sProperties = dType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            else throw new InvalidCastException("类型不一致");

            foreach (var pi in sProperties)
            {
                var si = pi.GetSetMethod();
                if (si == null) continue;
                pi.SetValue(destinueObj, pi.GetValue(sourceBase), null);
            }
            return destinueObj;
        }

        public static void CopyToByForce(this object sourceObj, object destinueObj,bool ignoreCase = false)
        {
            if (sourceObj == null || destinueObj == null) return;
            var sType = sourceObj.GetType();
            var dType = destinueObj.GetType();

            var sProperties = TypeDescriptor.GetProperties(sType);
            var dProperties = TypeDescriptor.GetProperties(dType);

            foreach (PropertyDescriptor dPd in dProperties)
            {
                var sPd = sProperties.Find(dPd.Name, ignoreCase);
                if (sPd == null) continue;
                if(dPd.PropertyType != sPd.PropertyType)continue;

                //Dynamitey.InvokeGet(component, propertyName);
                dPd.SetValue(destinueObj, sPd.GetValue(sourceObj));
            }
        }

        public static void CopyToExceptByForce<TModel>(this object sourceObj, TModel destinueObj,bool ignoreCase = false, params string[] dPropertiesExcept)
        {
            if (sourceObj == null || destinueObj == null) return;
            var sType = sourceObj.GetType();
            var dType = destinueObj.GetType();

            var sProperties = TypeDescriptor.GetProperties(sType);
            var dProperties = TypeDescriptor.GetProperties(dType);

            foreach (PropertyDescriptor dPd in dProperties)
            {
                if(dPropertiesExcept.Any(t=>t == dPd.Name))continue;
                var sPd = sProperties.Find(dPd.Name, ignoreCase);
                if (sPd == null) continue;
                if (dPd.PropertyType != sPd.PropertyType) continue;

                //Dynamitey.InvokeGet(component, propertyName);
                dPd.SetValue(destinueObj, sPd.GetValue(sourceObj));
            }
        }

        public static TModel CloneToExcept<TModel>(this object sourceBase, params Expression<Func<TModel, object>>[] exprs) where TModel : class, new()
        {
            var destinueObj = new TModel();
            return CopyObjectToExcept(sourceBase, destinueObj, GetPropertiesName(exprs)) as TModel;
        }

        public static TModel CopyToExcept<TModel>(this (object sourceBase, TModel destinueObj) pairTuple, params Expression<Func<TModel, object>>[] exprs) where TModel : class
        {
            return CopyObjectToExcept(pairTuple.sourceBase, pairTuple.destinueObj, GetPropertiesName(exprs)) as TModel;
        }

        public static TModel CopyToExcept<TModel>(this object sourceBase, TModel destinueObj, params Expression<Func<TModel, object>>[] exprs) where TModel : class
        {
            return CopyObjectToExcept(sourceBase, destinueObj, GetPropertiesName(exprs)) as TModel;
        }

        public static object CopyObjectToExcept(this object sourceBase, object destinueObj,string[] propertiesExcept)
        {
            if (sourceBase == null || destinueObj == null) return destinueObj;
            var sType = sourceBase.GetType();
            var dType = destinueObj.GetType();
            //if (!sType.Equals(dType) && !dType.IsSubclassOf(sType)) return destinueObj;
            PropertyInfo[] sProperties = null;
            if (sType == dType || dType.IsSubclassOf(sType)) sProperties = sType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            else if (sType.IsSubclassOf(dType)) sProperties = dType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            else throw new InvalidCastException("类型不一致");

            foreach (var pi in sProperties)
            {
                if(propertiesExcept.Any(t=>t == pi.Name))continue;
                var si = pi.GetSetMethod();
                if (si == null) continue;
                pi.SetValue(destinueObj, pi.GetValue(sourceBase), null);
            }
            return destinueObj;
        }

        /// <summary>
        /// 模版必须是自己或者派生类
        /// </summary>
        /// <typeparam name="TModel">必须是自己或者派生类</typeparam>
        /// <param name="sourceBase"></param>
        /// <returns></returns>
        public static TModel Clone<TModel>(this TModel sourceBase) where TModel : new()
        {
            TModel t = new TModel();
            return (TModel) sourceBase.CopyTo(t);
        }

        public static byte[] Compress(this byte[] buffer)
        {
            return LZ4Codec.Wrap(buffer);
        }

        public static byte[] UnCompress(this byte[] buffer)
        {
            return LZ4Codec.Unwrap(buffer);
        }

        public static TModel ToEntityFromJson<TModel>(this string objectContent, JsonSerializerSettings settings = null)
        {
            return JsonConvert.DeserializeObject<TModel>(objectContent, settings);
            //return SimpleJson.SimpleJson.DeserializeObject<TModel>(objectContent,new PocoJsonSerializerStrategy{});
        }

        public static TModel ToEntityFromJson<TModel>(this string objectContent, TypeNameHandling typeNameHanding, IContractResolver contractResolver = null)
        {
            return JsonConvert.DeserializeObject<TModel>(objectContent, new JsonSerializerSettings { TypeNameHandling = typeNameHanding, ContractResolver = contractResolver });
        }

        public static object ToEntityFromJson(this string objectContent, Type type, TypeNameHandling typeNameHanding, IContractResolver contractResolver = null)
        {
            return JsonConvert.DeserializeObject(objectContent, type, new JsonSerializerSettings { TypeNameHandling = typeNameHanding, ContractResolver = contractResolver });
        }

        public static object ToEntityFromJson(this string objectContent, Type type, JsonSerializerSettings settings = null)
        {
            return JsonConvert.DeserializeObject(objectContent, type, settings);
        }

        public static string ToJsonString(this object objectEntity, TypeNameHandling typeNameHanding, IContractResolver contractResolver = null)
        {
            return JsonConvert.SerializeObject(objectEntity, new JsonSerializerSettings { TypeNameHandling = typeNameHanding, ContractResolver = contractResolver });
        }

        public static string ToJsonString(this object objectEntity, JsonSerializerSettings settings = null)
        {
            return JsonConvert.SerializeObject(objectEntity, settings);
        }

        public static string[] GetPropertiesName<TModel>(params Expression<Func<TModel, object>>[] exprs)
        {
            var temp = new string[exprs.Length];
            for (var i = 0; i < exprs.Length; i++) temp[i] = GetPropertyName<TModel>(exprs[i]);
            return temp;
        }

        public static string GetPropertyName<TModel>(Expression<Func<TModel, object>> expr)
        {
            var rtn = expr.Body switch
            {
                UnaryExpression expression => ((MemberExpression) expression.Operand).Member.Name,
                MemberExpression expression => expression.Member.Name,
                ParameterExpression parameterExpression => parameterExpression.Type.Name,
                _ => string.Empty
            };
            return rtn;
        }

        public static PropertyDescriptor[] GetPropertiesDescriptor<TModel>(params Expression<Func<TModel, object>>[] exprs)
        {
            var properties = TypeDescriptor.GetProperties(typeof(TModel));
            var temp = new PropertyDescriptor[exprs.Length];
            for (var i = 0; i < exprs.Length; i++) temp[i] = properties.Find(GetPropertyName<TModel>(exprs[i]), false);
            return temp;
        }

        public static TAttribute GetPropertyAttribute<TModel, TAttribute>(Expression<Func<TModel, object>> expr) where TAttribute : Attribute
        {
            return typeof(TModel).GetProperty(GetPropertyName(expr)).GetCustomAttribute<TAttribute>();
        }

        public static TAttribute GetEnumAttribute<TAttribute>(this Enum em) where TAttribute : Attribute
        {
            var type = em.GetType();
            var fieldInfo = type.GetField(em.ToString());
            if (fieldInfo == null) return null;
            return fieldInfo.GetCustomAttribute<TAttribute>();
        }

        /// <summary>
        /// 未指定TAttribute则默认DescriptionAttribute
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static string GetPropertyAttribute<TModel>(Expression<Func<TModel, object>> expr)
        {
            return typeof(TModel).GetProperty(GetPropertyName(expr)).GetCustomAttribute<DescriptionAttribute>().Description;
        }

        public static (string PropertyName, string Description) GetPropertyAndDescribeAttribute<TModel>(Expression<Func<TModel, object>> expr)
        {
            var property = typeof(TModel).GetProperty(GetPropertyName(expr));
            var description = property.GetCustomAttribute<DescriptionAttribute>().Description;
            return (property.Name, description);
        }

        public static IEnumerable<(string PropertyName, string Description)> GetPropertiesAndDescriptions<TModel>()
        {
            return typeof(TModel).GetProperties().Select(t => (t.Name, t.GetCustomAttribute<DescriptionAttribute>().Description));
        }

        /// <summary>
        /// 根据属性名获取属性值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="name">属性名</param>
        /// <returns>属性的值</returns>
        public static object GetPropertyValue<T>(this T t, string name)
        {
            Type type = t.GetType();
            PropertyInfo p = type.GetProperty(name);
            if (p == null)
            {
                throw new Exception(String.Format("该类型没有名为{0}的属性", name));
            }
            var param_obj = Expression.Parameter(typeof(T));
            var param_val = Expression.Parameter(typeof(object));

            //转成真实类型，防止Dynamic类型转换成object
            var body_obj = Expression.Convert(param_obj, type);

            var body = Expression.Property(body_obj, p);
            var getValue = Expression.Lambda<Func<T, object>>(body, param_obj).Compile();
            return getValue(t);
        }

        public static object GetPropertyValue2(this object instance, string name)
        {
            Type type = instance.GetType();
            PropertyInfo p = type.GetProperty(name);
            if(p == null) throw new Exception($"Not exist {name}");
            return p.GetValue(instance);
        }

        /// <summary>
        /// 根据属性名称设置属性的值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="name">属性名</param>
        /// <param name="value">属性的值</param>
        public static void SetPropertyValue<T>(this T t, string name, object value)
        {
            Type type = t.GetType();
            PropertyInfo p = type.GetProperty(name);
            if (p == null)
            {
                throw new Exception(String.Format("该类型没有名为{0}的属性", name));
            }
            var param_obj = Expression.Parameter(type);
            var param_val = Expression.Parameter(typeof(object));
            var body_obj = Expression.Convert(param_obj, type);
            var body_val = Expression.Convert(param_val, p.PropertyType);

            //获取设置属性的值的方法
            var setMethod = p.GetSetMethod(true);

            //如果只是只读,则setMethod==null
            if (setMethod != null)
            {
                var body = Expression.Call(param_obj, p.GetSetMethod(), body_val);
                var setValue = Expression.Lambda<Action<T, object>>(body, param_obj, param_val).Compile();
                setValue(t, value);
            }
        }

        public static void SetPropertyValue2(this object instance, string name, object value)
        {
            Type type = instance.GetType();
            PropertyInfo p = type.GetProperty(name);
            p.SetValue(instance,value);
        }


        public static List<Type> GetImplTypes(this Type basicType, IEnumerable<Type> types)
        {
            return types.Where(t => t.IsClass && !t.IsAbstract && basicType.IsAssignableFrom(t) && !t.ContainsGenericParameters).ToList();
        }
    }
}