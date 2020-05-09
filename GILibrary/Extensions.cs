using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GILibrary
{
    public static class Extensions
    {
        public static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
        public static bool IsNullOrEmpty(this object data)
        {
            return string.IsNullOrEmpty(Convert.ToString(data));
        }
        public static int AsInt(this object data, int? defaultValue = null)
        {
            if (IsNullOrEmpty(data))
                return defaultValue != null ? Convert.ToInt32(defaultValue) : 0;

            return Convert.ToInt32(data);
        }
        public static TSource MergeObject<TSource, TTarget>(this TSource obj, TTarget obj2)
            where TSource : class
            where TTarget : class
        {
            foreach (var prop in obj.GetType().GetProperties())
            {
                try
                {
                    var propertyInfo = obj2.GetType().GetProperty(prop.Name);
                    if (propertyInfo != null)
                    {
                        var newValue = propertyInfo.GetValue(obj2, null);
                        prop.SetValue(obj, Extensions.IsNullOrEmpty(newValue) ? null : Convert.ChangeType(newValue, Extensions.IsNullableType(propertyInfo.PropertyType) ? Nullable.GetUnderlyingType(propertyInfo.PropertyType) : propertyInfo.PropertyType), null);
                    }
                }
                catch (Exception ex)
                {
                    //ex.Log();
                    //throw;
                }
            }
            return obj;
        }
        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }
        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }
        public static TTarget ToNewObject<TSource, TTarget>(this TSource obj, TTarget newObj)
            where TSource : class
            where TTarget : class, new()
        {
            foreach (var prop in obj.GetType().GetProperties())
            {
                try
                {
                    var propertyInfo = newObj.GetType().GetProperty(prop.Name);
                    if (propertyInfo != null)
                    {
                        var value = prop.GetValue(obj, null);
                        propertyInfo.SetValue(newObj, Extensions.IsNullOrEmpty(value) ? null : Convert.ChangeType(value, Extensions.IsNullableType(propertyInfo.PropertyType) ? Nullable.GetUnderlyingType(propertyInfo.PropertyType) : propertyInfo.PropertyType), null);
                    }
                }
                catch (Exception ex)
                {
                    //ex.Log();
                    //throw;
                }
            }
            return newObj;
        }
        public static T ToObject<T>(this DataTable table) where T : class, new()
        {
            try
            {
                var obj = new T();
                foreach (var row in table.AsEnumerable())
                {
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            var propertyInfo = obj.GetType().GetProperty(prop.Name);
                            if (table.Columns.Contains(prop.Name))
                                propertyInfo.SetValue(obj, row[prop.Name] == DBNull.Value || Extensions.IsNullOrEmpty(row[prop.Name]) ? null : Convert.ChangeType(row[prop.Name], Extensions.IsNullableType(propertyInfo.PropertyType) ? Nullable.GetUnderlyingType(propertyInfo.PropertyType) : propertyInfo.PropertyType), null);
                        }
                        catch (Exception ex)
                        {
                            //ex.Log();
                            //throw;
                        }
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                //ex.Log();
                return null;
            }
        }
        public static DateTime AsDateTime(this object data, DateTime? defaultValue = null)
        {
            if (IsNullOrEmpty(data))
                return defaultValue != null ? Convert.ToDateTime(defaultValue) : DateTime.MinValue;

            var dutchCultureInfo = CultureInfo.CreateSpecificCulture("en-US");
            dutchCultureInfo.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            dutchCultureInfo.DateTimeFormat.DateSeparator = "/";

            var culture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = dutchCultureInfo;
            DateTime dtTime = Convert.ToDateTime(data);

            var date = string.Format("{0:dd/MM/yyyy}", dtTime);

            DateTime newDate = DateTime.ParseExact(date, "dd/MM/yyyy", dutchCultureInfo, DateTimeStyles.None);

            Thread.CurrentThread.CurrentCulture = culture;
            return newDate;

        }
        public static DateTime? AsDateTimeNull(this object data)
        {
            //return IsNullOrEmpty(data) ? (DateTime?)null : Convert.ToDateTime(data);

            if (IsNullOrEmpty(data))
                return null;

            var dutchCultureInfo = CultureInfo.CreateSpecificCulture("en-US");
            dutchCultureInfo.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            dutchCultureInfo.DateTimeFormat.DateSeparator = "/";

            var culture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = dutchCultureInfo;
            DateTime dtTime = Convert.ToDateTime(data);

            var date = string.Format("{0:dd/MM/yyyy}", dtTime);

            DateTime newDate = DateTime.ParseExact(date, "dd/MM/yyyy", dutchCultureInfo, DateTimeStyles.None);

            Thread.CurrentThread.CurrentCulture = culture;
            return newDate;
        }
    }
}
