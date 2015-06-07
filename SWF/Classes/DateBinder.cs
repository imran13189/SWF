using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWF.Classes
{
    public class DateBinder : IModelBinder
    {
        string dateFormat;

        public DateBinder(string dateFormat)
        {
            this.dateFormat = dateFormat;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            //return base.BindModel(controllerContext, bindingContext);

            ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            return DateTime.ParseExact(value.AttemptedValue, this.dateFormat, CultureInfo.InvariantCulture);
        }
    }
}