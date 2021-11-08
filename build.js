var path = require('path');
var bt = require('./buildtools/buildTools.js')

if (bt.options.official)
{
    // Check everything committed
    bt.git_check();

    // Clock version
    bt.clock_version();

    // Force clean
    bt.run("rm -rf ./Build");
}
else if (bt.options.clockver)
{
    // Clock version
    bt.clock_version();
}

var buildSwitches = "-c Release";
if (!bt.options.verbose)
    buildSwitches += " -v q --nologo";

function dotnet_build(proj)
{
    console.log(`\n---------- ${path.basename(proj)} ----------`);
    bt.run(`dotnet build ${proj} ${buildSwitches}`);
}

dotnet_build("./Topten.Sharp86/Topten.Sharp86.csproj");
dotnet_build("./Topten.Sharp86.Debugger/Topten.Sharp86.Debugger.csproj");
dotnet_build("./Topten.Sharp86.TextGuiDebugger/Topten.Sharp86.TextGuiDebugger.csproj");


if (bt.options.official)
{
    // Tag and commit
    bt.git_tag();

    // Push nuget packages
    bt.run(`dotnet nuget push`,
           `./Build/Release/*.${bt.options.version.build}.nupkg`,
           `--source "Topten GitHub"`);
}

