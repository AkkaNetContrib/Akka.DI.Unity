//-----------------------------------------------------------------------
// <copyright file="UnityDependencyResolverSpec.cs" company="Akka.NET Project">
//     Copyright (C) 2009-2016 Lightbend Inc. <http://www.lightbend.com>
//     Copyright (C) 2013-2016 Akka.NET project <https://github.com/akkadotnet/akka.net>
// </copyright>
//-----------------------------------------------------------------------

using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.TestKit;
using System;
using System.Diagnostics;
using Unity;
using Unity.Lifetime;

namespace Akka.DI.Unity.Tests
{
    public class UnityDependencyResolverSpec : DiResolverSpec
    {
        protected override object NewDiContainer()
        {
            return new UnityContainer();
        }

        protected override IDependencyResolver NewDependencyResolver(object diContainer, ActorSystem system)
        {
            var unityContainer = ToContainer(diContainer);
            return new UnityDependencyResolver(unityContainer, system);
        }

        protected override void Bind<T>(object diContainer, Func<T> generator)
        {
            var unityContainer = ToContainer(diContainer);
            unityContainer.RegisterFactory<T>(ctx => generator(), new HierarchicalLifetimeManager());
        }

        protected override void Bind<T>(object diContainer)
        {
            var unityContainer = ToContainer(diContainer);
            unityContainer.RegisterType<T>();
        }

        private static IUnityContainer ToContainer(object diContainer)
        {
            var unityContainer = diContainer as IUnityContainer;
            Debug.Assert(unityContainer != null, "unityContainer != null");
            return unityContainer;
        }
    }
}