using AutoMapper;
using DG.Haer.Business;
using DG.Haer.Domain;

namespace DG.Haer.Api.Infrastructure.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }
        protected override void Configure()
        {
            Mapper.CreateMap<ContactViewModel, Contact>();
        }
    }
}
