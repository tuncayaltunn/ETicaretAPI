﻿using System;
using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest,
                                                                   RefreshTokenLoginCommandResponse>
    {
        private readonly IAuthService _authService;

        public RefreshTokenLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
        {
            Token token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
            return new() { Token = token };
        }
    }
}

