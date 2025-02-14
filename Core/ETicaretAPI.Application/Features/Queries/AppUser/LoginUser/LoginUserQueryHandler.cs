using System;
using ETicaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI.Application.Features.Queries.AppUser.LoginUser
{
	public class LoginUserQueryHandler : IRequestHandler<LoginUserQueryRequest, LoginUserQueryResponse>
	{
        private readonly UserManager<ETicaretAPI.Domain.Entities.Identity.AppUser> _userManager;
        private readonly SignInManager<ETicaretAPI.Domain.Entities.Identity.AppUser> _singInManager;

        public LoginUserQueryHandler(UserManager<Domain.Entities.Identity.AppUser> userManager,
            SignInManager<Domain.Entities.Identity.AppUser> singInManager)
        {
            _userManager = userManager;
            _singInManager = singInManager;
        }

        public async Task<LoginUserQueryResponse> Handle(LoginUserQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);

            if (user == null)
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (user == null)
                throw new NotFoundUserException();

            SignInResult result = await _singInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded)
            {
                // Yetkilendirme
            }

            return new();
        }
    }
}

