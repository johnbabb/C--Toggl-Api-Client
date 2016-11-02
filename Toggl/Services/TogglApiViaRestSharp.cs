using System;
using System.Collections.Generic;
using System.Net;

using Newtonsoft.Json;

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

			if (response.Content == "null") // Toggl returns "null", when no elements found
				return default(T);

			if (response.ErrorException != null)
			{
				const string message = "Error retrieving response.  Check inner details for more info.";
				var togglException = new ApplicationException(message, response.ErrorException);
				throw togglException;
			}

			if (response.StatusCode != HttpStatusCode.OK)
				throw new ApplicationException(
					string.Format(
						"Response status code: {0}, Response content: {1}",
						response.StatusCode, 
						response.Content ?? "empty"));
	
			return response.Data;
		}

		public UserRestSharp GetUserInfo()
		{
			var request = new RestRequest();
			request.Resource = "me";
			request.RootElement = "data";

			return Execute<UserRestSharp>(request);
		}

		public List<ClientRestSharp> GetClientsVisibleToUser()
		{
			var request = new RestRequest();
			request.Resource = "clients";
			
			var result = Execute<List<ClientRestSharp>>(request);

			if (result == null)
				return new List<ClientRestSharp>();

			return result;
		}

		public ClientRestSharp CreateClient(ClientRestSharp clientToAdd)
		{
			var request = new RestRequest();
			request.Resource = "clients";
			request.Method = Method.POST;
			request.RootElement = "data";
			request.RequestFormat = DataFormat.Json;
			request.AddBody(new { client = clientToAdd });
			
			return Execute<ClientRestSharp>(request);			
		}

		public ClientRestSharp GetClientDetails(int id)
		{
			var request = new RestRequest();
			request.Resource = "clients/{client_id}";
			request.Method = Method.GET;
			request.RootElement = "data";
			request.RequestFormat = DataFormat.Json;
			request.AddParameter("client_id", id, ParameterType.UrlSegment);

			return Execute<ClientRestSharp>(request);
		}

		public List<WorkspaceRestSharp> GetWorkspaces()
		{
			var request = new RestRequest();
			request.Resource = "workspaces";

			var result = Execute<List<WorkspaceRestSharp>>(request);

			if (result == null)
				return new List<WorkspaceRestSharp>();

			return result;
		}

		public ClientRestSharp UpdateClient(ClientRestSharp updatedClient)
		{
			var request = new RestRequest();
			request.Resource = "clients/{client_id}";
			request.Method = Method.PUT;
			request.RootElement = "data";
			request.RequestFormat = DataFormat.Json;
			request.AddBody(new { client = updatedClient });
			request.AddParameter("client_id", updatedClient.id.Value, ParameterType.UrlSegment);

			return Execute<ClientRestSharp>(request);		
		}

		public void DeleteClient(int id)
		{
			var request = new RestRequest();
			request.Resource = "clients/{client_id}";
			request.Method = Method.DELETE;
			request.RootElement = "data";
			request.RequestFormat = DataFormat.Json;
			request.AddParameter("client_id", id, ParameterType.UrlSegment);

			Execute<object>(request);	
		}
	}
}