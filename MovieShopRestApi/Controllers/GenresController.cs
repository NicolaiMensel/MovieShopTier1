using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MovieShopDLL;
using MovieShopDLL.Context;
using MovieShopDLL.Entities;

namespace MovieShopRestApi.Controllers
{
    public class GenresController : ApiController
    {
        private IRepository<Genre, int> _genreRepository = new DLLFacade().GetGenreRepository();

        // GET: api/Genres
        public IQueryable<Genre> GetGenres()
        {
            return (IQueryable<Genre>)_genreRepository.ReadAll();
        }

        // GET: api/Genres/5
        [ResponseType(typeof(Genre))]
        public IHttpActionResult GetGenre(int id)
        {
            Genre genre = _genreRepository.Read(id);
            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre);
        }

        // PUT: api/Genres/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGenre(int id, Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != genre.Id)
            {
                return BadRequest();
            }

            try
            {
                _genreRepository.Update(genre);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Genres
        [ResponseType(typeof(Genre))]
        public IHttpActionResult PostGenre(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _genreRepository.Create(genre);

            return CreatedAtRoute("DefaultApi", new { id = genre.Id }, genre);
        }

        // DELETE: api/Genres/5
        [ResponseType(typeof(Genre))]
        public IHttpActionResult DeleteGenre(int id)
        {
            Genre genre = _genreRepository.Read(id);
            if (genre == null)
            {
                return NotFound();
            }

            _genreRepository.Delete(id);

            return Ok(genre);
        }

        private bool GenreExists(int id)
        {
            return _genreRepository.ReadAll().Count(e => e.Id == id) > 0;
        }
    }
}