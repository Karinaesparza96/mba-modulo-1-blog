using AutoMapper;
using BlogApi.DTOs;
using BlogCore.Business.Models;

namespace BlogApi.Configurations;

public class AutoMappingConfiguration : Profile
{
    public AutoMappingConfiguration()
    {
        CreateMap<PostDto, Post>()
            .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => src.DataPublicacao))
            .ForMember(dest => dest.Comentarios, opt => opt.Ignore())
            .ForMember(dest => dest.Autor, opt => opt.Ignore());

        CreateMap<Post, PostDto>()
            .ForMember(dest => dest.DataPublicacao, opt => opt.MapFrom(src => src.DataCadastro));

        CreateMap<Autor, AutorDto>()
            .ForMember(dest => dest.NomeUsuario, opt => opt.MapFrom(src => src.Usuario.UserName));

        CreateMap<AutorDto, Autor>();

        CreateMap<ComentarioDto, Comentario>()
            .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => src.DataPublicacao));

        CreateMap<Comentario, ComentarioDto>()
            .ForMember(dest => dest.NomeUsuario, opt => opt.MapFrom(src => src.Usuario.UserName))
            .ForMember(dest => dest.DataPublicacao, opt => opt.MapFrom(src => src.DataCadastro));
    }
}
