using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;


namespace Toggl.Tests
{
	public class TogglApiTestWithDefaultProject : BaseTogglApiTest
	{
		public int DefaultProjectId;

		[SetUp]
		public override void Init()
		{
			base.Init();

			var project = ProjectService.Add(new Project
			{
				IsBillable = true,
				WorkspaceId = DefaultWorkspaceId,
				Name = "New Project" + DateTime.UtcNow,
				IsAutoEstimates = false
			});

			DefaultProjectId = project.Id.Value;
		}
	}
}
