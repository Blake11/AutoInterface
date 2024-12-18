using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace AutoInterface.Generator.Core.Descriptors;

public readonly struct MethodDescriptor : IEquatable<MethodDescriptor>
{
    public string Name { get; }
    public SlimTypeDescriptor ReturnType { get; }
    public EquatableArray<ParameterDescriptor> Arguments { get; }
    public EquatableArray<GenericArgumentDescriptor> GenericArguments { get; }

    public MethodDescriptor(string name, SlimTypeDescriptor returnType,
        EquatableArray<ParameterDescriptor> arguments,
        EquatableArray<GenericArgumentDescriptor> genericArguments)
    {
        ReturnType = returnType;
        Name = name;
        Arguments = arguments;
        GenericArguments = genericArguments;
    }

    public static MethodDescriptor Create(IMethodSymbol symbol)
    {
        var name = symbol.Name;

        if (!SlimTypeDescriptor.TryCreate(symbol.ReturnType, out var returnType))
            return default;

        var parameters = symbol.Parameters
            .Select(ParameterDescriptor.Create)
            .ToEquatableArray();

        var genericArguments = symbol.TypeArguments
            .Cast<ITypeParameterSymbol>()
            .Select(GenericArgumentDescriptor.Create)
            .ToEquatableArray();

        return new MethodDescriptor(
            name,
            returnType,
            parameters,
            genericArguments
        );
    }

    public bool Equals(MethodDescriptor other)
        => Name == other.Name &&
           ReturnType.Equals(other.ReturnType) &&
           Arguments.Equals(other.Arguments) &&
           GenericArguments.Equals(other.GenericArguments);

    public override bool Equals(object? obj)
        => obj is MethodDescriptor other && Equals(other);

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Name.GetHashCode();
            hashCode = (hashCode * 397) ^ ReturnType.GetHashCode();
            hashCode = (hashCode * 397) ^ Arguments.GetHashCode();
            hashCode = (hashCode * 397) ^ GenericArguments.GetHashCode();
            return hashCode;
        }
    }
}