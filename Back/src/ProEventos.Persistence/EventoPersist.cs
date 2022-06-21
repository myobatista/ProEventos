using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class EventoPersist : IEventoPersist
    {
        private readonly ProEventosContext _context;

        public EventoPersist(ProEventosContext context)
        {
            _context = context;
            
            // Para corrigir o erro de tracking quando é feito o select no banco e a query está usando o registro e não consege atualizar
            // Esse erro tbm pode ser corrigido passando o AsNoTracking() na query igual fiz no PalestrantePersist
           _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);
            
            if(includePalestrantes)
            {
                query = query
                    .Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
            
            query = query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);
            
            if(includePalestrantes)
            {
                query = query
                    .Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
            
            query = query.OrderBy(e => e.Id)
                         .Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);
            
            if(includePalestrantes)
            {
                query = query
                    .Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
            
            query = query.OrderBy(e => e.Id)
                         .Where(e => e.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }
   }
}