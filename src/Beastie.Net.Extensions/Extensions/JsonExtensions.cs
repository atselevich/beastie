using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Beastie.Net.Extensions.Extensions
{
    public static class JsonExtensions
    {
        private static readonly JsonSerializerSettings DefaultJsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new JsonConverter[]
                                {
                                     new StringEnumConverter
                                         {
                                             CamelCaseText = true
                                         }
                                }
        };

        /// <summary>
        /// Gets the dynamic from json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>The dynamic object</returns>
        public static dynamic GetDynamicFromJson(this string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                return JObject.Parse(json);
            }

            return new JObject();
        }

        /// <summary>
        ///  Uses JSON.NET instead Microsoft ASP.NET JSON. Deserializes JSON-formatted data into ECMAScript (JavaScript) types 
        /// </summary>
        /// <typeparam name="TObject">Object Type</typeparam>
        /// <param name="json">Serialized Object as string</param>
        /// <returns>deserialized object of type TObject</returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.Extensions\Extensions.Serialization.cs" title="Extensions.Serialization.cs" lang="C#" />
        /// </example>
        public static TObject DeserializeFromJson<TObject>(this string json) where TObject : class
        {
            if (!string.IsNullOrEmpty(json))
            {
                return JsonConvert.DeserializeObject<TObject>(json, DefaultJsonSerializerSettings);
            }

            return default(TObject);
        }

        /// <summary>
        /// Uses JSON.NET instead Microsoft ASP.NET JSON. Deserializes JSON-formatted data into ECMAScript (JavaScript) types
        /// </summary>
        /// <typeparam name="TObject">Object Type</typeparam>
        /// <param name="json">Serialized Object as string</param>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// deserialized object of type TObject
        /// </returns>
        public static TObject DeserializeFromJson<TObject>(this string json, JsonSerializerSettings settings) where TObject : class
        {
            if (!string.IsNullOrEmpty(json))
            {
                return JsonConvert.DeserializeObject<TObject>(json, settings);
            }

            return default(TObject);
        }

        /// <summary>
        /// Uses JSON.NET instead Microsoft ASP.NET JSON. Serialize Generic Object to the JavaScript Notation Object (JSON)
        /// </summary>
        /// <param name="value">The object value.</param>
        /// <returns>
        /// Serialized JSON string
        /// </returns>
        public static string SerializeToJson(this object value)
        {
            if (value != null)
            {
                return JsonConvert.SerializeObject(value, Formatting.None, DefaultJsonSerializerSettings);
            }

            return string.Empty;
        }

        /// <summary>
        /// Uses JSON.NET instead Microsoft ASP.NET JSON. Serialize Generic Object to the JavaScript Notation Object (JSON)
        /// </summary>
        /// <param name="value">The object value.</param>
        /// <param name="formatting">The formatting.</param>
        /// <returns>
        /// Serialized JSON string
        /// </returns>
        public static string SerializeToJson(this object value, Formatting formatting)
        {
            if (value != null)
            {
                return JsonConvert.SerializeObject(value, formatting, DefaultJsonSerializerSettings);
            }

            return string.Empty;
        }

        /// <summary>
        /// Reads the encrypted JSON base64.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="str">The STR.</param>
        /// <returns>Decrypted, deserialized object of type TObject </returns>
        //public static TObject ReadEncryptedJsonBase64<TObject>(this string str) where TObject : class
        //{
        //    if (!string.IsNullOrEmpty(str))
        //    {
        //        return str.DecryptAsDES().DeserializeFromJson<TObject>();
        //    }

        //    return default(TObject);
        //}

        ///// <summary>
        ///// Writes the encrypted JSON base64.
        ///// </summary>
        ///// <param name="obj">The obj.</param>
        ///// <returns>Encrypted, base 64 encoded JSON string</returns>
        //public static string WriteEncryptedJsonBase64(this object obj)
        //{
        //    if (obj != null)
        //    {
        //        return obj.SerializeToJson().EncryptDES();
        //    }

        //    return string.Empty;
        //}

    }
}