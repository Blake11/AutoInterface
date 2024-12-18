namespace AutoInterface.Generator.Tests.Methods;

public class GenericClassTests
{
    [Fact]
    public void GenericClassWithNoGenericArgumentUsed()
    {
        const string input =
            """
            namespace Tests;
            
            [AutoInterface]
            public partial class TestClass<T>
            {
                public void VoidMethodWithoutParameters()
            };
            """;

        const string expectedOutput =
            """
            public interface ITestClass<T>
            {
                void VoidMethodWithoutParameters();
            }
            """;


        TestsFixture.Assert(input, expectedOutput);
    }
    
    [Fact]
    public void GenericClassWithGenericArgumentUsed()
    {
        const string input =
            """
            namespace Tests;

            [AutoInterface]
            public partial class TestClass<T>
            {
                public void VoidMethodWithoutParameters(T parameter)
            };
            """;

        const string expectedOutput =
            """
            public interface ITestClass<T>
            {
                void VoidMethodWithoutParameters(T parameter);
            }
            """;


        TestsFixture.Assert(input, expectedOutput);
    }
    
    [Fact]
    public void GenericClassWithMultipleGenericArgumentUsed()
    {
        const string input =
            """
            namespace Tests;

            [AutoInterface]
            public partial class TestClass<T1, T2>
            {
                public void VoidMethodWithoutParameters1(T1 parameter)
                public void VoidMethodWithoutParameters2(T2 parameter)
            };
            """;

        const string expectedOutput =
            """
            public interface ITestClass<T1, T2>
            {
                void VoidMethodWithoutParameters1(T1 parameter);
                void VoidMethodWithoutParameters2(T2 parameter);
            }
            """;


        TestsFixture.Assert(input, expectedOutput);
    }

}