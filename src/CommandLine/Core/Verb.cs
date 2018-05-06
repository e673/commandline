// Copyright 2005-2015 Giacomo Stelluti Scala & Contributors. All rights reserved. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
#if !NET40
using System.Reflection;
#endif

namespace CommandLine.Core
{
    sealed class Verb
    {
        private readonly string name;
        private readonly string helpText;
        private readonly bool hidden;

        public Verb(string name, string helpText, bool hidden = false)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (helpText == null) throw new ArgumentNullException("helpText");

            this.name = name;
            this.helpText = helpText;
            this.hidden = hidden;
        }

        public string Name
        {
            get { return name; }
        }

        public string HelpText
        {
            get { return helpText; }
        }

        public bool Hidden
        {
            get { return hidden; }
        }

        private static Verb FromAttribute(VerbAttribute attribute)
        {
            return new Verb(
                attribute.Name,
                attribute.HelpText,
                attribute.Hidden
                );
        }

        public Verb WithHelpText(string newHelpText)
        {
            return new Verb(
                Name,
                newHelpText,
                Hidden
                );
        }

        public static IEnumerable<Tuple<Verb, Type>> SelectFromTypes(IEnumerable<Type> types)
        {
            foreach (var type in types.Select(x => x.GetTypeInfo()))
            {
                var typeInfo = type.GetTypeInfo();

                var attrs = typeInfo.GetCustomAttributes(typeof(VerbAttribute), true).ToArray();
                var helpAttr = typeInfo.GetCustomAttributes(typeof(ResourceHelpTextAttribute), true).Cast<ResourceHelpTextAttribute>().FirstOrDefault();

                if (attrs.Length != 1)
                    continue;

                var verb = FromAttribute((VerbAttribute)attrs.Single());

                if (helpAttr != null)
                {
                    verb = verb.WithHelpText(helpAttr.Text);
                }

                yield return Tuple.Create(verb, type);
            }
        }
    }
}