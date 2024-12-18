namespace AutoInterface.Generator.Tests.Methods;

public class GenericClassConstraintTests
{
    [Fact]
    public void GenericClassWithManagedConstraintUsed()
    {
        const string input =
            """
            namespace Tests;
            
            [AutoInterface]
            public partial class TestClass<T> 
                where T : new()
            {
                public void VoidMethodWithoutParameters(T parameter)
            };
            """;

        const string expectedOutput =
            """
            public interface ITestClass<T>
                where T : new()
            {
                void VoidMethodWithoutParameters(T parameter);
            }
            """;
        
        TestsFixture.Assert(input, expectedOutput);
    }
    
    [Fact]
    public void GenericClassWithClassConstraintUsed()
    {
        const string input =
            """
            namespace Tests;
            
            public class GenericConstraintTest;

            [AutoInterface]
            public partial class TestClass<T> 
                where T : GenericConstraintTest
            {
                public void VoidMethodWithoutParameters(T parameter)
            };
            """;

        const string expectedOutput =
            """
            public interface ITestClass<T>
                where T : global::Tests.GenericConstraintTest
            {
                void VoidMethodWithoutParameters(T parameter);
            }
            """;
        
        TestsFixture.Assert(input, expectedOutput);
    }
    
    [Fact]
    public void GenericClassWithMultipleClassConstraintUsed()
    {
        const string input =
            """
            namespace Tests;

            public class GenericConstraintTest1;
            public class GenericConstraintTest2;

            [AutoInterface]
            public partial class TestClass<T, TOther> 
                where T : GenericConstraintTest1
                where TOther : GenericConstraintTest2
            {
                public void VoidMethodWithoutParameters(T parameter)
            };
            """;

        const string expectedOutput =
            """
            public interface ITestClass<T, TOther>
                where T : global::Tests.GenericConstraintTest1 where TOther : global::Tests.GenericConstraintTest2
            {
                void VoidMethodWithoutParameters(T parameter);
            }
            """;
        
        TestsFixture.Assert(input, expectedOutput);
    }
}