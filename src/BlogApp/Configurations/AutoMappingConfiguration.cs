using AutoMapper;
using BlogApp.ViewsModels;
using BlogCore.Business.Models;

namespace BlogApp.Configurations
{
    public class AutoMappingConfiguration : Profile
    {
        public AutoMappingConfiguration()
        {
            CreateMap<PostViewModel, Post>()
                .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => src.DataPublicacao));

            CreateMap<Post, PostViewModel>()
                .ForMember(dest => dest.DataPublicacao, opt => opt.MapFrom(src => src.DataCadastro));

            CreateMap<Autor, AutorViewModel>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Usuario!.UserName));

            CreateMap<ComentarioViewModel, Comentario>()
                .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => src.DataPublicacao));

            CreateMap<Comentario, ComentarioViewModel>()
                .ForMember(dest => dest.NomeUsuario, opt => opt.MapFrom(src => src.Usuario.UserName))
                .ForMember(dest => dest.DataPublicacao, opt => opt.MapFrom(src => src.DataCadastro));
        }
    }
}
