using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLine.Tests.Fakes
{
    [ResourceHelpText(typeof(StringResource), nameof(StringResource.ResourceString))]
    [Verb("verb")]
    public class Options_With_HelpText_From_Resource
    {
        [ResourceHelpText(typeof(StringResource), nameof(StringResource.ResourceString))]
        [Option('o')]
        public bool SomeOption { get; set; }
    }
}
