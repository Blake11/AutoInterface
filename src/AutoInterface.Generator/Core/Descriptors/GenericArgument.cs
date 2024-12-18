using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace AutoInterface.Generator.Core.Descriptors;

public readonly struct GenericArgumentDescriptor : IEquatable<GenericArgumentDescriptor>
{
    public readonly string Name;
    public readonly ManagedConstraintType ManagedConstraintType;
    public readonly EquatableArray<SlimTypeDescriptor> TypedDescriptors;

    private GenericArgumentDescriptor(string name,
        ManagedConstraintType managedConstraintType,
        EquatableArray<SlimTypeDescriptor> typedDescriptors)
    {
        ManagedConstraintType = managedConstraintType;
        TypedDescriptors = typedDescriptors;
        Name = name;
    }

    public static GenericArgumentDescriptor Create(ITypeParameterSymbol type)
    {
        var managedConstraintType = ManagedConstraintType.None;

        managedConstraintType |= type.HasConstructorConstraint
            ? ManagedConstraintType.Constructor
            : ManagedConstraintType.None;
        managedConstraintType |= type.HasNotNullConstraint
            ? ManagedConstraintType.NotNull
            : ManagedConstraintType.None;
        managedConstraintType |= type.HasValueTypeConstraint
            ? ManagedConstraintType.ValueType
            : ManagedConstraintType.None;
        managedConstraintType |= type.HasReferenceTypeConstraint
            ? ManagedConstraintType.Reference
            : ManagedConstraintType.None;
        managedConstraintType |= type.HasUnmanagedTypeConstraint
            ? ManagedConstraintType.Unmanaged
            : ManagedConstraintType.None;

        var typedDescriptor = type.ConstraintTypes
            .Select(x => SlimTypeDescriptor.TryCreate(x, out var result) ? result : null as SlimTypeDescriptor?)
            .Where(x => x != null)
            .Select(x => x!.Value)
            .ToEquatableArray();


        return new GenericArgumentDescriptor(
            type.Name,
            managedConstraintType,
            typedDescriptor
        );
    }

    public bool Equals(GenericArgumentDescriptor other) 
        => Name == other.Name &&
           ManagedConstraintType == other.ManagedConstraintType &&
           TypedDescriptors.Equals(other.TypedDescriptors);

    public override bool Equals(object? obj) 
        => obj is GenericArgumentDescriptor other && Equals(other);

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Name.GetHashCode();
            hashCode = (hashCode * 397) ^ (int)ManagedConstraintType;
            hashCode = (hashCode * 397) ^ TypedDescriptors.GetHashCode();
            return hashCode;
        }
    }
}

[Flags]
public enum ManagedConstraintType
{
    None,
    Constructor,
    NotNull,
    ValueType,
    Reference,
    Unmanaged
}