using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace AutoInterface.Generator.Generators.Extensions;

public static class StringExtensions
{
    public static SourceText ToFormattedCode(this string input)
    {
        var sourceCode = SourceText.From(input, Encoding.UTF8);
        //make the code a bit more pretty
        var formattedCode = CSharpSyntaxTree.ParseText(sourceCode)
            .GetRoot()
            .NormalizeWhitespace()
            .SyntaxTree
            .GetText();

        return formattedCode;
    }
}