using FluentAssertions;

namespace ZD.WikiFuzz.Web.UnitTest;

public class ExampleTest
{
    /*
     * Random example test, it has two purposes:
     *
     * 1. Templating new project
     * 2. Testing build-and-test.yml workflow :)
     */
    [Fact]
    public void Example()
    {
        const int a = 4;
        const int b = 8;

        const int c = a * b;

        c.Should().Be(32);
    }
}