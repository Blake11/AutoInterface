using System.Linq;
using System.Text;
using AutoInterface.Generator.Core;
using AutoInterface.Generator.Core.Descriptors;

namespace AutoInterface.Generator.Templates;

public static class InterfaceTemplate
{
    public static void Render(in StringBuilder sb, in DetailedTypeDescriptor type)
    {
        sb.AppendLine($"public interface I{type.TypeDescriptor.Name}");
        GenericArgumentTemplate.Render(sb, type.GenericArguments);
        GenericArgumentsConstraintTemplate.Render(sb, type.GenericArguments);
        sb.AppendLine("{");
        RenderMethods(sb, type);
        sb.AppendLine("}");
    }

    private static void RenderMethods(in StringBuilder sb, in DetailedTypeDescriptor detailedTypeDescriptor)
    {
        foreach (var method in detailedTypeDescriptor.Methods)
        {
            sb.AppendLine();
            sb.Append($"{SlimTypeDescriptorTemplates.Render(method.ReturnType)} {method.Name}");
            GenericArgumentTemplate.Render(sb, method.GenericArguments);
            sb.Append("(");

            RenderParameters(sb, method.Arguments, method.GenericArguments, detailedTypeDescriptor.GenericArguments);

            sb.Append(")");
            GenericArgumentsConstraintTemplate.Render(sb, method.GenericArguments);
            sb.Append(";");
        }
    }

    private static void RenderParameters(
        in StringBuilder sb,
        in EquatableArray<ParameterDescriptor> parameters,
        in EquatableArray<GenericArgumentDescriptor> genericArguments,
        in EquatableArray<GenericArgumentDescriptor> typeGenericArguments)
    {
        if (parameters is not { Count: > 0 })
            return;

        foreach (var parameter in parameters)
        {
            var isGenericParameter = genericArguments.Any(x => x.Name == parameter.TypeDescriptor.Name) ||
                                     typeGenericArguments.Any(x => x.Name == parameter.TypeDescriptor.Name);

            ParameterDescriptorTemplate.Render(sb, parameter, isGenericParameter);
            sb.Append(",");
        }

        //remove last comma
        sb.Remove(sb.Length - 1, 1);
    }
}