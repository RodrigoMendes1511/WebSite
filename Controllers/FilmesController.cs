using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using WebSite.Data;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class FilmesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilmesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Filmes
        public async Task<IActionResult> Index()
        {
            var client = new RestClient("https://localhost:5001/");
            var request = new RestRequest("Filme/", Method.Get);
            var Result = client.ExecuteAsync<List<Filmes>>(request).Result.Data;

            return View(Result);
        }

        // GET: Filmes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var client = new RestClient("https://localhost:5001/");
            var request = new RestRequest("Filme/" + id, Method.Get);
            var Result = client.ExecuteAsync<Filmes>(request).Result.Data;

            return View(Result);
        }

        // GET: Filmes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filmes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Diretor,Genero,Duracao")] Filmes filmes)
        {
            var client = new RestClient("https://localhost:5001/");
            var request = new RestRequest("Filme/", Method.Post);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new Filmes
            {
                Titulo = filmes.Titulo,
                Diretor = filmes.Diretor,
                Duracao = filmes.Duracao,
                Genero = filmes.Genero,
                Id = filmes.Id

            }) ;
            await client.ExecuteAsync(request);
            return View(filmes);
        }

        // GET: Filmes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var client = new RestClient("https://localhost:5001/");
            var request = new RestRequest("Filme/" + id, Method.Get);
            var Result = client.ExecuteAsync<Filmes>(request).Result.Data;
            return View(Result);
        }

        // POST: Filmes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Diretor,Genero,Duracao")] Filmes filmes)
        {
            var client = new RestClient("https://localhost:5001/");
            var request = new RestRequest("Filme/" + id, Method.Put);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new Filmes
            {
                Titulo = filmes.Titulo,
                Diretor = filmes.Diretor,
                Duracao = filmes.Duracao,
                Genero = filmes.Genero
                

            });
            await client.ExecuteAsync(request);
            return View(filmes);
           
        }

        // GET: Filmes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var client = new RestClient("https://localhost:5001/");
            var request = new RestRequest("Filme/" + id, Method.Get);
            var Result = client.ExecuteAsync<Filmes>(request).Result.Data;
            return View(Result);
        }

        // POST: Filmes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
       
            var client = new RestClient("https://localhost:5001/");
            var request = new RestRequest($"Filme/{id}", Method.Delete);
            var response = await client.ExecuteAsync(request);
            return RedirectToAction(nameof(Index));
        }

        private bool FilmesExists(int id)
        {
            return _context.Filmes.Any(e => e.Id == id);
        }
    }
}
