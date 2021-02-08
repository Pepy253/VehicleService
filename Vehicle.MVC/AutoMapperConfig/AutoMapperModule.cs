﻿using AutoMapper;
using Ninject;
using Ninject.Modules;

namespace Vehicle.MVC.AutoMapperConfig
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            Bind<IMapper>().ToMethod(ctx =>
                new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.AddMaps(GetType().Assembly);
            });

            return config;
        }
    }
}