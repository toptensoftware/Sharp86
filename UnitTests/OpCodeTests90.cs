using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTests90 : CPUUnitTests
    {
        [Fact]
        public void Xchg_cx_ax()
        {
            ax = 1000;
            cx = 2000;
            emit("xchg ax,cx");
            run();
            Assert.Equal(2000, ax);
            Assert.Equal(1000, cx);
        }

        [Fact]
        public void Xchg_dx_ax()
        {
            ax = 1000;
            dx = 2000;
            emit("xchg dx,ax");
            run();
            Assert.Equal(2000, ax);
            Assert.Equal(1000, dx);
        }

        [Fact]
        public void Xchg_bx_ax()
        {
            ax = 1000;
            bx = 2000;
            emit("xchg ax,bx");
            run();
            Assert.Equal(2000, ax);
            Assert.Equal(1000, bx);
        }

        [Fact]
        public void Xchg_sp_ax()
        {
            ax = 1000;
            sp = 2000;
            emit("xchg ax,sp");
            run();
            Assert.Equal(2000, ax);
            Assert.Equal(1000, sp);
        }

        [Fact]
        public void Xchg_bp_ax()
        {
            ax = 1000;
            sp = 2000;
            emit("xchg ax,sp");
            run();
            Assert.Equal(2000, ax);
            Assert.Equal(1000, sp);
        }

        [Fact]
        public void Xchg_si_ax()
        {
            ax = 1000;
            si = 2000;
            emit("xchg ax,si");
            run();
            Assert.Equal(2000, ax);
            Assert.Equal(1000, si);
        }

        [Fact]
        public void Xchg_di_ax()
        {
            ax = 1000;
            di = 2000;
            emit("xchg ax,di");
            run();
            Assert.Equal(2000, ax);
            Assert.Equal(1000, di);
        }

    }
}
