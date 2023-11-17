# Integration Test

This readme provides a brief overview of the libraries used in your integration test project along with their versions. Ensure you have the necessary NuGet packages installed and configured before running your tests.

## Libraries and Versions:

1. **Bogus (v34.0.2):**
    - Description: A simple and easy-to-use library for generating fake data.
    - [Bogus GitHub Repository](https://github.com/bchavez/Bogus)

2. **FluentAssertions (v7.0.0-alpha.1):**
    - Description: FluentAssertions is a set of .NET extension methods that allow you to more naturally specify the expected outcome of a TDD or BDD-style test.
    - [FluentAssertions GitHub Repository](https://github.com/fluentassertions/fluentassertions)

3. **Meziantou.Extensions.Logging.Xunit (v1.0.6):**
    - Description: Provides extensions to integrate Xunit with the ASP.NET Core logging system.
    - [Meziantou.Extensions.Logging.Xunit GitHub Repository](https://github.com/meziantou/Meziantou.Extensions.Logging.Xunit)

4. **Microsoft.AspNetCore.Mvc.Testing (v8.0.0-rc.1.23421.29):**
    - Description: A package that simplifies the creation of integration tests for ASP.NET Core MVC applications.
    - [Microsoft.AspNetCore.Mvc.Testing NuGet Page](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Testing/)

5. **Microsoft.NET.Test.Sdk (v17.8.0-preview-23424-02):**
    - Description: The .NET Test SDK is a set of assemblies, NuGet packages, and tools that enable creating and running unit tests using the MSTest framework.
    - [Microsoft.NET.Test.Sdk GitHub Repository](https://github.com/microsoft/testfx)

6. **Testcontainers (v3.5.0):**
    - Description: A .NET library for running Docker containers in your integration tests.
    - [Testcontainers GitHub Repository](https://github.com/isen-ng/testcontainers-dotnet)

7. **Testcontainers.PostgreSql (v3.5.0):**
    - Description: Extension for Testcontainers to support PostgreSQL containers in your integration tests.
    - [Testcontainers.PostgreSql GitHub Repository](https://github.com/isen-ng/testcontainers-dotnet)

8. **WireMock.Net (v1.5.36):**
    - Description: A flexible library for stubbing and mocking HTTP services.
    - [WireMock.Net GitHub Repository](https://github.com/WireMock-Net/WireMock.Net)

9. **xunit (v2.5.1):**
    - Description: xUnit.net is a free, open-source, community-focused unit testing tool for the .NET Framework.
    - [xUnit.net GitHub Repository](https://github.com/xunit/xunit)

10. **Xunit.Priority (v1.1.6):**
    - Description: A simple library to support test prioritization in xUnit.net.
    - [Xunit.Priority GitHub Repository](https://github.com/microsoft/vstest)

11. **xunit.runner.visualstudio (v2.5.2-pre.3):**
    - Description: The Visual Studio runner for xUnit.net.
    - [xUnit.net Visual Studio Runner GitHub Repository](https://github.com/xunit/visualstudio.xunit)

## Usage:

Ensure that you have the necessary NuGet packages installed and configured in your project. You can use the following commands in the Package Manager Console:

```bash
dotnet restore
```

## Running Tests:

Use your preferred test runner (e.g., `dotnet test` or Visual Studio's Test Explorer) to execute your integration tests.

Feel free to customize this readme based on your project's specific requirements and additional configurations.
