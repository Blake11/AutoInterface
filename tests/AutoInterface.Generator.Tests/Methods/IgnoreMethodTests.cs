namespace AutoInterface.Generator.Tests.Methods;

public class IgnoreMethodTests
{
    [Fact]
    public void IgnoreMethod()
    {
        const string input =
            """
            namespace Tests;

            [AutoInterface]
            public partial class TestClass 
            {
                [AutoInterfaceIgnore]
                public void VoidMethod()
            }
            """;

        const string expectedOutput =
            """
            public interface ITestClass
            {
            }
            """;

        TestsFixture.Assert(input, expectedOutput);
    }
    
    [Fact]
    public void IgnoreSomeMethod()
    {
        const string input =
            """
            namespace Tests;

            [AutoInterface]
            public partial class TestClass 
            {
                [AutoInterfaceIgnore]
                public void VoidMethod()
                
                public void VoidMethod2()
            }
            """;

        const string expectedOutput =
            """
            public interface ITestClass
            {
                void VoidMethod2();
            }
            """;

        TestsFixture.Assert(input, expectedOutput);
    }
}