using Nuke.Common;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.IO;
using System;
using System.IO;

class Build : NukeBuild
{
	public static int Main() => Execute<Build>(x => x.Publish);

	private AbsolutePath ProjectPath => RootDirectory / "console";

	[Parameter]
	private AbsolutePath PublishFolder = RootDirectory / "publish";

	Target SetOutputs => _ => _
		.Executes(async () =>
		{
			var filename = Environment.GetEnvironmentVariable("GITHUB_OUTPUT");
			using var file = File.OpenWrite(filename);
			using var filewriter = new StreamWriter(file);
			await filewriter.WriteLineAsync("FAV_NUMBER=5");
			await filewriter.WriteLineAsync("FAV_COLOR=blue");
		});

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
