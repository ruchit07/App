using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace App.Service.Extention
{
    public static class BaseExtension
    {
        public static void ToValidate<Tvm>(this Tvm model)
        {
            if (model == null)
            {
                throw new ArgumentException(nameof(Tvm));
            }
        }

        public static T ToEntity<T, Tvm>(this Tvm model)
        {
            if (model == null)
            {
                throw new ArgumentException(nameof(Tvm));
            }

            return (T)Convert.ChangeType(model, typeof(T));
        }

        public static T ToEntity<T, Tvm>(this Tvm model, T entity)
        {
            if (model == null)
            {
                throw new ArgumentException(nameof(Tvm));
            }

            T obj = (T)Convert.ChangeType(model, typeof(T));
            PropertyInfo[] sourceProperties = obj.GetType().GetProperties();
            PropertyInfo[] destinationProperties = entity.GetType().GetProperties();

            foreach (PropertyInfo pi in destinationProperties)
            {
                PropertyInfo sourcePi = sourceProperties.FirstOrDefault(x => x.Name == pi.Name && !pi.Name.Contains("Uid"));
                if (sourcePi != null) pi.SetValue(entity, sourcePi.GetValue(obj, null), null);
            }

            return entity;
        }

        public static TResult ToResult<T, TResult>(this T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException(nameof(T));
            }

            TResult result = (TResult)Activator.CreateInstance(typeof(TResult));
            PropertyInfo[] sourceProperties = entity.GetType().GetProperties();
            PropertyInfo[] destinationProperties = result.GetType().GetProperties();

            foreach (PropertyInfo pi in destinationProperties)
            {
                PropertyInfo sourcePi = sourceProperties.FirstOrDefault(x => x.Name == pi.Name);
                if (sourcePi != null) pi.SetValue(result, sourcePi.GetValue(entity, null), null);
            }

            return result;
        }

        public static List<TResult> ToResult<T, TResult>(this List<T> entities)
        {
            List<TResult> result = (List<TResult>)Activator.CreateInstance(typeof(List<TResult>));
            foreach (var item in entities)
            {
                result.Add(item.ToResult<T, TResult>());
            }

            return result;
        }
    }
}
