using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

static class CredentialProviderVSSPAT
{
    enum Verbosity
    {
        Normal,
        Quiet,
        Detailed
    }

    const int Success = 0;
    const int ProviderNotApplicable = 1;
    const int Failure = 2;

    [DataContract]
    sealed class Response
    {
        [DataMember] public string Message;
        [DataMember] public string Username;
        [DataMember] public string Password;

        public override string ToString ()
        {
            using (var stream = new MemoryStream ()) {
                new DataContractJsonSerializer (GetType ()).WriteObject (stream, this);
                stream.Position = 0;
                return Encoding.UTF8.GetString (stream.ToArray ());
            }
        }
    }

    public static int Main (string [] args)
    {
        Uri uri = null;
        Verbosity verbosity = default;
        #pragma warning disable
        bool isRetry = false;
        bool nonInteractive = false;
        #pragma warning restore

        for (int i = 0; i < args.Length; i++) {
            switch (args [i].ToLowerInvariant ()) {
            case "-uri":
                if (i < args.Length - 1)
                    Uri.TryCreate (args [++i], UriKind.Absolute, out uri);
                break;
            case "-verbosity":
                if (i < args.Length - 1)
                    Enum.TryParse<Verbosity> (args [++i], true, out verbosity);
                break;
            case "-isretry":
                isRetry = true;
                break;
            case "-noninteractive":
                nonInteractive = true;
                break;
            }
        }

        if (uri == null)
            return ProviderNotApplicable;

        if (!uri.Host.EndsWith (".pkgs.visualstudio.com", StringComparison.OrdinalIgnoreCase))
            return ProviderNotApplicable;

        var patDir = Path.Combine (
            Path.GetDirectoryName (Assembly.GetEntryAssembly ().Location),
            "VSSPAT");

        var patPath = Path.Combine (patDir, uri.Host);

        // be nice and create the directory so the user
        // can simply echo the PAT directly to a file
        Directory.CreateDirectory (patDir);

        if (!File.Exists (patPath)) {
            Console.WriteLine (new Response { Message = $"PAT file does not exist: {patPath}" });
            return Failure;
        }

        Console.WriteLine (new Response {
            Username = "vssuser",
            Password = File.ReadAllText (patPath).Trim ()
        });

        return Success;
    }
}