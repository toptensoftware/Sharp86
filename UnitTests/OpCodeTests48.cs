using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTests48 : CPUUnitTests
    {
        [Fact]
        public void Dec_ax()
        {
            ax = 4000;
            emit("dec ax");
            run();
            Assert.Equal(3999, ax);
        }

        [Fact]
        public void Dec_cx()
        {
            cx = 4000;
            emit("dec cx");
            run();
            Assert.Equal(3999, cx);
        }

        [Fact]
        public void Dec_dx()
        {
            dx = 4000;
            emit("dec dx");
            run();
            Assert.Equal(3999, dx);
        }

        [Fact]
        public void Dec_bx()
        {
            bx = 4000;
            emit("dec bx");
            run();
            Assert.Equal(3999, bx);
        }

        [Fact]
        public void Dec_sp()
        {
            sp = 4000;
            emit("dec sp");
            run();
            Assert.Equal(3999, sp);
        }

        [Fact]
        public void Dec_bp()
        {
            bp = 4000;
            emit("dec bp");
            run();
            Assert.Equal(3999, bp);
        }

        [Fact]
        public void Dec_si()
        {
            si = 4000;
            emit("dec si");
            run();
            Assert.Equal(3999, si);
        }

        [Fact]
        public void Dec_di()
        {
            di = 4000;
            emit("dec di");
            run();
            Assert.Equal(3999, di);
        }

    }
}
