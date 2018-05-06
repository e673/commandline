using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CommandLine
{
    /// <summary>
    /// Configure HelpText to be taken from resource
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public sealed class ResourceHelpTextAttribute
        : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLine.ResourceHelpTextAttribute"/> class.
        /// </summary>
        /// <param name="provider">Resource provider</param>
        /// <param name="key">Resource key</param>
        public ResourceHelpTextAttribute(Type provider, string key)
        {
            var rmProperty = provider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic)
                .FirstOrDefault(property => property.PropertyType == typeof(System.Resources.ResourceManager));

            if (rmProperty != null)
            {
                var resourceManager = (System.Resources.ResourceManager)rmProperty.GetValue(null, null);
                Text = resourceManager.GetString(key);
            }
            else
            {
                Text = key;
            }
        }

        /// <summary>
        /// A short description of a command line option. Usually a sentence summary.
        /// </summary>
        public string Text { get; private set; }

    }
}
