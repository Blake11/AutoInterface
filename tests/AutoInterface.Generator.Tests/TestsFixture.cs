using System;
using System.Linq;
using AutoInterface.Generator.Generators;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace AutoInterface.Generator.Tests;

public static class TestsFixture
{
    public static void Assert(string input, string expectedOutput)
    {
        var generator = new AutoInterfaceMethodsGenerator();
        var driver = CSharpGeneratorDriver.Create(generator);

        var compilation = CSharpCompilation.Create(
            nameof(input),
            [
                CSharpSyntaxTree.ParseText(input)
            ],
            [
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
            ]
        );

        var runResult = driver.RunGenerators(compilation).GetRunResult();

        var generatedFileSyntax = runResult.GeneratedTrees.Single(t =>
            !t.FilePath.Contains("AutoInterfaceAttribute.g.cs")
        );


        var actual = generatedFileSyntax.GetText().ToString().ReplaceLineEndings();

        actual = actual[actual.IndexOf("public interface", StringComparison.Ordinal)..];


        // actual[475..^0].ToList().Zip(expectedOutput.ReplaceLineEndings()).Select(x => (x.First, x.Second, x.First == x.Second))
        actual.Should().Contain(expectedOutput.ReplaceLineEndings());
    }
}