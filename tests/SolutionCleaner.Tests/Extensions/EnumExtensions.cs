using System;

namespace SolutionCleaner.Tests.Extensions
{
    public static class EnumExtensions
    {
        public static T RandomEnumValue<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum) { throw new Exception("Random enum variable is not an enum!"); }

            var random = new Random();
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(random.Next(values.Length));
        }
    }
}