using System;

using RestSharp;

namespace Toggl
{
	public class TogglApiViaRestSharp
	{
		private readonly string username;
		private readonly string password;

		/// <summary>
		/// Creates an TogglApi .Net client
		/// </summary>
		/// <param name="username">Provide username, or use "api" to authenticate by api token</param>
		/// <param name="password">Password</param>
		public TogglApiViaRestSharp(string username, string password)
		{
			if (username == null)
				throw new ArgumentNullException("username");
			
			if (password == null)
				throw new ArgumentNullException("password");
			
			this.username = username;
			this.password = password;			
		}
		
		public T Execute<T>(RestRequest request) where T : new()
		{
			var client = new RestClient();
			client.BaseUrl = new Uri("https://www.toggl.com/api/v8");
			client.Authenticator = new HttpBasicAuthenticator(username, password);
			
			var response = client.Execute<T>(request);

			if (response.ErrorException != null)
			{
				const string message = "Error retrieving response.  Check inner details for more info.";
				var togglException = new ApplicationException(message, response.ErrorException);
				throw togglException;
			}
			return response.Data;
		}

		public ClientRestSharp GetClientInfo()
		{
			var request = new RestRequest();
			request.Resource = "me";
			request.RootElement = "data";

			// request.AddParameter("CallSid", callSid, ParameterType.UrlSegment);

			return Execute<ClientRestSharp>(request);
		}
	}
}