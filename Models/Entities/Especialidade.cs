namespace Office.Models.Entities;

public class Especialidade : Base
{
    public string Nome { get; set; }
    public bool Ativa { get; set; }
    public List<Profissional> Profissionais { get; set; }
    public List<Consulta> Consultas { get; set; }
}
