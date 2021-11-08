using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public sealed class IgnoreXunitAnalyzersRule1013Attribute : Attribute { }

    [IgnoreXunitAnalyzersRule1013]
    public class NotAFact : Attribute { }
}
