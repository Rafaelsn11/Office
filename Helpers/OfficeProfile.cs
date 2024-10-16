using AutoMapper;
using Office.Models.Dtos;
using Office.Models.Entities;

namespace Office.Helpers;

public class OfficeProfile : Profile
{
    public OfficeProfile()
    {
        CreateMap<Paciente, PacienteDetalhesDto>().ReverseMap();
        // .ForMember(destino => destino.Email, origem => origem.Ignore());
        CreateMap<Paciente, PacienteDto>();
        CreateMap<ConsultaDto, Consulta>()
            .ForMember(destino => destino.Profissional, opt => opt.Ignore())
            .ForMember(destino => destino.Especialidade, opt => opt.Ignore());
        CreateMap<Consulta, ConsultaDto>()
            .ForMember(destino => destino.Especialidade, opt => opt.MapFrom(origem => origem.Especialidade.Nome))
            .ForMember(destino => destino.Profissional, opt => opt.MapFrom(origem => origem.Profissional.Nome));

        CreateMap<Consulta, ConsultaDetalhesDto>();
        CreateMap<ConsultaAdicionarDto, Consulta>();
        CreateMap<ConsultaAtualizarDto, Consulta>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
            srcMember != null));

        CreateMap<PacienteAdicionarDto, Paciente>();

        CreateMap<PacienteAtualizarDto, Paciente>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
            srcMember != null));

        CreateMap<Profissional, ProfissionalDetalhesDto>()
            .ForMember(dest => dest.TotalConsultas, opt => opt.MapFrom(src => src.Consultas.Count()))
            .ForMember(dest => dest.Especialidades, opt => opt.MapFrom(src =>
            src.Especialidades.Select(x => x.Nome).ToList()));
        CreateMap<Profissional, ProfissionalDto>();

        CreateMap<ProfissionalAdicionarDto, Profissional>();
        CreateMap<ProfissionalAtualizarDto, Profissional>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
            srcMember != null));

        CreateMap<Especialidade, EspecialidadeDetalhesDto>();
        CreateMap<Especialidade, EspecialidadeDto>();
    }
}
