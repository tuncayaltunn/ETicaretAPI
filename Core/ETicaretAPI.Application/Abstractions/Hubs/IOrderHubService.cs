﻿using System;
namespace ETicaretAPI.Application.Abstractions.Hubs
{
	public interface IOrderHubService
	{
        Task OrderAddedMessageAsync(string message);
    }
}

