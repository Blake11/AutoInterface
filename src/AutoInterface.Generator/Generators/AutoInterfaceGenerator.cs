using System.Threading;
using AutoInterface.Generator.Core.Descriptors;
using AutoInterface.Generator.Generators.Extensions;
using AutoInterface.Generator.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoInterface.Generator.Generators;

[Generator(LanguageNames.CSharp)]
public class AutoInterfaceMethodsGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(x =>
        {
            x.AddSource(
                GenerateAutoInterfaceAttributeTemplate.FileName,
                GenerateAutoInterfaceAttributeTemplate.Code.ToFormattedCode()
            );
            x.AddSource(
                GenerateAutoInterfaceIgnoreAttributeTemplate.FileName,
                GenerateAutoInterfaceIgnoreAttributeTemplate.Code.ToFormattedCode()
            );
        });

        var classesToParse = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                $"{GenerateAutoInterfaceAttributeTemplate.ClassName}",
                predicate: Predicate,
                transform: Transform)
            .Where(IsMatch);

        context.RegisterSourceOutput(classesToParse,
            static (spc, typeDescriptor) => OutputMutationClasses(spc, in typeDescriptor));
    }


    private static bool Predicate(SyntaxNode node, CancellationToken cancellationToken)
        => !cancellationToken.IsCancellationRequested &&
           node is TypeDeclarationSyntax cds &&
           !cds.Modifiers.Any(SyntaxKind.StaticKeyword);

    private static DetailedTypeDescriptor? Transform(GeneratorAttributeSyntaxContext context, CancellationToken ct)
        => !ct.IsCancellationRequested &&
           DetailedTypeDescriptor.TryCreate(context, out var typeDescriptor)
            ? typeDescriptor
            : null;

    private static bool IsMatch(DetailedTypeDescriptor? m)
        => m is not null;

    private static void OutputMutationClasses(in SourceProductionContext context,
        in DetailedTypeDescriptor? typeDescriptor)
    {
        if (!typeDescriptor.HasValue)
            return;

        var result = GenerateInterfaceTemplate.Render(typeDescriptor.Value);
        var formattedCode = result.ToFormattedCode();

        context.AddSource(GenerateInterfaceTemplate.FileName(typeDescriptor.Value), formattedCode);
    }
}