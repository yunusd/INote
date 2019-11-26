using INote.API.Dtos;
using INote.API.Extensions;
using INote.API.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace INote.API.Controllers
{
    [Authorize]
    public class NotesController : ApiController
    {
        ApplicationDbContext ctx = new ApplicationDbContext();

        public IHttpActionResult GetNotes()
        {
            var userId = User.Identity.GetUserId();
            var user = ctx.Users.Find(userId);

            return Ok(user.Notes.Select(x => new GetNoteDto { Id = x.Id, Title = x.Title, Content = x.Content, CreatedAt = x.CreatedAt, ModifiedAt = x.ModifiedAt }).ToList());
        }

        public IHttpActionResult GetNote(int id)
        {
            var userId = User.Identity.GetUserId();
            var user = ctx.Users.Find(userId);

            return Ok(user.Notes.Where(x => x.Id == id).Select(x => new GetNoteDto { Id = x.Id, Title = x.Title, Content = x.Content, CreatedAt = x.CreatedAt, ModifiedAt = x.ModifiedAt }).FirstOrDefault());
        }

        public IHttpActionResult PostNote(PostNoteDto dto)
        {
            if (ModelState.IsValid)
            {
                var note = new Note
                {
                    Title = dto.Title,
                    Content = dto.Content,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    AuthorId = User.Identity.GetUserId()
                };
                ctx.Notes.Add(note);
                ctx.SaveChanges();
                return Ok(note.ToGetNoteDto());
            }
            return BadRequest(ModelState);
        }

        // PUT: api/notes/putnote/1
        public IHttpActionResult PutNote(int id, PutNoteDto dto)
        {
            if(id != dto.Id)
            {
                return BadRequest();
            }


            var note = ctx.Notes.Find(id);

            if(note == null)
            {
                return NotFound();
            }

            if(note.AuthorId != User.Identity.GetUserId())
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                note.Title = dto.Title;
                note.Content = dto.Content;
                note.ModifiedAt = DateTime.Now;
                ctx.SaveChanges();

                return Ok(note.ToGetNoteDto());
            };

            return BadRequest(ModelState);
        }

        public IHttpActionResult DeleteNote(int id)
        {
            var note = ctx.Notes.Find(id);

            if (note == null)
            {
                return NotFound();
            }

            if (note.AuthorId != User.Identity.GetUserId())
            {
                return Unauthorized();
            }

            ctx.Notes.Remove(note);
            ctx.SaveChanges();

            return Ok(note.ToGetNoteDto());
        }
    }
}
