using System.Globalization;
using System.Text;
using AutoInterface.Generator.Core.Descriptors;
using Microsoft.CodeAnalysis;

namespace AutoInterface.Generator.Templates;

public static class ParameterDescriptorTemplate
{
    public static void Render(in StringBuilder sb, in ParameterDescriptor descriptor, bool isGenericParameter)
    {
        sb.AppendLine();
        if (descriptor.IsParams) 
            sb.Append("params ");

        sb.Append(ToParameterPrefix(descriptor.RefKind));
        sb.Append(' ');
        sb.Append(!isGenericParameter
            ? SlimTypeDescriptorTemplates.Render(descriptor.TypeDescriptor)
            : descriptor.TypeDescriptor.Name);

        sb.Append(' ');
        sb.AppendLine(descriptor.Name);
        
        if (descriptor.Default.IsSet)
        {
            var defaultValue = descriptor.Default.Value switch
            {
                null => "null",
                string s => $"\"{s.Replace("\\", @"\\").Replace("\"", "\\\"")}\"",
                float f => $"{f.ToString(CultureInfo.InvariantCulture)}f",
                double d => d.ToString(CultureInfo.InvariantCulture),
                bool b => b ? "true" : "false",
                _ => descriptor.Default.Value.ToString()
            };
            
            sb.Append($" = {defaultValue}");
        }

    }

    private static string ToParameterPrefix(RefKind kind) =>
        kind switch
        {
            RefKind.Out => "out ",
            RefKind.Ref => "ref ",
            RefKind.In => "in ",
            RefKind.RefReadOnlyParameter => "ref readonly ",
            RefKind.None => string.Empty,
            _ => string.Empty
        };
}