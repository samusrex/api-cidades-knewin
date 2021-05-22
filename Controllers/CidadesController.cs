using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public CidadesController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Cidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CidadesResponse>>> GetCidades()
        {
            return await (from c in _context.Cidades.Include(c => c.Fronteiras)
                          select new CidadesResponse(c)).ToListAsync();
        }

        // GET: api/Cidades/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<CidadesResponse>> GetCidades(int id)
        {
            var cidades = await _context.Cidades.Include(c => c.Fronteiras).Where(c => c.Id == id).FirstOrDefaultAsync();

            if (cidades == null)
            {
                return NotFound();
            }

            return new CidadesResponse(cidades);
        }

        [HttpGet("{id_origem}/{id_destino}")]
        public async Task<ActionResult<string[]>> PathFinder(int id_origem, int id_destino)
        {

            Dictionary<int, Node> cidades = await _context.Cidades.Include(c => c.Fronteiras).Select(c => new Node(c)).ToDictionaryAsync(n => n.Cidade.Id);

            foreach (KeyValuePair<int, Node> cidade in cidades)
            {
                foreach (Fronteiras fronteira in cidade.Value.Cidade.Fronteiras)
                {

                    if (cidades.TryGetValue(fronteira.CidadesId2, out Node connectionNode))
                    {
                        cidade.Value.ConnectTo(connectionNode, 1);
                    }

                }
            }

            Dijkstra pathFinder = new Dijkstra();

            if (cidades.TryGetValue(id_origem, out Node origem) & cidades.TryGetValue(id_destino, out Node destino))
            {
                return pathFinder.FindShortestPath(origem, destino).Select(n => n.Cidade.Name).ToArray();
            }

            return System.Array.Empty<string>();

        }

        // PUT: api/Cidades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCidades(int id, Cidades cidades)
        {
            if (id != cidades.Id)
            {
                return BadRequest();
            }

            _context.Update(cidades);
            _context.ChangeTracker.DetectChanges();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CidadesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cidades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cidades>> PostCidades(Cidades cidades)
        {
            _context.Cidades.Add(cidades);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCidades", new { id = cidades.Id }, cidades);
        }

        // DELETE: api/Cidades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCidades(int id)
        {
            var cidades = await _context.Cidades.FindAsync(id);
            if (cidades == null)
            {
                return NotFound();
            }

            _context.Cidades.Remove(cidades);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CidadesExists(int id)
        {
            return _context.Cidades.Any(e => e.Id == id);
        }
    }
}
