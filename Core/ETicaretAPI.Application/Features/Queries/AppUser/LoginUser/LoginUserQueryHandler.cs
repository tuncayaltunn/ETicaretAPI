using System;
using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI.Application.Features.Queries.AppUser.LoginUser
{
	public class LoginUserQueryHandler : IRequestHandler<LoginUserQueryRequest, LoginUserQueryResponse>
	{
        private readonly UserManager<ETicaretAPI.Domain.Entities.Identity.AppUser> _userManager;
        private readonly SignInManager<ETicaretAPI.Domain.Entities.Identity.AppUser> _singInManager;
        private readonly ITokenHandler _tokenHandler;

        public LoginUserQueryHandler(UserManager<Domain.Entities.Identity.AppUser> userManager,
            SignInManager<Domain.Entities.Identity.AppUser> singInManager,
            ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _singInManager = singInManager;
            _tokenHandler = tokenHandler;
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
                Token token = _tokenHandler.CreateAccessToken(5);
                return new LoginUserSuccessCommandResponse { Token = token };
            }
             
            //return new LoginUserErrorCommandResponse { Message = "Kullanıcı adı veya şifre hatalı." };
            throw new AuthenticationErrorException();
        }
    }
}

