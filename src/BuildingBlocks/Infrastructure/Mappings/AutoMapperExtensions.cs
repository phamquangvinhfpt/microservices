using System.Reflection;
using AutoMapper;

namespace Infrastructure.Mappings;

public static class AutoMapperExtensions
{
    public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>
        (this IMappingExpression<TSource, TDestination> expression)
    {
        var flags = BindingFlags.Public | BindingFlags.Instance;
        var sourceType = typeof(TSource);
        var desinationProperties = typeof(TDestination).GetProperties(flags);

        foreach (var property in desinationProperties)
            if (sourceType.GetProperty(property.Name, flags) == null)
                expression.ForMember(property.Name, opt => opt.Ignore());
        
        return expression;
    }
}