using Nuke.Common;

class Build : NukeBuild
{
	public static int Main() => Execute<Build>(x => x.Release);

	Target Release => _ => _
		.Executes(() =>
		{

		});
}
