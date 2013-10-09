using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;

namespace VinaSale.Infrastructure
{
    public static class ProvidersHelper
    {
        /// <summary>
        ///     Instantiates the provider.
        /// </summary>
        /// <param name="providerSettings"> The settings. </param>
        /// <param name="providerType"> Type of the provider to be instantiated. </param>
        /// <returns> </returns>
        public static ProviderBase InstantiateProvider(ProviderSettings providerSettings, Type providerType)
        {
            var str = (providerSettings.Type == null) ? null : providerSettings.Type.Trim();
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("Provider type name is invalid");
            }

            var type = Type.GetType(str, true, true);
            if (!providerType.IsAssignableFrom(type))
            {
                throw new ArgumentException(String.Format("Provider must implement type {0}.", providerType));
            }

            var providerBase = (ProviderBase)Activator.CreateInstance(type);
            var parameters = providerSettings.Parameters;
            var config = new NameValueCollection(parameters.Count, StringComparer.Ordinal);
            foreach (string param in parameters)
            {
                config[param] = parameters[param];
            }

            providerBase.Initialize(providerSettings.Name, config);
            return providerBase;
        }

        public static void InstantiateProviders(ProviderSettingsCollection providerSettings, ProviderCollection providers, Type providerType)
        {
            foreach (ProviderSettings settings in providerSettings)
            {
                providers.Add(InstantiateProvider(settings, providerType));
            }
        }
    }
}