﻿using ECE.WebApp.MVC.Extensions;
using System.Text.Json;
using System.Text;

namespace ECE.WebApp.MVC.Services
{
	public abstract class Service
	{
		protected StringContent SerializeToStringContent(object data)
		{
			return new StringContent(
				JsonSerializer.Serialize(data),
				Encoding.UTF8,
				"application/json");
		}

		protected async Task<T> DeserializeObjectResponse<T>(HttpResponseMessage responseMessage)
		{
			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			};

			return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
		}

		protected bool HandleResponseErrors(HttpResponseMessage response)
		{
			switch ((int)response.StatusCode)
			{
				case 401:
				case 403:
				case 404:
				case 500:
					throw new CustomHttpResponseException(response.StatusCode);

				case 400:
					return false;
				default:
					response.EnsureSuccessStatusCode();
					return true;
			}
		}
	}
}
