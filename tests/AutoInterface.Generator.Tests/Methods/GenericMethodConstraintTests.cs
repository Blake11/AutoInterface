namespace AutoInterface.Generator.Tests.Methods;

public class GenericMethodsConstraintTests
{
    [Fact]
    public void GenericMethodWithManagedConstraintUsed()
    {
        const string input =
            """
            namespace Tests;
            
            [AutoInterface]
            public partial class TestClass
            {
                public void VoidMethodWithoutParameters<T>(T parameter)
                    where T : new()
            };
            """;

        const string expectedOutput =
            """
            public interface ITestClass
            {
                void VoidMethodWithoutParameters<T>(T parameter)
                    where T : new();
            }
            """;
        
        TestsFixture.Assert(input, expectedOutput);
    }
    
    [Fact]
    public void GenericMethodWithClassConstraintUsed()
    {
        const string input =
            """
            namespace Tests;
            
            public class GenericConstraintTest;

            [AutoInterface]
            public partial class TestClass
            {
                public void VoidMethodWithoutParameters<T>(T parameter)
                    where T : GenericConstraintTest
            };
            """;

        const string expectedOutput =
            """
            public interface ITestClass
            {
                void VoidMethodWithoutParameters<T>(T parameter)
                    where T : global::Tests.GenericConstraintTest;
            }
            """;
        
        TestsFixture.Assert(input, expectedOutput);
    }
    
    [Fact]
    public void GenericMethodWithMultipleClassConstraintUsed()
    {
        const string input =
            """
            namespace Tests;

            public class GenericConstraintTest1;
            public class GenericConstraintTest2;

            [AutoInterface]
            public partial class TestClass
            {
                public void VoidMethodWithoutParameters<T, TOther>(T parameter, TOther parameter2) 
                    where T : GenericConstraintTest1
                    where TOther : GenericConstraintTest2
            };
            """;

        const string expectedOutput =
            """
            public interface ITestClass
            {
                void VoidMethodWithoutParameters<T, TOther>(T parameter, TOther parameter2)
                    where T : global::Tests.GenericConstraintTest1 where TOther : global::Tests.GenericConstraintTest2;
            }
            """;
        
        TestsFixture.Assert(input, expectedOutput);
    }
    
    [Fact]
    public void MixedGenericClassWithGenericMethodWithMultipleClassConstraintUsed()
    {
        const string input =
            """
            namespace Tests;

            public class GenericConstraintTest1;
            public class GenericConstraintTest2;
            public class GenericConstraintTest3;
            public class GenericConstraintTest4;

            [AutoInterface]
            public partial class TestClass<T1, T2>
                where T1 : GenericConstraintTest1
                where T2 : GenericConstraintTest2
            {
                public void VoidMethodWithoutParameters<T3, T4>(T1 p1, T2 p2, T3 p3, T4 p4) 
                    where T3 : GenericConstraintTest3
                    where T4 : GenericConstraintTest4
            };
            """;

        const string expectedOutput =
            """
            public interface ITestClass<T1, T2>
                where T1 : global::Tests.GenericConstraintTest1 where T2 : global::Tests.GenericConstraintTest2
            {
                void VoidMethodWithoutParameters<T3, T4>(T1 p1, T2 p2, T3 p3, T4 p4)
                    where T3 : global::Tests.GenericConstraintTest3 where T4 : global::Tests.GenericConstraintTest4;
            }
            """;
        
        TestsFixture.Assert(input, expectedOutput);
    }
}