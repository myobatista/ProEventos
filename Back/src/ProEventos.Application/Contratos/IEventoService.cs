using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Application.Contratos
{
    public interface IEventoService
    {
        Task<Evento> AddEvento(Evento model);
        Task<Evento> UpdateEvento(int EventoId, Evento model);
        Task<bool> DeleteEvento(int EventoId);

        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<Evento> GetEventoByTemaAsync(string tema, bool includePalestrantes = false);
        Task<Evento> GetEventoByIdAsync(int EventoId, bool includePalestrantes = false);
    }
}