﻿using System;
using Microsoft.Extensions.Configuration;

namespace ETicaretAPI.Persistence
{
	static class Configuration
	{
		public static string ConnectionString
		{
			get
			{
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath
                    (Path.Combine(Directory.GetCurrentDirectory(),
                    "../../Presentation/ETicaretAPI.API"));
                configurationManager.AddJsonFile("appsettings.json");

				return configurationManager.GetConnectionString("PostgreSQL");
            }
		}
	}
}

