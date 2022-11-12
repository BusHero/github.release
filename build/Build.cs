using Nuke.Common;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.IO;
using System;

class Build : NukeBuild
{
	public static int Main() => Execute<Build>(x => x.Publish);

	private AbsolutePath ProjectPath => RootDirectory / "console";

	[Parameter]
	private AbsolutePath PublishFolder = RootDirectory / "publish";

	Target SetOutputs => _ => _
		.Executes(() => Console.WriteLine("""
		"FAV_NUMBER=3" >> $GITHUB_OUTPUT
		"FAV_COLOR='blue'" >> $GITHUB_OUTPUT
		"""));

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
