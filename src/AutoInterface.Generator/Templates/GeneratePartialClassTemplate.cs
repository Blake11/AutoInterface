using System.Text;
using AutoInterface.Generator.Core.Descriptors;

namespace AutoInterface.Generator.Templates;

public static class PartialClassTemplate
{
    public static void Render(in StringBuilder sb, in DetailedTypeDescriptor type)
    {
        sb.AppendLine($"public partial class {type.TypeDescriptor.Name} ");
        GenericArgumentTemplate.Render(sb, type.GenericArguments);
        
        sb.AppendLine($" : I{type.TypeDescriptor.Name} ");
        GenericArgumentTemplate.Render(sb, type.GenericArguments);
        GenericArgumentsConstraintTemplate.Render(sb, type.GenericArguments);
        
        sb.AppendLine("{}");
    }
}