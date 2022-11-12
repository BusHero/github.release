using Nuke.Common;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.IO;

class Build : NukeBuild
{
	public static int Main() => Execute<Build>(x => x.Publish);

	private AbsolutePath ProjectPath => RootDirectory / "console";

	[Parameter]
	private AbsolutePath PublishFolder => RootDirectory / "publish";

	Target Publish => _ => _
		.Executes(() => DotNetPublish(_ => _
			.SetProject(ProjectPath)
			.SetPublishProfile(Configuration.Release)
			.SetOutput(PublishFolder)));


	Target Release => _ => _
		.Executes(() =>
		{

		});
}
