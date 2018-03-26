using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Beastie.Net.Extensions.Extensions
{
    public static class ReflectionExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Vopies property values from one instance of an object to another, used to avoid reference pointers for reference
        ///     types
        /// </summary>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <param name="destination">
        ///     The destination.
        /// </param>
        public static void CopyPropertyValuesTo(this object source, object destination)
        {
            var destProperties = destination.GetType().GetProperties();

            foreach (var sourceProperty in source.GetType().GetProperties())
            {
                foreach (var destProperty in destProperties)
                {
                    if (destProperty.Name == sourceProperty.Name
                        && destProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType)
                        && destProperty.CanWrite)
                    {
                        destProperty.SetValue(
                            destination,
                            sourceProperty.GetValue(source, new object[] { }),
                            new object[] { });

                        break;
                    }
                }
            }
        }

        /// <summary>
        ///     The get default.
        /// </summary>
        /// <param name="type">
        ///     The type.
        /// </param>
        /// <returns>
        ///     The <see cref="object" />.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static object GetDefault(this Type type)
        {
            // If no Type was supplied, if the Type was a reference type, or if the Type was a System.Void, return null
            if (type == null || !type.IsValueType || type == typeof(void))
            {
                return null;
            }

            // If the supplied Type has generic parameters, its default value cannot be determined
            if (type.ContainsGenericParameters)
            {
                throw new ArgumentException(
                    "{" + MethodBase.GetCurrentMethod() + "} Error:\n\nThe supplied value type <" + type
                    + "> contains generic parameters, so the default value cannot be retrieved");
            }

            // If the Type is a primitive type, or if it is another publicly-visible value type (i.e. struct/enum), return a 
            // default instance of the value type
            if (type.IsPrimitive || !type.IsNotPublic)
            {
                try
                {
                    return Activator.CreateInstance(type);
                }
                catch (Exception e)
                {
                    throw new ArgumentException(
                        "{" + MethodBase.GetCurrentMethod()
                        + "} Error:\n\nThe Activator.CreateInstance method could not "
                        + "create a default instance of the supplied value type <" + type
                        + "> (Inner Exception message: \"" + e.Message + "\")",
                        e);
                }
            }

            // Fail with exception
            throw new ArgumentException(
                "{" + MethodBase.GetCurrentMethod() + "} Error:\n\nThe supplied value type <" + type
                + "> is not a publicly-visible type, so the default value cannot be retrieved");
        }

        public static string GetMethodSignature<TObject>(this Func<TObject> method)
        {
            var signature = string.Empty;
            var info = method.GetMethodInfo();

            var sb = new StringBuilder();
            sb.AppendFormat("{0} {1}", info.ReturnType.Name, info.Name);
            if (info.IsGenericMethod)
            {
                sb.AppendFormat("<{0}>", string.Join(", ", info.GetGenericArguments().Select(x => x.Name)));
            }

            sb.AppendFormat(
                "({0})",
                string.Join(
                    ", ",
                    info.GetParameters().Select(x => $"{x.ParameterType.Name} {x.Name}")));

            return signature;
        }

        /// <summary>
        ///     Returns a private Property Value from a given Object. Uses Reflection.
        ///     Throws a ArgumentOutOfRangeException if the Property is not found.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the Property
        /// </typeparam>
        /// <param name="obj">
        ///     Object from where the Property Value is returned
        /// </param>
        /// <param name="propName">
        ///     Propertyname as string.
        /// </param>
        /// <returns>
        ///     PropertyValue
        /// </returns>
        public static T GetPrivateFieldValue<T>(this object obj, string propName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            var t = obj.GetType();
            FieldInfo fi = null;
            while (fi == null && t != null)
            {
                fi = t.GetField(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                t = t.BaseType;
            }

            if (fi == null)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(propName),
                    $"Field {propName} was not found in Type {obj.GetType().FullName}");
            }

            return (T) fi.GetValue(obj);
        }

        /// <summary>
        ///     Returns a _private_ Property Value from a given Object. Uses Reflection.
        ///     Throws a ArgumentOutOfRangeException if the Property is not found.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the Property
        /// </typeparam>
        /// <param name="obj">
        ///     Object from where the Property Value is returned
        /// </param>
        /// <param name="propName">
        ///     Propertyname as string.
        /// </param>
        /// <returns>
        ///     PropertyValue
        /// </returns>
        public static T GetPrivatePropertyValue<T>(this object obj, string propName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            var pi = obj.GetType()
                .GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (pi == null)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(propName),
                    $"Property {propName} was not found in Type {obj.GetType().FullName}");
            }

            return (T) pi.GetValue(obj, null);
        }

        /// <summary>
        ///     The are all properties default or empty.
        /// </summary>
        /// <param name="input">
        ///     The input.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsObjectSetToDefault(this object input)
        {
            var properties = input.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(input, null);
                if (value != prop.PropertyType.GetDefault())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     Set a private Property Value on a given Object. Uses Reflection.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the Property
        /// </typeparam>
        /// <param name="obj">
        ///     Object from where the Property Value is returned
        /// </param>
        /// <param name="propName">
        ///     Propertyname as string.
        /// </param>
        /// <param name="val">
        ///     the value to set
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     if the Property is not found
        /// </exception>
        public static void SetPrivateFieldValue<T>(this object obj, string propName, T val)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            var t = obj.GetType();
            FieldInfo fi = null;
            while (fi == null && t != null)
            {
                fi = t.GetField(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                t = t.BaseType;
            }

            if (fi == null)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(propName),
                    $"Field {propName} was not found in Type {obj.GetType().FullName}");
            }

            fi.SetValue(obj, val);
        }

        /// <summary>
        ///     Sets a _private_ Property Value from a given Object. Uses Reflection.
        ///     Throws a ArgumentOutOfRangeException if the Property is not found.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the Property
        /// </typeparam>
        /// <param name="obj">
        ///     Object from where the Property Value is set
        /// </param>
        /// <param name="propName">
        ///     Propertyname as string.
        /// </param>
        /// <param name="val">
        ///     Value to set.
        /// </param>
        public static void SetPrivatePropertyValue<T>(this object obj, string propName, T val)
        {
            var t = obj.GetType();
            if (t.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) == null)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(propName),
                    $"Property {propName} was not found in Type {obj.GetType().FullName}");
            }

            t.InvokeMember(
                propName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance,
                null,
                obj,
                new object[] {val});
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Resolves the primitive setters.
        /// </summary>
        /// <param name="type">
        ///     The type.
        /// </param>
        /// <returns>
        ///     The list of properties
        /// </returns>
        internal static IList<PropertyInfo> ResolvePrimitiveSetters(this Type type)
        {
            var attr =
                type.GetProperties(
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty)
                    .ToArray();
            var primitiveSetters =
                attr.Where(x => x.PropertyType.IsValueType || x.PropertyType == typeof(string)).ToArray();

            return primitiveSetters;
        }

        /// <summary>
        ///     Sets the primitive value.
        /// </summary>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        /// <param name="propertyInfo">
        ///     The property info.
        /// </param>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <param name="cultureInfo">
        ///     The culture info.
        /// </param>
        /// <param name="numberStyles">
        ///     The number styles.
        /// </param>
        internal static void SetPrimitiveValue(
            this object entity,
            PropertyInfo propertyInfo,
            string value,
            CultureInfo cultureInfo,
            NumberStyles numberStyles = NumberStyles.Any)
        {
            if (!string.IsNullOrWhiteSpace(value) && entity != null)
            {
                value = value.Trim();
                if (propertyInfo.PropertyType == typeof(string))
                {
                    propertyInfo.SetValue(entity, value, null);
                    return;
                }

                if (propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(bool?))
                {
                    var propertyValue = value.ConvertToBooleanNullable();
                    if (propertyValue.HasValue)
                    {
                        propertyInfo.SetValue(entity, propertyValue, null);
                    }

                    return;
                }

                if (propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType == typeof(int?))
                {
                    int result;
                    if (int.TryParse(value, numberStyles, cultureInfo, out result))
                    {
                        propertyInfo.SetValue(entity, result, null);
                    }

                    return;
                }

                if (propertyInfo.PropertyType == typeof(float) || propertyInfo.PropertyType == typeof(float?))
                {
                    float result;
                    if (float.TryParse(value, numberStyles, cultureInfo, out result))
                    {
                        propertyInfo.SetValue(entity, result, null);
                    }

                    return;
                }

                if (propertyInfo.PropertyType == typeof(double) || propertyInfo.PropertyType == typeof(double?))
                {
                    double result;
                    if (double.TryParse(value, numberStyles, cultureInfo, out result))
                    {
                        propertyInfo.SetValue(entity, result, null);
                    }

                    return;
                }

                if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
                {
                    DateTime result;
                    if (DateTime.TryParse(
                        value,
                        cultureInfo,
                        DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite
                        | DateTimeStyles.AllowWhiteSpaces,
                        out result))
                    {
                        propertyInfo.SetValue(entity, result, null);
                    }

                    return;
                }

                if (propertyInfo.PropertyType == typeof(Guid) || propertyInfo.PropertyType == typeof(Guid?))
                {
                    Guid result;
                    if (Guid.TryParse(value.RemoveNonLetterOrDigitChars(), out result))
                    {
                        propertyInfo.SetValue(entity, result, null);
                    }
                }

                if (propertyInfo.PropertyType == typeof(byte) || propertyInfo.PropertyType == typeof(byte?))
                {
                    byte result;
                    if (byte.TryParse(value, numberStyles, cultureInfo, out result))
                    {
                        propertyInfo.SetValue(entity, result, null);
                    }
                }
            }
        }

        #endregion
    }
}