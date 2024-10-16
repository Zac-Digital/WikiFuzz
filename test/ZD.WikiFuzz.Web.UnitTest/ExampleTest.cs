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
        const int x = 4;
        const int y = 8;

        const int z = x * y;

        z.Should().Be(32);
    }
}