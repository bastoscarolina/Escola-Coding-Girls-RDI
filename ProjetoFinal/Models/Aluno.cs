using System.Text.Json.Serialization;

namespace ProjetoFinal.Models
{
    public class Aluno
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public char Sexo { get; set; }
        public int TotalFaltas { get; set; }
        public int TurmaID { get; set; }

        #region  Navigation Properties
        [JsonIgnore]
        public virtual Turma? Turma { get;set; }
        #endregion

    }
}
