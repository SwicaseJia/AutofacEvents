﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.Variance;

namespace Autofac.Events.Tests
{
    public abstract class UnitTests
    {
        public ILifetimeScope BeginScope(Action<ContainerBuilder> additionalConfig = null)
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterEventing();
            builder.RegisterInstance(this).AsSelf().AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(LifetimeScopeExtensions).Assembly, GetType().Assembly)
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            additionalConfig?.Invoke(builder);
            return builder.Build();
        }
    }
}
