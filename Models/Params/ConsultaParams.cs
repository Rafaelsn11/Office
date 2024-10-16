namespace Office.Models.Params
{
    public class ConsultaParams
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string NomeEspecialidade { get; set; } = string.Empty;

    }
}