using System.Collections.Generic;
using Xunit;

namespace Antix.Blackhole.Tests
{
    public class BlackholeTemplateEngineTests
    {
        [Fact]
        void when_template_token_has_no_prefix_or_suffix()
        {
            const string template = "{{One}}";
            const string expected = "1";

            var data = new Dictionary<string, string>
                {
                    {"One", "1"}
                };

            var sut = GetSut();
            var actual = sut.Execute(template, data);

            Assert.Equal(expected, actual);
        }

        [Fact]
        void when_template_token_has_a_prefix_and_suffix()
        {
            const string template = "{x{One}y}";
            const string expected = "x1y";

            var data = new Dictionary<string, string>
                {
                    {"One", "1"}
                };

            var sut = GetSut();
            var actual = sut.Execute(template, data);

            Assert.Equal(expected, actual);
        }

        [Fact]
        void when_template_has_leading_and_trailing_text()
        {
            const string template = "x{{One}}y";
            const string expected = "x1y";

            var data = new Dictionary<string, string>
                {
                    {"One", "1"}
                };

            var sut = GetSut();
            var actual = sut.Execute(template, data);

            Assert.Equal(expected, actual);
        }

        [Fact]
        void when_data_value_is_null()
        {
            const string template = "w{x{One}y}z";
            const string expected = "wz";

            var data = new Dictionary<string, string>
                {
                    {"One", null}
                };

            var sut = GetSut();
            var actual = sut.Execute(template, data);

            Assert.Equal(expected, actual);
        }

        [Fact]
        void when_data_value_is_null_and_has_default()
        {
            const string template = "w{x{One|default}y}z";
            const string expected = "wxdefaultyz";

            var data = new Dictionary<string, string>
                {
                    {"One", null}
                };

            var sut = GetSut();
            var actual = sut.Execute(template, data);

            Assert.Equal(expected, actual);
        }

        [Fact]
        void when_data_value_not_found()
        {
            const string template = "w{x{One}y}z";

            var data = new Dictionary<string, string>
                {
                };

            var sut = GetSut();
            Assert.Throws<BlackholePathNotFoundException>(
                () => sut.Execute(template, data));
        }

        static BlackholeTemplateEngine GetSut()
        {
            return new BlackholeTemplateEngine();
        }
    }
}