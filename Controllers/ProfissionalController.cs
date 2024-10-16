using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Office.Models.Dtos;
using Office.Models.Entities;
using Office.Repository.Interfaces;

namespace Office.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfissionalController : ControllerBase
{
    private readonly IProfissionalRepository _repository;
    private readonly IMapper _mapper;
    public ProfissionalController(IProfissionalRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var profissionais = await _repository.GetProfissionaisAsync();

        return profissionais.Any()
        ? Ok(profissionais)
        : NotFound("Profissionais não encontrados");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0) return BadRequest("Id inválido");

        var profissional = await _repository.GetProfissionalByIdAsync(id);

        var profissionalRetorno = _mapper.Map<ProfissionalDetalhesDto>(profissional);

        return profissionalRetorno != null
            ? Ok(profissionalRetorno)
            : NotFound("Profissional não existe na base de dados");
    }

    [HttpPost]
    public async Task<IActionResult> Post(ProfissionalAdicionarDto profissional)
    {
        if (string.IsNullOrEmpty(profissional.Nome)) return BadRequest("Dados inválidos");

        var profissionalAdicionar = _mapper.Map<Profissional>(profissional);

        _repository.Add(profissionalAdicionar);

        return await _repository.SaveChangesAsync()
            ? Ok("Profissional adicionado")
            : BadRequest("Erro ao adicionar o Profissional");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(ProfissionalAtualizarDto profissional, int id)
    {
        if (id <= 0) return BadRequest("Id inválido");

        var profissionalBanco = await _repository.GetProfissionalByIdAsync(id);

        if (profissionalBanco == null) return NotFound("Profissional não encontrado na base de dados");

        var profissionalAtualizar = _mapper.Map(profissional, profissionalBanco);

        _repository.Update(profissionalAtualizar);

        return await _repository.SaveChangesAsync()
            ? Ok("Atualizado com sucesso")
            : BadRequest("Erro ao atualizar o Profissional");
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0) return BadRequest("Profissional inválido");

        var profissionalExclui = await _repository.GetProfissionalByIdAsync(id);

        if (profissionalExclui == null) return BadRequest("Profissional não encontrado");

        _repository.Delete(profissionalExclui);

        return await _repository.SaveChangesAsync()
            ? Ok("Profissional deletado com sucesso")
            : BadRequest("Erro ao deletar o profissional");
    }

    [HttpPost("adicionar-profissional")]
    public async Task<IActionResult> PostProfissionalEspecialidade(ProfissionalEspecialidadeAdicionarDto profissional)
    {
        int profissionalId = profissional.ProfissionalId;
        int especialidadeId = profissional.EspecialidadeId;

        if (profissionalId <= 0 || especialidadeId <= 0) return BadRequest("Dados inválidos");

        var profissionalEspecialidade = await _repository.GetProfissionalEspecialidade(profissionalId, especialidadeId);

        if (profissionalEspecialidade != null) return Ok("Especialidade já cadastrada");

        var especialidadeAdicionar = new ProfissionalEspecialidade
        {
            EspecialidadeId = especialidadeId,
            ProfissionalId = profissionalId
        };

        _repository.Add(especialidadeAdicionar);

        return await _repository.SaveChangesAsync()
            ? Ok("Especialidade adicionada com sucesso")
            : BadRequest("Erro ao adicionar especialidade");
    }

    [HttpDelete("{profissionalId}/deletar-especialidade/{especialidadeId}")]
    public async Task<IActionResult> DeleteProfissionalEspecialidade(int profissionalId, int especialidadeId)
    {
        if (profissionalId <= 0 || especialidadeId <= 0) return BadRequest("Dados inválidos");

        var profissionalEspecialidade = await _repository.GetProfissionalEspecialidade(profissionalId, especialidadeId);

        if (profissionalEspecialidade == null) return Ok("Especialidade não cadastrada");

        _repository.Delete(profissionalEspecialidade);

        return await _repository.SaveChangesAsync()
            ? Ok("Especialidade deletada do profissional")
            : BadRequest("Erro ao deletar especialidade");
    }
}
