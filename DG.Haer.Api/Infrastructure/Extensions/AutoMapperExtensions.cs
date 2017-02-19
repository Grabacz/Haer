using AutoMapper;
using DG.Haer.Business;
using DG.Haer.Domain;
using System.Collections.Generic;

namespace DG.Haer.Api
{
    public static class AutoMapperExtensions
    {
        public class MapWrapper
        {
            public object Source { get; set; }
        }

        public static MapWrapper Map(this object @this)
        {
            return new MapWrapper { Source = @this };
        }

        public static TDest ToObject<TDest>(this MapWrapper @this)
        {
            return Mapper.Map<TDest>(@this.Source);
        }
        public static List<TDest> ToList<TDest>(this MapWrapper @this)
        {
            return Mapper.Map<List<TDest>>(@this.Source);
        }

        public static TDest ToBussinessObject<TDest>(this Entity @this) where TDest : ViewModel
        {
            return Mapper.Map<TDest>(@this);
        }

        public static IEnumerable<TDest> ToBussinessObjects<TDest>(this IEnumerable<Entity> @this) where TDest : ViewModel
        {
            return Mapper.Map<List<TDest>>(@this);
        }

        public static TDest ToDomainObject<TDest>(this ViewModel @this) where TDest : Entity
        {
            return Mapper.Map<TDest>(@this);
        }

        public static IEnumerable<TDest> ToDomainObjects<TDest>(this IEnumerable<ViewModel> @this) where TDest : Entity
        {
            return Mapper.Map<List<TDest>>(@this);
        }
    }
}
