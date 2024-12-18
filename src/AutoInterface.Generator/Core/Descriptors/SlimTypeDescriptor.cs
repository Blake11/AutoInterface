using System;
using Microsoft.CodeAnalysis;

namespace AutoInterface.Generator.Core.Descriptors;

public readonly struct SlimTypeDescriptor : IEquatable<SlimTypeDescriptor>
{
    public readonly string Name;
    public readonly string Namespace;

    public SlimTypeDescriptor(string name, string @namespace)
    {
        Name = name;
        Namespace = @namespace;
    }

    public static bool TryCreate(GeneratorAttributeSyntaxContext context, out SlimTypeDescriptor result)
        => TryCreate(context.TargetSymbol as ITypeSymbol, out result);

    public static bool TryCreate(ITypeSymbol? typeSymbol, out SlimTypeDescriptor result)
    {
        result = default;

        if (typeSymbol is null)
            return false;

        var name = typeSymbol.Name;
        var nameSpace = typeSymbol.ContainingNamespace?.IsGlobalNamespace ?? true
            ? string.Empty
            : typeSymbol.ContainingNamespace.ToString();

        if (typeSymbol is IArrayTypeSymbol arrayTypeSymbol)
        {
            name = $"{arrayTypeSymbol.ElementType.Name}[]";
            nameSpace = arrayTypeSymbol.ElementType.ContainingNamespace?.IsGlobalNamespace ?? true
                ? string.Empty
                : arrayTypeSymbol.ElementType.ContainingNamespace.ToString();
        }

        result = new SlimTypeDescriptor(
            name,
            nameSpace
        );

        return true;
    }

    public bool Equals(SlimTypeDescriptor other) =>
        Name == other.Name &&
        Namespace == other.Namespace;

    public override bool Equals(object? obj) => obj is SlimTypeDescriptor other && Equals(other);

    public override int GetHashCode()
    {
        unchecked
        {
            return (Name.GetHashCode() * 397) ^ Namespace.GetHashCode();
        }
    }
}