namespace AutoInterface.Generator.Tests.Methods;

public class ReturnTypeMethodsTests
{
    [Fact]
    public void InParameter()
    {
        const string input =
            """
            namespace Tests;

            [AutoInterface]
            public partial class TestClass 
            {
                public void VoidMethodWithInParameter(in int extra)
            }
            """;

        const string expectedOutput =
            """
            public interface ITestClass
            {
                void VoidMethodWithInParameter(in global::System.Int32 extra);
            }
            """;


        TestsFixture.Assert(input, expectedOutput);
    }

    [Fact]
    public void OutParameter()
    {
        const string input =
            """
            namespace Tests;

            [AutoInterface]
            public partial class TestClass 
            {
                public int ReturnTypeWithParams(out int extra)
            }
            """;

        const string expectedOutput =
            """
            public interface ITestClass
            {
                global::System.Int32 ReturnTypeWithParams(out global::System.Int32 extra);
            }
            """;


        TestsFixture.Assert(input, expectedOutput);
    }

    [Fact]
    public void RefParameter()
    {
        const string input =
            """
            namespace Tests;

            [AutoInterface]
            public partial class TestClass 
            {
                public int ReturnTypeWithParams(ref int extra)
            }
            """;

        const string expectedOutput =
            """
            public interface ITestClass
            {
                global::System.Int32 ReturnTypeWithParams(ref global::System.Int32 extra);
            }
            """;


        TestsFixture.Assert(input, expectedOutput);
    }

    [Fact]
    public void ParamsParameter()
    {
        const string input =
            """
            namespace Tests;

            [AutoInterface]
            public partial class TestClass 
            {
                public int ReturnTypeWithParams(params int[] extra)
            }
            """;

        const string expectedOutput =
            """
            public interface ITestClass
            {
                global::System.Int32 ReturnTypeWithParams(params global::System.Int32[] extra);
            }
            """;


        TestsFixture.Assert(input, expectedOutput);
    }
}