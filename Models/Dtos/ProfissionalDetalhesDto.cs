namespace Office.Models.Dtos;

public class ProfissionalDetalhesDto
{
    public string Nome { get; set; }
    public bool Ativo { get; set; }
    public int TotalConsultas { get; set; }
    public List<string> Especialidades { get; set; }
}
