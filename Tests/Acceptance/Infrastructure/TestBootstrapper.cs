using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using HypermediaEngine;
using Nancy;
using Nancy.TinyIoc;

namespace Acceptance.Infrastructure
{
    internal class TestBootstrapper : Bootstrapper
    {
        private readonly Action<TinyIoCContainer> _stubsRegistration;

        public TestBootstrapper(Action<TinyIoCContainer> stubsRegistration)
        {
            _stubsRegistration = stubsRegistration;
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            IgnoreTestAssemblies();
            base.ConfigureApplicationContainer(container);

            if (_stubsRegistration != null)
                _stubsRegistration.Invoke(container);
        }

        private static void IgnoreTestAssemblies()
        {
            var autoRegisterIgnoredAssemblies = new List<Func<Assembly, bool>>(DefaultAutoRegisterIgnoredAssemblies) { asm => asm.FullName.Contains("Acceptance") };
            DefaultAutoRegisterIgnoredAssemblies = autoRegisterIgnoredAssemblies;
        }
    }

    public class TestingRootPathProvider : IRootPathProvider
    {
        private static readonly string RootPath;

        static TestingRootPathProvider()
        {
            var directoryName = Path.GetDirectoryName(typeof(Bootstrapper).Assembly.CodeBase);

            if (directoryName != null)
            {
                var assemblyPath = directoryName.Replace(@"file:\", string.Empty);

                RootPath = Path.Combine(assemblyPath, "..", "..", "..", "..", "HypermediaEngine", "Website");
            }
        }

        public string GetRootPath()
        {
            return RootPath;
        }
    }
}