using Application.Models.ApplicationUserModel.Queries;
using Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ApplicationUserModel.QueryHandlers
{
    public class GetApplicationUsersQueryHandler : IRequestHandler<GetApplicationUsersQuery, IEnumerable<ApplicationUser>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetApplicationUsersQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<ApplicationUser>> Handle(GetApplicationUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.ToList();
            return users;  // Returning ApplicationUser directly
        }
    }

}
