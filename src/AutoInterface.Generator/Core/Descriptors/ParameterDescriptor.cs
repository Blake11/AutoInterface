using System;
using Microsoft.CodeAnalysis;

namespace AutoInterface.Generator.Core.Descriptors;

public readonly struct ParameterDescriptor : IEquatable<ParameterDescriptor>
{
    public readonly string Name;
    public readonly bool IsParams;
    public readonly RefKind RefKind;
    public readonly SlimTypeDescriptor TypeDescriptor;

    public ParameterDescriptor(string name, bool isParams, RefKind refKind, SlimTypeDescriptor typeDescriptor)
    {
        TypeDescriptor = typeDescriptor;
        RefKind = refKind;
        Name = name;
        IsParams = isParams;
    }

    public static ParameterDescriptor Create(IParameterSymbol symbol)
    {
        var name = symbol.Name;
        var refKind = symbol.RefKind;
        var isParams = symbol.IsParams;
        

        return SlimTypeDescriptor.TryCreate(symbol.Type, out var typeDescriptor)
            ? new ParameterDescriptor(name,isParams, refKind, typeDescriptor)
            : default;
    }

    public bool Equals(ParameterDescriptor other) 
        => Name == other.Name &&
           IsParams == other.IsParams &&
           RefKind == other.RefKind && 
           TypeDescriptor.Equals(other.TypeDescriptor);

    public override bool Equals(object? obj) 
        => obj is ParameterDescriptor other && Equals(other);

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Name.GetHashCode();
            hashCode = (hashCode * 397) ^ IsParams.GetHashCode();
            hashCode = (hashCode * 397) ^ (int)RefKind;
            hashCode = (hashCode * 397) ^ TypeDescriptor.GetHashCode();
            return hashCode;
        }
    }
}