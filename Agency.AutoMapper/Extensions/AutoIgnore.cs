using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Agency.AutoMapper.Extensions
{
    public static class AutoIgnore
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression)
        {
            foreach (var property in expression.TypeMap.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, a => a.Ignore());
            }
            return expression;
        }
    }
}
