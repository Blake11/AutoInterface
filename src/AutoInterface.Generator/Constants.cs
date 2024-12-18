namespace AutoInterface.Generator;

public static class Constants
{
    public const string GlobalNameSpace = "global::";

    public const string SystemAttribute
        = $"{GlobalNameSpace}System.Attribute";

    public const string SystemAttributeUsage
        = $"{GlobalNameSpace}System.AttributeUsage";

    public const string SystemAttributeTargetsClass
        = $"{GlobalNameSpace}System.AttributeTargets.Class";

    public const string SystemAttributeUsageClass =
        $"{SystemAttributeUsage}({SystemAttributeTargetsClass})";
}