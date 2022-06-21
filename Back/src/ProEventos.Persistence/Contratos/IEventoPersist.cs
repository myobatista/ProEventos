using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IEventoPersist
    {
        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<Evento> GetEventoByTemaAsync(string tema, bool includePalestrantes = false);
        Task<Evento> GetEventoByIdAsync(int EventoId, bool includePalestrantes = false);
    }
}