using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTests40 : CPUUnitTests
    {
        [Fact]
        public void Inc_ax()
        {
            ax = 4000;
            emit("inc ax");
            run();
            Assert.Equal(4001, ax);
        }

        [Fact]
        public void Inc_cx()
        {
            cx = 4000;
            emit("inc cx");
            run();
            Assert.Equal(4001, cx);
        }

        [Fact]
        public void Inc_dx()
        {
            dx = 4000;
            emit("inc dx");
            run();
            Assert.Equal(4001, dx);
        }

        [Fact]
        public void Inc_bx()
        {
            bx = 4000;
            emit("inc bx");
            run();
            Assert.Equal(4001, bx);
        }

        [Fact]
        public void Inc_sp()
        {
            sp = 4000;
            emit("inc sp");
            run();
            Assert.Equal(4001, sp);
        }

        [Fact]
        public void Inc_bp()
        {
            bp = 4000;
            emit("inc bp");
            run();
            Assert.Equal(4001, bp);
        }

        [Fact]
        public void Inc_si()
        {
            si = 4000;
            emit("inc si");
            run();
            Assert.Equal(4001, si);
        }

        [Fact]
        public void Inc_di()
        {
            di = 4000;
            emit("inc di");
            run();
            Assert.Equal(4001, di);
        }

    }
}
