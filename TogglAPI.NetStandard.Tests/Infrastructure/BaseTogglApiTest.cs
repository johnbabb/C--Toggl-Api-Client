using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using global::Toggl.Services;
using NUnit.Framework;

namespace Toggl.Tests
{
	using global::Toggl.QueryObjects;

	public class BaseTogglApiTest
	{
		public WorkspaceService WorkspaceService;
		public ClientService ClientService;
		public TaskService TaskService;
		public ProjectService ProjectService;
		public TagService TagService;
		public UserService UserService;
		public TimeEntryService TimeEntryService;
		public ReportService ReportService;
        public DashboardService DashboardService;

		public int DefaultWorkspaceId;

		[SetUp]
		public virtual void Init()
		{
			WorkspaceService = new WorkspaceService(Constants.ApiToken);
			var workspaces = WorkspaceService.List();

			ClientService = new ClientService(Constants.ApiToken);
			TaskService = new TaskService(Constants.ApiToken);
			TagService = new TagService(Constants.ApiToken);
			ProjectService = new ProjectService(Constants.ApiToken);
			UserService = new UserService(Constants.ApiToken);
			TimeEntryService = new TimeEntryService(Constants.ApiToken);
			ReportService = new ReportService(Constants.ApiToken);
            DashboardService = new DashboardService(Constants.ApiToken);

			foreach (var workspace in workspaces)
			{
				var projects = WorkspaceService.Projects(workspace.Id.Value);
				var tasks = WorkspaceService.Tasks(workspace.Id.Value);
				var tags = WorkspaceService.Tags(workspace.Id.Value); // TODO
				var users = WorkspaceService.Users(workspace.Id.Value); // TODO
				var clients = WorkspaceService.Clients(workspace.Id.Value);
				var rte = new TimeEntryParams { StartDate = DateTime.Now.AddYears(-1)};
				var timeEntries = TimeEntryService.List(rte);

				Assert.IsTrue(TimeEntryService.DeleteIfAny(timeEntries.Select(te => te.Id.Value).ToArray()));
				Assert.IsTrue(ProjectService.DeleteIfAny(projects.Select(p => p.Id.Value).ToArray()));				
				Assert.IsTrue(TaskService.DeleteIfAny(tasks.Select(t => t.Id.Value).ToArray()));
				Assert.IsTrue(ClientService.DeleteIfAny(clients.Select(c => c.Id.Value).ToArray()));

				Assert.IsFalse(WorkspaceService.Projects(workspace.Id.Value).Any());
				Assert.IsFalse(WorkspaceService.Tasks(workspace.Id.Value).Any());
				Assert.IsFalse(WorkspaceService.Clients(workspace.Id.Value).Any());
				Assert.IsFalse(TimeEntryService.List(rte).Any());
			}

			DefaultWorkspaceId = workspaces.First().Id.Value;
		}
	}
}
