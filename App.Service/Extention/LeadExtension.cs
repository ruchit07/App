using App.Data.Helpers;
using App.Data.Model;
using App.Data.Models;
using App.Data.Models.Results;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace App.Service.Extention
{
    public static class LeadExtension
    {
        #region 'Validate'
        public static void Validate(this LeadVm model)
        {
            if (string.IsNullOrEmpty(model.FirstName)) BaseExtention.ThowValidationException(Message.FistNameRequired);

            if (string.IsNullOrEmpty(model.Email)) BaseExtention.ThowValidationException(Message.EmailIsRequired);

            if (!Helper.IsValidEmail(model.Email)) BaseExtention.ThowValidationException(Message.InvalidEmail);
        }
        #endregion

        #region 'Entity'
        public static Lead ToEntity(this LeadVm model)
        {
            return model as Lead;
        }

        public static Lead ToEntity(this LeadVm model, Lead entity)
        {
            Lead obj = model as Lead;
            PropertyInfo[] sourceProperties = obj.GetType().GetProperties();
            PropertyInfo[] destinationProperties = entity.GetType().GetProperties();

            foreach (PropertyInfo pi in destinationProperties)
            {
                PropertyInfo sourcePi = sourceProperties.FirstOrDefault(x => x.Name == pi.Name);
                if (sourcePi != null) pi.SetValue(entity, sourcePi.GetValue(obj, null), null);
            }

            return entity;
        }

        #endregion

        #region 'Result'
        public static Result ToResult(this Lead entity)
        {
            Result result = new Result();
            PropertyInfo[] sourceProperties = entity.GetType().GetProperties();
            PropertyInfo[] destinationProperties = result.GetType().GetProperties();

            foreach (PropertyInfo pi in destinationProperties)
            {
                PropertyInfo sourcePi = sourceProperties.FirstOrDefault(x => x.Name == pi.Name);
                if (sourcePi != null) pi.SetValue(result, sourcePi.GetValue(entity, null), null);
            }

            return result;
        }

        public static List<Result> ToResult(this List<Lead> entities, int count)
        {
            List<Result> results = new List<Result>();
            Result result = new Result();
            foreach (var item in entities)
            {
                result = item.ToResult();
                results.Add(result);
            }

            return results;
        }
        #endregion
    }
}
