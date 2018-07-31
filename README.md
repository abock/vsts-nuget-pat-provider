# VSTS PAT Credential Provider for NuGet

A [credential provider for NuGet][nuget-credential-providers] that provides
PAT authentication for package feeds hosted on pkgs.visualstudio.com.

This is primarily a workaround for easily supporting authenticated VSTS NuGet
feeds on macOS/Linux while the existing credential provider only supports
Windows.

## Installation

To build and install the credential provider to `~/.local/share/NuGet/CredentialProviders`:

```
msbuild /restore /t:Install
```

## Configure

PATs are stored in files named after the host component of the repository URI:

```
https://contoso.pkgs.visualstudio.com/...
```

Would have its PAT stored:

```
~/.local/share/NuGet/CredentialProviders/VSSPAT/contoso.pkgs.visualstudio.com
```

## Contributing

This project welcomes contributions and suggestions. Most contributions require
you to agree to a Contributor License Agreement (CLA) declaring that you have
the right to, and actually do, grant us the rights to use your contribution.
For details, visit https://cla.microsoft.com.

When you submit a pull request, a CLA-bot will automatically determine whether
you need to provide a CLA and decorate the PR appropriately (e.g., label,
comment). Simply follow the instructions provided by the bot. You will only
need to do this once across all repositories using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/)
or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any
additional questions or comments.


[nuget-credential-providers]: https://docs.microsoft.com/en-us/nuget/reference/extensibility/nuget-exe-credential-providers