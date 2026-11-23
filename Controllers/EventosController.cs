using Microsoft.AspNetCore.Mvc;
using ExerciciosASPNET.Models;
using ExerciciosASPNET.ViewModels;

namespace ExerciciosASPNET.Controllers
{
    public class EventosController : Controller
    {
        private static List<Evento> _evento = new()
        {
            new Evento { Id = 1, Nome = "The Town", Descricao = "Show de artistas com grande relevancia internacional e nacional, como o Drake.", Local = "São Paulo - SP", DataEvento = new DateTime(2027, 07, 04), QuantidadeVagas = 1000, ValorIngresso = 975m},
            new Evento { Id = 2, Nome = "Lollapalooza", Descricao = "Show de artistas com grande relevancia internacional e nacional, como o Travis Scott.", Local = "São Paulo - SP", DataEvento = new DateTime(2027, 03, 19), QuantidadeVagas = 1000, ValorIngresso = 460m}
        };

        public IActionResult Index()
        {
            return View(_evento);
        }

        // GET /Evento/Criar - retornar a View de Criação de Eventos
        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        // POST /Evento/Criar - recebe dados da View Criar()
        [HttpPost]
        public IActionResult Criar(Evento evento)
        {
            if (!ModelState.IsValid)
            {
                return View(evento);
            }

            evento.Id = _evento.Any() ? _evento.Max(a => a.Id) + 1 : 1; // Ternario
            _evento.Add(evento);

            return RedirectToAction("Index");
        }

        // GET /Eventos/Buscar
        [HttpGet]
        public IActionResult Buscar()
        {
            return View(new BuscaEventoViewModel());
        }

        // POST /Eventos/Buscar
        [HttpPost]
        public IActionResult Buscar(BuscaEventoViewModel vm)
        {
            if (string.IsNullOrEmpty(vm.NomeBusca) &&
                string.IsNullOrEmpty(vm.LocalBusca) &&
                !vm.DataInicial.HasValue) // Verifica se os parametros estão vazios.
            {
                vm.Resultados = new List<Evento>(); // Cria uma lista sem informações.
                return View(vm); 
            }
            
            // Inicia a consulta apontando para a lista de eventos.
            // O IEnumerable funciona como um "rascunho" de filtros: ele acumula as regras dos 'ifs' abaixo
            // e só executa a busca de verdade no final, quando chamamos o .ToList().
            IEnumerable<Evento> consulta = _evento;

            if (!string.IsNullOrEmpty(vm.NomeBusca))
            {
                consulta = consulta.Where(a => a.Nome.Contains(vm.NomeBusca, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(vm.LocalBusca))
            {
                consulta = consulta.Where(a => a.Local.Contains(vm.LocalBusca, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (vm.DataInicial.HasValue)
            {
                consulta = consulta.Where(a => a.DataEvento.Date >= vm.DataInicial.Value.Date).ToList();
            }

            vm.Resultados = consulta.ToList();

            return View(vm);
        }

        public IActionResult Detalhes(int id)
        {
            var evento = _evento.FirstOrDefault(a => a.Id == id);
            if(evento == null)
                return NotFound();
            return View(evento);
        }
    }
}