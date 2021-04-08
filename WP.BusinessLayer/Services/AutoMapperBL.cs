using AutoMapper;
using System;


namespace WP.BusinessLayer.Services
{
    public static class AutoMapperBL<L, R>
    {
        public static R Map(Func<int,L> call, int temp) 
        {
            if (typeof(L).GetGenericArguments().Length > 0) 
            {
                Type left = typeof(L).GetGenericArguments()[0];
                Type right = typeof(R).GetGenericArguments()[0];
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap(left, right)).CreateMapper();
                return mapper.Map<L, R>(call.Invoke(temp));
            }
            else 
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<L, R>()).CreateMapper();
                return mapper.Map<L, R>(call.Invoke(temp));
            }
        }

        public static R Map(Func<L> call)
        {
            if (typeof(L).GetGenericArguments().Length > 0)
            {
                Type left = typeof(L).GetGenericArguments()[0];
                Type right = typeof(R).GetGenericArguments()[0];
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap(left, right)).CreateMapper();
                return mapper.Map<L, R>(call.Invoke());
            }
            else
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<L, R>()).CreateMapper();
                return mapper.Map<L, R>(call.Invoke());
            }
        }

        public static R Map(Func<string, L> call, string temp)
        {
            if (typeof(L).GetGenericArguments().Length > 0)
            {
                Type left = typeof(L).GetGenericArguments()[0];
                Type right = typeof(R).GetGenericArguments()[0];
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap(left, right)).CreateMapper();
                return mapper.Map<L, R>(call.Invoke(temp));
            }
            else
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<L, R>()).CreateMapper();
                return mapper.Map<L, R>(call.Invoke(temp));
            }
        }

        public static R Map(L item)
        {
            if (typeof(L).GetGenericArguments().Length > 0)
            {
                Type left = typeof(L).GetGenericArguments()[0];
                Type right = typeof(R).GetGenericArguments()[0];
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap(left, right)).CreateMapper();
                return mapper.Map<L, R>(item);
            }
            else
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<L, R>()).CreateMapper();
                return mapper.Map<L, R>(item);
            }
        }
    }
}
