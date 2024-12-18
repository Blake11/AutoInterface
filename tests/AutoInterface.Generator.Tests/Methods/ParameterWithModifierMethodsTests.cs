namespace AutoInterface.Generator.Tests.Methods;

public class ParameterWithModifierMethodsTests
{
    [Fact]
    public void VoidMethodWithoutParameters()
    {
        const string input =
            """
            namespace Tests;
            
            [AutoInterface]
            public partial class TestClass 
            {
                public void VoidMethodWithoutParameters()
            };
            """;

        const string expectedOutput =
            """
            public interface ITestClass
            {
                void VoidMethodWithoutParameters();
            }
            """;


        TestsFixture.Assert(input, expectedOutput);
    }
    
    [Fact]
    public void VoidMethodWithParams()
    {
        const string input =
            """
            namespace Tests;

            [AutoInterface]
            public partial class TestClass 
            {
                public void VoidMethodWithParams(int extra)
            }
            """;

        const string expectedOutput =
            """
            public interface ITestClass
            {
                void VoidMethodWithParams(global::System.Int32 extra);
            }
            """;


        TestsFixture.Assert(input, expectedOutput);
    }
}