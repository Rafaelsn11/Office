using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Office.Models.Dtos;
using Office.Models.Entities;
using Office.Repository.Interfaces;

namespace Office.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EspecialidadeController : ControllerBase
{
    private readonly IEspecialidadeRepository _repository;
    private readonly IMapper _mapper;

    public EspecialidadeController(IEspecialidadeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var especialidades = await _repository.GetEspecialidades();

        return especialidades.Any()
            ? Ok(especialidades)
            : NotFound("Especialidades não encontradas");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        if (id <= 0) return BadRequest("Dados inválidos");

        var especialidade = await _repository.GetEspecialidadeById(id);

        var especialidadeRetorno = _mapper.Map<EspecialidadeDetalhesDto>(especialidade);

        return especialidadeRetorno != null
            ? Ok(especialidade)
            : NotFound("Especialidades não encontradas");
    }

    [HttpPost]
    public async Task<IActionResult> Post(EspecialidadeAdicionarDto especialidade)
    {
        if (string.IsNullOrEmpty(especialidade.Nome)) return BadRequest("Nome inválido");

        var especialidadeAdicionar = new Especialidade
        {
            Nome = especialidade.Nome,
            Ativa = especialidade.Ativa
        };

        _repository.Add(especialidadeAdicionar);

        return await _repository.SaveChangesAsync()
            ? Ok("Especialidade adicionadas")
            : BadRequest("Erro ao adicionar especialidade");
    }

    [HttpPut("{id}/atualizar-status/")]
    public async Task<IActionResult> Put(int id, bool ativo)
    {
        if (id <= 0) return BadRequest("Dados inválidos");

        var especialidade = await _repository.GetEspecialidadeById(id);

        if (especialidade == null) return NotFound("especialidade não existe na base de dados");

        string status = ativo ? "ativa" : "inativa";
        if (especialidade.Ativa == ativo) return Ok($"A especialidade já está {status}");

        especialidade.Ativa = ativo;

        _repository.Update(especialidade);

        return await _repository.SaveChangesAsync()
            ? Ok("Status atualizada")
            : BadRequest("Erro ao atualizar status");

    }
}
