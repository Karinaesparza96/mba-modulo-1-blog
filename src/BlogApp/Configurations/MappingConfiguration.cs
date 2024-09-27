using AutoMapper;
using BlogApp.ViewsModels;
using BlogCore.Business.Models;

namespace BlogApp.Configurations
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<PostViewModel, Post>()
                .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => src.DataPublicacao));

            CreateMap<Post, PostViewModel>()
                .ForMember(dest => dest.DataPublicacao, opt => opt.MapFrom(src => src.DataCadastro));

            CreateMap<AutorViewModel, Autor>().ReverseMap();
                
            CreateMap<ComentarioViewModel, Comentario>()
                .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => src.DataPublicacao))
                .ForPath(dest => dest.Usuario.UserName, opt => opt.MapFrom(src => src.NomeUsuario));

            CreateMap<Comentario, ComentarioViewModel>()
                .ForMember(dest => dest.NomeUsuario, opt => opt.MapFrom(src => src.Usuario.UserName))
                .ForMember(dest => dest.DataPublicacao, opt => opt.MapFrom(src => src.DataCadastro));
        }
    }
}
