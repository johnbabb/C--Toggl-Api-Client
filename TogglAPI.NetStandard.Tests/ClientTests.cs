using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
//using Toggl.Services;

namespace Toggl.Tests
{
    [TestFixture]
    public class ClientTests : BaseTogglApiTest
    {
	    [Test]
        public void NoClientsByDefault()
        {
			Assert.IsFalse(ClientService.List().Any());
        }


		[Test]
		public void NoClientsByDefaultRestSharp()
		{
			var client = new TogglApiViaRestSharp("6eae86fe55a39666057f045af4e3ca83", "api_token");

			var clientsList = client.GetClientsVisibleToUser();
			Assert.IsFalse(clientsList.Any());
		}

		[Test]
		public void GetClientsRestSharp()
		{
			var client = new TogglApiViaRestSharp("6eae86fe55a39666057f045af4e3ca83", "api_token");

			var clientsList = client.GetClientsVisibleToUser();
			Assert.IsFalse(clientsList.Any());

			var workspaceId = client.GetWorkspaces().Single().id;

			var clientToAdd = new ClientRestSharp()
			{
				name = "Test Client",
				wid = workspaceId.Value
			};

			client.CreateClient(clientToAdd);

			clientsList = client.GetClientsVisibleToUser();
			Assert.AreEqual(1, clientsList.Count);

		}

		[Test]
		public void AddClientRestSHarp()
		{
			var client = new TogglApiViaRestSharp("6eae86fe55a39666057f045af4e3ca83", "api_token");

			var workspaceId = client.GetWorkspaces().Single().id;

			var clientToAdd = new ClientRestSharp()
						{
							name = "Test Client",
							wid = workspaceId.Value
						};

			var addedClient = client.CreateClient(clientToAdd);
			
			Assert.IsNotNull(addedClient);
			Assert.AreEqual("Test Client", addedClient.name);
			Assert.AreEqual(workspaceId, addedClient.wid);
			Assert.IsTrue(addedClient.id.HasValue);
		}

		[Test]
		public void GetClientDetailsRestSharp()
		{
			var client = new TogglApiViaRestSharp("6eae86fe55a39666057f045af4e3ca83", "api_token");

			var workspaceId = client.GetWorkspaces().Single().id;

			var clientToAdd = new ClientRestSharp()
			{
				name = "Test Client",
				wid = workspaceId.Value
			};

			var addedClient = client.CreateClient(clientToAdd);

			var loadedClient = client.GetClientDetails(addedClient.id.Value);

			Assert.AreEqual(addedClient.name, loadedClient.name);
			Assert.AreEqual(addedClient.cur, loadedClient.cur);
			Assert.AreEqual(addedClient.hrate, loadedClient.hrate);
			Assert.AreEqual(addedClient.notes, loadedClient.notes);
			Assert.AreEqual(addedClient.wid, loadedClient.wid);
		}

		[Test]
		public void UpdateClientRestSharp()
		{
			var client = new TogglApiViaRestSharp("6eae86fe55a39666057f045af4e3ca83", "api_token");

			var workspaceId = client.GetWorkspaces().Single().id;

			var clientToAdd = new ClientRestSharp()
			{
				name = "Test Client",
				wid = workspaceId.Value
			};

			var addedClient = client.CreateClient(clientToAdd);

			addedClient.notes = "Edited client";
			
			var editedClient = client.UpdateClient(addedClient);

			Assert.AreEqual(addedClient.name, editedClient.name);
			Assert.AreEqual(addedClient.cur, editedClient.cur);
			Assert.AreEqual(addedClient.hrate, editedClient.hrate);
			Assert.AreEqual("Edited client", editedClient.notes);
			Assert.AreEqual(addedClient.wid, editedClient.wid);
		}

		[Test]
		public void DeleteClientRestSharp()
		{
			var client = new TogglApiViaRestSharp("6eae86fe55a39666057f045af4e3ca83", "api_token");

			var workspaceId = client.GetWorkspaces().Single().id;

			var clientToAdd = new ClientRestSharp()
			{
				name = "Test Client",
				wid = workspaceId.Value
			};

			var addedClient = client.CreateClient(clientToAdd);

			Assert.AreEqual(1, client.GetClientsVisibleToUser().Count);

			client.DeleteClient(addedClient.id.Value);

			Assert.AreEqual(0, client.GetClientsVisibleToUser().Count);			
		}
		
	    [Test]
	    public void Add()
	    {
		    var addedClient = ClientService.Add(new Client()
							{
								Name = "Client #1",
								WorkspaceId = DefaultWorkspaceId
							});

			Assert.IsNotNull(addedClient);
			Assert.AreEqual(1, ClientService.List().Count());
	    }
		
		[Test]
        public void Get()
        {
			var addedClient = ClientService.Add(new Client()
			{
				Name = "Client #1",
				WorkspaceId = DefaultWorkspaceId
			});


			var loadedClient = ClientService.Get(addedClient.Id.Value);

            Assert.IsNotNull(loadedClient);
			Assert.AreEqual(addedClient.Id, loadedClient.Id);
			Assert.AreEqual(addedClient.Name, loadedClient.Name);
			Assert.AreEqual(addedClient.WorkspaceId, loadedClient.WorkspaceId);			
        }

		[Test]
        public void Delete()
        {
			var addedClient = ClientService.Add(new Client()
			{
				Name = "Client #1",
				WorkspaceId = DefaultWorkspaceId
			});

			Assert.IsNotNull(addedClient);
			Assert.AreEqual(1, ClientService.List().Count);
			Assert.IsTrue(ClientService.Delete(addedClient.Id.Value));
			Assert.IsFalse(ClientService.List().Any());
        }

		[Test]
		public void BulkDelete()
		{
			var ids = new List<int>();
			for (int i = 0; i < 3; i++)
			{
				var addedClient = ClientService.Add(new Client()
				{
					Name = "Client #" + i,
					WorkspaceId = DefaultWorkspaceId
				});
				Assert.IsNotNull(addedClient);
				ids.Add(addedClient.Id.Value);
			}

			Assert.AreEqual(3, ClientService.List().Count);
			Assert.IsTrue(ClientService.Delete(ids.ToArray()));
			Assert.IsFalse(ClientService.List().Any());
		}

		[Test]
		public void Edit()
		{
			var addedClient = ClientService.Add(new Client()
			{
				Name = "Client #1",
				WorkspaceId = DefaultWorkspaceId
			});

			var loadedClient = ClientService.Get(addedClient.Id.Value);
			loadedClient.Name = "TestEdit";
			var editedClient = ClientService.Edit(loadedClient);

			Assert.IsNotNull(editedClient);
			Assert.AreEqual(addedClient.Id, editedClient.Id);
			Assert.AreEqual(loadedClient.Name, editedClient.Name);
			Assert.AreEqual(addedClient.WorkspaceId, editedClient.WorkspaceId);	
		}

	    [Test]
	    public void GetByName()
	    {
			var addedClient = ClientService.Add(new Client()
			{
				Name = "Client #1",
				WorkspaceId = DefaultWorkspaceId
			});

			var loadedClient = ClientService.GetByName("Client #1");

			Assert.IsNotNull(loadedClient);
			Assert.AreEqual(addedClient.Id, loadedClient.Id);
			Assert.AreEqual(addedClient.Name, loadedClient.Name);
			Assert.AreEqual(addedClient.WorkspaceId, loadedClient.WorkspaceId);		
	    }


    }
}
