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

bt.run("dotnet build -c Release");

if (bt.options.official)
{
    // Tag and commit
    bt.git_tag();

    // Push nuget packages
    bt.run(`dotnet nuget push`,
           `./Build/Release/*.${bt.options.version.build}.nupkg`,
           `--source "Topten GitHub"`);
}

