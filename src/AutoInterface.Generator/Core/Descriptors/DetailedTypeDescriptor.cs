using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace AutoInterface.Generator.Core.Descriptors;

public readonly struct DetailedTypeDescriptor : IEquatable<DetailedTypeDescriptor>
{
    public readonly SlimTypeDescriptor TypeDescriptor;
    public readonly EquatableArray<MethodDescriptor> Methods;
    public readonly EquatableArray<GenericArgumentDescriptor> GenericArguments;

    public DetailedTypeDescriptor(SlimTypeDescriptor typeDescriptor, EquatableArray<MethodDescriptor> methods,
        EquatableArray<GenericArgumentDescriptor> genericArguments)
    {
        TypeDescriptor = typeDescriptor;
        Methods = methods;
        GenericArguments = genericArguments;
    }

    public static bool TryCreate(GeneratorAttributeSyntaxContext context, out DetailedTypeDescriptor result)
    {
        result = default;

        // nothing to do if this type isn't available
        if (context.TargetSymbol is not INamedTypeSymbol classSymbol ||
            !SlimTypeDescriptor.TryCreate(context, out var slimType))
            return false;

        var methodDescriptors = classSymbol.GetMembers()
            .OfType<IMethodSymbol>()
            .Where(static x => x.DeclaredAccessibility == Accessibility.Public)
            .Where(static x => x.MethodKind == MethodKind.Ordinary)
            .Where(static x => !x.IsStatic)
            .Where(static x => !x.IsImplicitlyDeclared)
            .Select(static m => MethodDescriptor.Create(m))
            .ToEquatableArray();

        var genericArguments = classSymbol.TypeArguments
            .Cast<ITypeParameterSymbol>()
            .Select(x => GenericArgumentDescriptor.Create(x))
            .ToEquatableArray();
        
        result = new DetailedTypeDescriptor(
            slimType,
            methodDescriptors,
            genericArguments.ToEquatableArray()
        );

        return true;
    }
    
    public bool Equals(DetailedTypeDescriptor other) =>
        TypeDescriptor.Equals(other.TypeDescriptor) &&
        Methods.Equals(other.Methods) &&
        GenericArguments.Equals(other.GenericArguments);

    public override bool Equals(object? obj) => obj is DetailedTypeDescriptor other && Equals(other);

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = TypeDescriptor.GetHashCode();
            hashCode = (hashCode * 397) ^ Methods.GetHashCode();
            hashCode = (hashCode * 397) ^ GenericArguments.GetHashCode();
            return hashCode;
        }
    }
}