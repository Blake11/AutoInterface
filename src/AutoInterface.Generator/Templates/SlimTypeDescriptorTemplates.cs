using AutoInterface.Generator.Core.Descriptors;

namespace AutoInterface.Generator.Templates;

public static class SlimTypeDescriptorTemplates
{
    public static string Render(SlimTypeDescriptor typeDescriptor) =>
        typeDescriptor switch
        {
            { Namespace: "System", Name: "Void" } => "void",
            _ => $"{Constants.GlobalNameSpace}{typeDescriptor.Namespace}.{typeDescriptor.Name}"
        };
}