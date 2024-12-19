namespace AutoInterface.Generator.Templates;

public static class GenerateAutoInterfaceAttributeTemplate
{
    private const string AttributeName = "AutoInterface";
    
    public const string ClassName = $"{AttributeName}Attribute";
    public const string FileName = $"{ClassName}.g.cs";

    public const string Code =
        $$"""
          {{CommonTemplates.AutoGeneratedAnnotation}}

          [{{Constants.SystemAttributeUsageClass}}]
          public class {{ClassName}} : {{Constants.SystemAttribute}}
          {
          }
          """;
}