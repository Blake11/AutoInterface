using System.Collections.Generic;
using System.Text;
using AutoInterface.Generator.Core.Descriptors;

namespace AutoInterface.Generator.Templates;

public static class GenericArgumentTemplate
{
    public static void Render(
        in StringBuilder sb,
        in IReadOnlyCollection<GenericArgumentDescriptor> genericArguments)
    {
        if (genericArguments.Count <= 0) return;

        sb.Append("<");

        foreach (var genericArgument in genericArguments)
        {
            sb.Append(genericArgument.Name);
            sb.Append(",");
        }

        //remove last comma
        sb.Remove(sb.Length - 1, 1);

        sb.Append(">");
    }
}