[![.NET](https://github.com/Blake11/AutoInterface/actions/workflows/nuget.yml/badge.svg)](https://github.com/Blake11/AutoInterface/actions/workflows/nuget.yml)
[![NuGet](https://img.shields.io/nuget/dt/Blake11.AutoInterface.svg)](https://www.nuget.org/packages/Blake11.AutoInterface)
[![NuGet](https://img.shields.io/nuget/vpre/Blake11.AutoInterface.svg)](https://www.nuget.org/packages/Blake11.AutoInterface)

# AutoInterface
Yet another source generator that creates interfaces for implementations and auto-implements the said interface.

# How it works
## Define a partial class with some public methods

```
public class MyService
{
    public void HelloWorld()
}
```

## Add \[AutoInterface\] attribute and make it partial
```
[AutoInterface]
public partial class MyService
{
    public void HelloWorld()
}
```

## Now you can use the interface
```
IMyService service = new MyService();
```

# How it works

## Scanning
Source generator will scan the project for partial classes with \[AutoInterfaceAttribute\]

Found classes will be analyzed for public methods, generic arguments and generic argument constrains

## Generation
For matching types there will be generated 2 components:

1. An interface with the classes name
2. A partial class implementing your interface


# Supports:

- public methods
- methods with parameter modifiers
- generic class arguments
- generic class type constrains
- generic method arguments
- generic method type constraints


# Not supported yet:
- docs
- explicit interface name, namespace
- nullable parameters
- probably a bunch more

# FAQ:

### 1. Should I use AutoInterface?
A: Probably not but in some cases it would help.

