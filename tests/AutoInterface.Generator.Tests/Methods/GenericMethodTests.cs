namespace AutoInterface.Generator.Tests.Methods;

public class GenericMethodTests
{
    [Fact]
    public void NonGenericClassWithGenericMethod()
    {
        const string input =
            """
            namespace Tests;

            [AutoInterface]
            public partial class TestClass
            {
                public void VoidMethodWithoutParameters<T>(T parameter)
            };
            """;

        const string expectedOutput =
            """
            public interface ITestClass
            {
                void VoidMethodWithoutParameters<T>(T parameter);
            }
            """;


        TestsFixture.Assert(input, expectedOutput);
    }
    
    [Fact]
    public void GenericClassWithGenericMethod()
    {
        const string input =
            """
            namespace Tests;

            [AutoInterface]
            public partial class TestClass<T1>
            {
                public void VoidMethodWithoutParameters1(T1 parameter)
                public void VoidMethodWithoutParameters2<T2>(T2 parameter)
                public void VoidMethodWithoutParameters3<T2>(T1 parameter1, T2 parameter2)
            };
            """;

        const string expectedOutput =
            """
            public interface ITestClass<T1>
            {
                void VoidMethodWithoutParameters1(T1 parameter);
                void VoidMethodWithoutParameters2<T2>(T2 parameter);
                void VoidMethodWithoutParameters3<T2>(T1 parameter1, T2 parameter2);
            }
            """;


        TestsFixture.Assert(input, expectedOutput);
    }

}