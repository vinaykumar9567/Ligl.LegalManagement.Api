using System.Reflection;
namespace Ligl.LegalManagement.Business.Command
{
    public static class GenericMapper
    {
        public static TDestination Map<TSource, TDestination>(TSource source)
            where TSource : class
            where TDestination : class, new()
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Source object cannot be null.");
            }

            // Create an instance of the destination type
            TDestination destination = new TDestination();

            // Get the properties of the source and destination types
            PropertyInfo[] sourceProperties = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] destinationProperties = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Create a dictionary for faster lookup of destination properties
            var destinationPropertyDict = destinationProperties.ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

            // Map each property from the source to the destination
            foreach (var sourceProperty in sourceProperties)
            {
                if (destinationPropertyDict.TryGetValue(sourceProperty.Name, out var destinationProperty))
                {
                    if (destinationProperty.CanWrite)
                    {
                        var value = sourceProperty.GetValue(source);
                        destinationProperty.SetValue(destination, value);
                    }
                }
            }

            return destination;
        }
    }
}
