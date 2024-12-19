namespace AutoInterface.Generator.Tests.Methods;

public class DefaultMethodParameterTests
{
    [Fact]
    public void VoidMethodWithSingleParams()
    {
        const string input =
            """
            namespace Tests;

            [AutoInterface]
            public partial class TestClass 
            {
                public void VoidMethodWithParams(int extra = 3)
            }
            """;

        const string expectedOutput =
            """
            public interface ITestClass
            {
                void VoidMethodWithParams(global::System.Int32 extra = 3);
            }
            """;

        TestsFixture.Assert(input, expectedOutput);
    }

    [Fact]
    public void VoidMethodWithMultipleParams()
    {
        const string input =
            """
            namespace Tests;

            [AutoInterface]
            public partial class TestClass 
            {
                public void VoidMethodWithParams(int extra = 3, int extra2 = 3)
            }
            """;

        const string expectedOutput =
            """
            public interface ITestClass
            {
                void VoidMethodWithParams(global::System.Int32 extra = 3, global::System.Int32 extra2 = 3);
            }
            """;

        TestsFixture.Assert(input, expectedOutput);
    }

    [Fact]
    public void VoidMethodWithIntParams()
    {
        const string input =
            """
            namespace Tests;

            [AutoInterface]
            public partial class TestClass 
            {
                public void VoidMethodWithParams(int extra = 3)
            }
            """;

        const string expectedOutput =
            """
            public interface ITestClass
            {
                void VoidMethodWithParams(global::System.Int32 extra = 3);
            }
            """;

        TestsFixture.Assert(input, expectedOutput);
    }

    [Fact]
    public void VoidMethodWithDoubleParams()
    {
        const string input =
            """
            namespace Tests;

            [AutoInterface]
            public partial class TestClass 
            {
                public void VoidMethodWithParams(double extra = 2.2)
            }
            """;

        const string expectedOutput =
            """
            public interface ITestClass
            {
                void VoidMethodWithParams(global::System.Double extra = 2.2);
            }
            """;

        TestsFixture.Assert(input, expectedOutput);
    }

    [Fact]
    public void VoidMethodWithFloatParams()
    {
        const string input =
            """
            namespace Tests;

            [AutoInterface]
            public partial class TestClass 
            {
                public void VoidMethodWithParams(float extra = 2.2f)
            }
            """;

        const string expectedOutput =
            """
            public interface ITestClass
            {
                void VoidMethodWithParams(global::System.Single extra = 2.2f);
            }
            """;

        TestsFixture.Assert(input, expectedOutput);
    }

    [Fact]
    public void VoidMethodWithStringParams()
    {
        const string input =
            """
            namespace Tests;

            [AutoInterface]
            public partial class TestClass 
            {
                public void VoidMethodWithParams(string extra = "abc")
            }
            """;

        const string expectedOutput =
            """
            public interface ITestClass
            {
                void VoidMethodWithParams(global::System.String extra = "abc");
            }
            """;

        TestsFixture.Assert(input, expectedOutput);
    }

    [Fact]
    public void VoidMethodWithUintParams()
    {
        const string input =
            """
            namespace Tests;

            [AutoInterface]
            public partial class TestClass 
            {
                public void VoidMethodWithParams(uint extra = 1)
            }
            """;

        const string expectedOutput =
            """
            public interface ITestClass
            {
                void VoidMethodWithParams(global::System.UInt32 extra = 1);
            }
            """;

        TestsFixture.Assert(input, expectedOutput);
    }
    
    [Fact]
    public void VoidMethodWithByteParams()
    {
        const string input =
            """
            namespace Tests;

            [AutoInterface]
            public partial class TestClass 
            {
                public void VoidMethodWithParams(byte extra = 1)
            }
            """;

        const string expectedOutput =
            """
            public interface ITestClass
            {
                void VoidMethodWithParams(global::System.Byte extra = 1);
            }
            """;

        TestsFixture.Assert(input, expectedOutput);
    }
}