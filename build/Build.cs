using Nuke.Common;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.IO;
using System;
using System.IO;
using Nuke.Common.Tooling;
using System.Collections.Generic;
using static GitHubActionTasks;
using System.Threading.Tasks;
using System.Linq;

class Build : NukeBuild
{
	public static int Main() => Execute<Build>(x => x.Publish);

	private AbsolutePath ProjectPath => RootDirectory / "console";

	[Parameter]
	private AbsolutePath PublishFolder = RootDirectory / "publish";

	Target SetOutputs => _ => _
		.Executes(async () =>
		{
			await SetStepOutput(_ => _
				.AddOutput("FAV_NUMBER", "5")
				.AddOutput("FAV_COLOR", "blue"));
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

public class GitHubActionSetting
{
	public Dictionary<string, string> Outputs { get; }
		= new Dictionary<string, string>();
}

public static class GitHubActionTasks
{
	public static async Task SetStepOutput(
		Configure<GitHubActionSetting> configurator)
	{
		if (configurator is null)
		{
			throw new ArgumentNullException(nameof(configurator));
		}

		var settings = configurator(new GitHubActionSetting());
		await WriteOutputs(settings.Outputs);
	}

	private static async Task WriteOutputs(IDictionary<string, string> outputs)
	{
		if (outputs.Count == 0)
		{
			return;
		}

		var result = string.Join('\n', outputs.Select(pair => $"{pair.Key}={pair.Value}"));
		var githubOutputFilename = Environment.GetEnvironmentVariable("GITHUB_OUTPUT");
		using var file = File.OpenWrite(githubOutputFilename);
		using var filewriter = new StreamWriter(file);
		await filewriter.WriteLineAsync(result);
	}

	public static async Task SetStepOutput() => await SetStepOutput(_ => _);
}

public static class GitHubActionSettingExtensions
{
	public static GitHubActionSetting AddOutput(
		this GitHubActionSetting settings,
		string name,
		string value)
	{
		settings.Outputs[name] = value;
		return settings;
	}
}
