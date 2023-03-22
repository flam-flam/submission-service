using Moq;

namespace flamflam.SubmissionServiceTests.Integration.Extensions
{
    internal static class ServiceProviderExtensions
    {
        public static T? GetService<T>(this IServiceProvider provider) where T : class
        {
            return provider.GetService(typeof(T)) as T;
        }

        public static Mock<T> GetMock<T>(this IServiceProvider provider) where T : class
        {
            var instance = provider.GetService<T>();
            if (instance == null) throw new ArgumentException($"Can not resolve an instance of '{typeof(T).FullName}'");

            return Mock.Get(instance);
        }
    }
}
