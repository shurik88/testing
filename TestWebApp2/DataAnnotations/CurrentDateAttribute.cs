using System;
using System.ComponentModel.DataAnnotations;

namespace TestWebApp2.DataAnnotations
{
    public class CurrentDateAttribute : ValidationAttribute
    {
        public CurrentDateAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            var dt = (DateTime)value;
            return dt >= DateTime.Now;
        }
    }
}
