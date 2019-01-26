using System;
using Microsoft.Owin;
using MirrorSharp;
using MirrorSharp.Owin;
using Owin;
using SharpServer;

[assembly: OwinStartup(typeof(Startup), nameof(Startup.Configuration))]

namespace SharpServer
{
    public class Startup
    {
        private static readonly string MscorlibReferencePath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
            + @"\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6\mscorlib.dll";

        public void Configuration(IAppBuilder app)
        {
            app.UseDefaultFiles()
               .UseStaticFiles();

            app.UseMirrorSharp(new MirrorSharpOptions
            {
                SelfDebugEnabled = true,
                IncludeExceptionDetails = true,
                SetOptionsFromClient = new SetOptionsFromClientExtension()
            }
            .SetupCSharp(c => {
                c.MetadataReferences = c.MetadataReferences.Clear();
                c.AddMetadataReferencesFromFiles(MscorlibReferencePath);
            }));
        }
    }
}
