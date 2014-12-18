using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using global::Toggl.Services;
using NUnit.Framework;

namespace Toggl.Tests
{
	[TestFixture]
	public class BaseTogglApiTest
	{
		public static WorkspaceService WorkspaceService;
		public static ClientService ClientService;
		public static TaskService TaskService;
		public static ProjectService ProjectService;
		public static TagService TagService;
		public static UserService UserService;
		public static TimeEntryService TimeEntryService;

		public int DefaultWorkspaceId;

		[SetUp]
		public virtual void Init()
		{
			WorkspaceService = new WorkspaceService();
			var workspaces = WorkspaceService.List();
			
			ClientService = new ClientService();
			TaskService = new TaskService();
			TagService = new TagService();
			ProjectService = new ProjectService();
			UserService = new UserService();
			TimeEntryService = new TimeEntryService();

			foreach (var workspace in workspaces)
			{
				var projects = WorkspaceService.Projects(workspace.Id.Value);
				var tasks = WorkspaceService.Tasks(workspace.Id.Value);
				var tags = WorkspaceService.Tags(workspace.Id.Value); // TODO
				var users = WorkspaceService.Users(workspace.Id.Value); // TODO
				var clients = WorkspaceService.Clients(workspace.Id.Value);
				var timeEntries = TimeEntryService.List();

				Assert.IsTrue(TimeEntryService.DeleteIfAny(timeEntries.Select(te => te.Id.Value).ToArray()));
				Assert.IsTrue(ProjectService.DeleteIfAny(projects.Select(p => p.Id.Value).ToArray()));				
				Assert.IsTrue(TaskService.DeleteIfAny(tasks.Select(t => t.Id.Value).ToArray()));
				Assert.IsTrue(ClientService.DeleteIfAny(clients.Select(c => c.Id.Value).ToArray()));

				Assert.IsFalse(WorkspaceService.Projects(workspace.Id.Value).Any());
				Assert.IsFalse(WorkspaceService.Tasks(workspace.Id.Value).Any());
				Assert.IsFalse(WorkspaceService.Clients(workspace.Id.Value).Any());
				Assert.IsFalse(TimeEntryService.List().Any());
			}

			DefaultWorkspaceId = workspaces.First().Id.Value;
		}
	}
}
