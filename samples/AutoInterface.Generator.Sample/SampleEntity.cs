using System;

namespace AutoInterface.Generator.Sample;


[AutoInterface]
public partial class HelloWorldExample
{
    public void HelloWorld() 
        => Console.WriteLine("Hello World");
}

public static class Test
{
    public static void Run()
    {
        IHelloWorldExample sample = new HelloWorldExample();

        sample.HelloWorld();
    }
}