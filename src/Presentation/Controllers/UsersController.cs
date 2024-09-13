using Application.Models.ApplicationUserModel.Queries;
using Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            return await _mediator.Send(new GetApplicationUsersQuery());
        }
    }
}
