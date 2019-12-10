using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Connect4.Data;
using Connect4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Connect4.Controllers
{
    public class TorneiosController : Controller
    {
        private readonly ApplicationDbContext _context;

        private int torneioID { get; set; }

        private UserManager<ApplicationUser> _userManager { get; set; }

        public TorneiosController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
           this._userManager = userManager;
           this. _context = context;
        }

        // GET: Torneios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Torneio.ToListAsync());
        }

        // GET: Torneios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var torneio = await _context.Torneio
                .SingleOrDefaultAsync(m => m.Id == id);
            if (torneio == null)
            {
                return NotFound();
            }

            return View(torneio);
        }

        [Authorize]
        // GET: Torneios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Torneios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,NomeTorneio,QuantidadeJogadores,Premiacao,Inicio")] Torneio torneio)
        {
            if (ModelState.IsValid)
            {
                torneio.Dono = User.Identity.Name;
                _context.Add(torneio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(torneio);
        }

        // GET: Torneios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var torneio = await _context.Torneio.SingleOrDefaultAsync(m => m.Id == id);
            if (torneio == null)
            {
                return NotFound();
            }
            return View(torneio);
        }

        // GET: Torneios/Edit/5
        public async Task<IActionResult> Entrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var torneio = await _context.Torneio.Include(j => j.Jogadores).SingleOrDefaultAsync(m => m.Id == id);

            int? jogadorId =
                _userManager.GetUserAsync(User).Result.JogadorId;

            var jogador = _context.JogadorPessoas
                                    .Include(j => j.Usuario)
                                    .Where(j => j.Id == jogadorId)
                                    .FirstOrDefault();

            torneio.Jogadores.Add(jogador);

            _context.SaveChanges();
            if (torneio == null)
            {
                return NotFound();
            }
            return Ok("Entrou com sucesso.");
        }

        public async Task<IActionResult> LobbyTorneio(int? id)
        {

            var torneio = await _context.Torneio.Include(j => j.Jogos).Include(j => j.Jogadores).SingleOrDefaultAsync(m => m.Id == id);

            int? jogadorId =
                    _userManager.GetUserAsync(User).Result.JogadorId;

            var jogo = torneio.Jogos.Where(j => j.Jogador1Id == jogadorId || j.Jogador2Id == jogadorId).FirstOrDefault();

            return View(torneio);


        }

        public async Task<IActionResult> Comecar(int? id)
        {

            var torneio = await _context.Torneio.Include(j => j.Jogos).Include(j => j.Jogadores).SingleOrDefaultAsync(m => m.Id == id);

            int? jogadorId =
                    _userManager.GetUserAsync(User).Result.JogadorId;


            return RedirectToAction(nameof(JogoController.Lobby), "Jogo",
                new { id = id });
        }



            // GET: Torneios/Edit/5
            public async Task<IActionResult> Iniciar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var torneio = await _context.Torneio.Include(j => j.Jogadores).SingleOrDefaultAsync(m => m.Id == id);

            if (torneio.Jogadores.Count == torneio.QuantidadeJogadores)
            {
                MontarJogos(id);

                return Ok("Torneio iniciado.");
            }
            else
            {
                return Forbid("O torneio precisa estar preenchido com todos os jogadores.");
            }
        }

        // GET: Torneios/Edit/5
        public void MontarJogos(int? id)
        {
            var torneio = _context.Torneio.Include(j => j.Jogos).SingleOrDefault(m => m.Id == id);

            var LstJogadores = torneio.Jogadores;

            var Jogo = new Jogo();

            foreach (Jogador jogador in LstJogadores)
            {

                if(Jogo.Jogador1 == null)
                {
                    Jogo.Jogador1 = jogador;
                    Jogo.Jogador1Id = jogador.Id;
                    _context.Jogos.Add(Jogo);
                    torneio.Jogos.Add(Jogo);
                    _context.SaveChanges();
                }
                else
                {
                    Jogo.Jogador2 = jogador;
                    Jogo.Jogador2Id = jogador.Id;
                    _context.Jogos.Update(Jogo);
                    _context.SaveChanges();
                    Jogo = new Jogo();
                }
           
            }
        }

        // POST: Torneios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeTorneio,QuantidadeJogadores,Premiacao,Inicio")] Torneio torneio)
        {
            if (id != torneio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(torneio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TorneioExists(torneio.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(torneio);
        }

        // GET: Torneios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var torneio = await _context.Torneio
                .SingleOrDefaultAsync(m => m.Id == id);
            if (torneio == null)
            {
                return NotFound();
            }
            if (User.Identity.Name != torneio.Dono)
            {
                return Forbid();
            }
            return View(torneio);
        }

        // POST: Torneios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var torneio = await _context.Torneio.SingleOrDefaultAsync(m => m.Id == id);
            if (User.Identity.Name != torneio.Dono)
            {
                return Forbid();
            }
            _context.Torneio.Remove(torneio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TorneioExists(int id)
        {
            return _context.Torneio.Any(e => e.Id == id);
        }
    }
}
