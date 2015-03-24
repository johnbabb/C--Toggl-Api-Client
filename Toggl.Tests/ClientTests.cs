using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Toggl.Services;

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
			var client = new TogglApiViaRestSharp("53e8569674f124ac8226e786168bbd76", "api_token");

			var clientsList = client.GetClientsVisibleToUser();
			Assert.IsFalse(clientsList.Any());
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
