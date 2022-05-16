using Microsoft.VisualStudio.TestPlatform.Utilities;
using Xunit.Abstractions;
using IOutput = PactNet.Infrastructure.Outputters.IOutput;

namespace DiscountServiceProvider.Test {
    public class XUnitOutput : IOutput {
        private readonly ITestOutputHelper output;

        public XUnitOutput(ITestOutputHelper output) { 
            this.output = output;
        }

        public void Write(string message, OutputLevel level) {
            this.output.WriteLine(message);
        }

        public void WriteLine(string line) {
            this.output.WriteLine(line);
        }

        public void WriteLine(string message, OutputLevel level) {
            this.output.WriteLine(message);
        }
    }
}
