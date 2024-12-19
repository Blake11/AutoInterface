using System;
using Microsoft.CodeAnalysis;

namespace AutoInterface.Generator.Core.Descriptors;

public readonly struct ParameterDescriptor : IEquatable<ParameterDescriptor>
{
    public readonly string Name;
    public readonly bool IsParams;
    public readonly RefKind RefKind;
    public readonly SlimTypeDescriptor TypeDescriptor;
    public readonly ParameterDefaultValue Default;

    public ParameterDescriptor(string name, bool isParams, RefKind refKind, SlimTypeDescriptor typeDescriptor,
        ParameterDefaultValue @default)
    {
        TypeDescriptor = typeDescriptor;
        Default = @default;
        RefKind = refKind;
        Name = name;
        IsParams = isParams;
    }

    public static ParameterDescriptor Create(IParameterSymbol symbol)
    {
        var name = symbol.Name;
        var refKind = symbol.RefKind;
        var isParams = symbol.IsParams;
        var defaultValue = ParameterDefaultValue.Create(symbol);

        return SlimTypeDescriptor.TryCreate(symbol.Type, out var typeDescriptor)
            ? new ParameterDescriptor(name, isParams, refKind, typeDescriptor, defaultValue)
            : default;
    }

    public bool Equals(ParameterDescriptor other)
        => Name == other.Name &&
           IsParams == other.IsParams &&
           RefKind == other.RefKind &&
           TypeDescriptor.Equals(other.TypeDescriptor) &&
           Default.Equals(other.Default);

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
            hashCode = (hashCode * 397) ^ Default.GetHashCode();
            return hashCode;
        }
    }
}

public readonly struct ParameterDefaultValue : IEquatable<ParameterDefaultValue>
{
    public readonly bool IsSet;
    public readonly object? Value;

    public ParameterDefaultValue(bool isSet, object? value)
    {
        IsSet = isSet;
        Value = value;
    }

    public static ParameterDefaultValue Create(IParameterSymbol type) => new(
        type.HasExplicitDefaultValue,
        type.HasExplicitDefaultValue ? type.ExplicitDefaultValue : null
    );

    public bool Equals(ParameterDefaultValue other)
        => IsSet && other.IsSet && Equals(Value, other.Value);

    public override bool Equals(object? obj)
    {
        return obj is ParameterDefaultValue other && Equals(other);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}