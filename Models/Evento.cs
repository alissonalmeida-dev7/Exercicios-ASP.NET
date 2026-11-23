using System.ComponentModel.DataAnnotations;

namespace ExerciciosASPNET.Models
{
    public class Evento
    {
        public int Id { get; set; }

        [StringLength(80, MinimumLength = 5, ErrorMessage = "Nome deve ter entre 5 e 80 caracteres.")]
        [Required(ErrorMessage = "O nome é obrigatorio.")]
        public string Nome { get; set; } = string.Empty;

        [MinLength( 10, ErrorMessage = "A descrição deve ter no minimo 10 caracteres.")]
        [Required(ErrorMessage = "A descrição é obrigatorio.")]
        public string Descricao { get; set; } = string.Empty;
        
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O local deve ter entre 3 e 100 caracteres.")]
        [Required(ErrorMessage = "O local é obrigatorio.")]
        public string Local { get; set; } = string.Empty;
        public DateTime DataEvento { get; set; }

        [Range(1, 1000, ErrorMessage = "Quantidade de vagas deve ser entre 1 e 100.")]
        public int QuantidadeVagas { get; set; }
        public decimal ValorIngresso { get; set; }

    }
}