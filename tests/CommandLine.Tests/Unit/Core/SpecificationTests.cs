using CommandLine.Core;
using CommandLine.Tests.Fakes;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CommandLine.Tests.Unit.Core
{
    public class SpecificationTests
    {
        [Fact]
        public void Specification_when_ResourceHelpTextAttribute_is_defined()
        {
            var type = typeof(Options_With_HelpText_From_Resource);
            var property = type.GetProperty(nameof(Options_With_HelpText_From_Resource.SomeOption));
            var specification = Specification.FromProperty(property);
            specification.HelpText.ShouldBeEquivalentTo("ResourceText");
        }
    }
}
