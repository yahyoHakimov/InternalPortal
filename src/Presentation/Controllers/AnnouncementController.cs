using Application.Models.Announcements.Commands;
using Application.Models.Announcements.Queries.AnnouncementQueries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AnnouncementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/announcement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Announcement>>> GetAnnouncements()
        {
            var query = new GetAllAnnouncementsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/announcement/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Announcement>> GetAnnouncement(int id)
        {
            var query = new GetAnnouncementByIdQuery { AnnouncementId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST: api/announcement
        [HttpPost]
        public async Task<ActionResult<Announcement>> CreateAnnouncement([FromBody] CreateAnnouncementCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAnnouncements), new { id = result.AnnouncementId }, result);
        }

        // PUT: api/announcement/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnnouncement(int id, [FromBody] UpdateAnnouncementCommand command)
        {
            if (id != command.AnnouncementId)
            {
                return BadRequest("Announcement ID mismatch");
            }

            var result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/announcement/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            var command = new DeleteAnnouncementCommand { AnnouncementId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
