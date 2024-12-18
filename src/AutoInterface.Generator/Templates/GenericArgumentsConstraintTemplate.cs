using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoInterface.Generator.Core;
using AutoInterface.Generator.Core.Descriptors;

namespace AutoInterface.Generator.Templates;

public static class GenericArgumentsConstraintTemplate
{
    public static void Render(
        in StringBuilder sb,
        in IReadOnlyCollection<GenericArgumentDescriptor> genericArguments)
    {
        foreach (var genericArgument in genericArguments
                     .Where(x => HasManagedGenericConstraint(x) || x.TypedDescriptors.Count > 0))
        {
            sb.AppendLine(HasManagedGenericConstraint(genericArgument)
                ? $"where {genericArgument.Name} : {Render(genericArgument.ManagedConstraintType)}"
                : $"where {genericArgument.Name} : {Render(genericArgument.TypedDescriptors)}");
        }
    }

    private static string Render(EquatableArray<SlimTypeDescriptor> managedConstraintType)
        => string.Join(", ", managedConstraintType.Select(SlimTypeDescriptorTemplates.Render));

    private static string Render(ManagedConstraintType managedConstraintType)
        => managedConstraintType switch
        {
            ManagedConstraintType.None => string.Empty,
            ManagedConstraintType.Constructor => "new ()",
            ManagedConstraintType.Reference => "class",
            ManagedConstraintType.NotNull => "notnull",
            ManagedConstraintType.ValueType => "struct",
            ManagedConstraintType.Unmanaged => "unmanaged",
            _ => string.Empty
        };

    private static bool HasManagedGenericConstraint(in GenericArgumentDescriptor x)
        => x.ManagedConstraintType != ManagedConstraintType.None;
}