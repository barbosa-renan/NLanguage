# NLanguage

NLanguage is a easy way to translate terms in your API projects using IStringLocalizer with .json files instead of .resx files


| Package |  Version | Downloads |
| ------- | ----- | ----- |
| `NLanguage` | [![NuGet](https://img.shields.io/badge/nuget-1.0.2-blue.svg)](https://www.nuget.org/packages/NLanguage) | [![Nuget](https://img.shields.io/badge/downloads-%2B30-green)](https://www.nuget.org/packages/NLanguage) |


### Dependencies
.NET Standard 2.0

You can check supported frameworks here:

https://docs.microsoft.com/pt-br/dotnet/standard/net-standard

### Instalation
This package is available through Nuget Packages: https://www.nuget.org/packages/NLanguage

Install on your WebAPI project

**Nuget**
```
Install-Package NLanguage
```

**.NET CLI**
```
dotnet add package NLanguage
```

**Add .json files to Languages folder in WebAPI Project**
```
{
  "hello": "Hello",
  "welcome": "Welcome {0}, How are you?"
}
```

**Pattern to naming .json files for each language**
```
pt-BR.json
en-US.json
```

## How to use
```csharp
// In Startup.cs class
public void ConfigureServices(IServiceCollection services)
{
    //NLanguage
    services.AddNLanguage();
    services.AddDistributedMemoryCache();
}

  ...

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    //NLanguage
    app.UseNLanguage();
}

  ...
  
// In other classes, use IStringLocalizer
private readonly IStringLocalizer<YourClass> _localizer;

public Translator(IStringLocalizer<YourClass> localizer)
{
    _localizer = localizer;
}

  ...
  
public string Translate(string key)
{
    if (string.IsNullOrEmpty(key)) return key;
    return _localizer[key];
}
```
