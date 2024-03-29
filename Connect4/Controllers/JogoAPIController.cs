﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Connect4.Data;
using Connect4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Connect4.Controllers
{
    [Produces("application/json")]
    [Route("api/Jogo")]
    public class JogoAPIController : Controller
    {
        private UserManager<ApplicationUser> _userManager { get; set; }
        private SignInManager<ApplicationUser> _signInManager { get; set; }
        private ILogger<ManageController> _logger { get; set; }

        private ApplicationDbContext _context { get; set; }
        public JogoAPIController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          ILogger<ManageController> logger,
          ApplicationDbContext context
          )
        {
            _userManager = userManager;
            _signInManager = signInManager;           
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "Obter")]
        [Route("Obter")]
        [Authorize]
        public Tabuleiro ObterJogo()
        {
            Tabuleiro t = null;
            try
            {
                t = new Tabuleiro();
                _context.Tabuleiros.Add(t);
                _context.SaveChanges();
            }catch(Exception e)
            {
                _logger.LogCritical(e, e.Message, null);
            }
            return t;
        }


        [HttpGet(Name = "Obter")]
        [Route("Obter/{id}")]
        [Authorize]
        public Tabuleiro ObterJogo(int id)
        {
            var jogo = _context.Jogos
                .Include(j => j.Tabuleiro)
                .Include(j =>  j.Jogador1)
                .Include(j => j.Jogador2)
                .Where(j => j.Id == id)
                .FirstOrDefault();

            if (jogo == null)
            {
                throw new ApplicationException("Não Existe o Jogo");
            }
            //TODO: Verificar Permissão antes.
            if (jogo.Tabuleiro != null)
            {                
                return jogo.Tabuleiro;
            }
            jogo.Tabuleiro = new Tabuleiro();
            _context.SaveChanges();
            return jogo.Tabuleiro;
        }

        [HttpPost(Name = "Vencedor")]
        [Route("Vencedor")]
        public int Vencedor(Tabuleiro t)
        {
            return t.Vencedor();
        }

        [HttpPost(Name = "Jogar")]
        [Route("Jogar")]
        [Authorize]
        //(...)/Jogar?JogoId=1&Pos=4
        public IActionResult Jogar([FromQuery] int JogoId, 
            [FromQuery]int Pos)
        {
            var jogo = _context.Jogos
                .Include(j => j.Tabuleiro)
                .Where(j => j.Id == JogoId)
                .FirstOrDefault();
            if(jogo == null)
            {
                return NotFound();
            }
            if(jogo.Tabuleiro == null)
            {
                return BadRequest();
            }
            //TODO: Pegar o usuário autenticado. DONE
            //Verificar se ele é o jogador 1 ou 2. DONE
            //Verificar se ele pode fazer a jogada. DONE
            //Por último executar a jogada ou exceção. DONE
            int? jogadorId =
                _userManager.GetUserAsync(User).Result.JogadorId;

            if (jogadorId is null)
            {
                return NotFound();
            }
            if (jogadorId == jogo.Jogador1Id)
            {
                if (jogadorId == jogo.Tabuleiro.Turno)
                {
                    if(jogo.Tabuleiro.TVencedor == 1 || jogo.Tabuleiro.TVencedor == 2 || jogo.Tabuleiro.TVencedor == -1)
                    {
                        var nomeVencedor = _context.JogadorPessoas
                                    .Include(j => j.Usuario)
                                    .Where(j => j.Id == jogo.Tabuleiro.TVencedor).FirstOrDefault();

                        throw new ArgumentException($"O jogo ja acabou. E o vencedor foi :" + nomeVencedor.Nome);
                    }
                    jogo.Tabuleiro.Jogar(jogadorId.GetValueOrDefault(), Pos);
                    _context.Update(jogo.Tabuleiro);
                    _context.SaveChanges();
                    return Ok(jogo.Tabuleiro);
                }
                else
                {
                    return Forbid();
                }
            }
            else
            {
                if (jogadorId == jogo.Tabuleiro.Turno)
                {
                    jogo.Tabuleiro.Jogar(jogadorId.GetValueOrDefault(), Pos);
                    _context.Update(jogo.Tabuleiro);
                    _context.SaveChanges();
                    return Ok(jogo.Tabuleiro);
                }
                else
                {
                    return Forbid();
                }
            }
        }
    }
}