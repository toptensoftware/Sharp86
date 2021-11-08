using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTestsB8 : CPUUnitTests
    {
        [Fact]
        public void mov_ax_iv()
        {
            emit("mov ax,1234h");
            run();
            Assert.Equal(0x1234, ax);
        }

        [Fact]
        public void mov_cx_iv()
        {
            emit("mov cx,1234h");
            run();
            Assert.Equal(0x1234, cx);
        }

        [Fact]
        public void mov_dx_iv()
        {
            emit("mov dx,1234h");
            run();
            Assert.Equal(0x1234, dx);
        }

        [Fact]
        public void mov_bx_iv()
        {
            emit("mov bx,1234h");
            run();
            Assert.Equal(0x1234, bx);
        }

        [Fact]
        public void mov_sp_iv()
        {
            emit("mov sp,1234h");
            run();
            Assert.Equal(0x1234, sp);
        }

        [Fact]
        public void mov_bp_iv()
        {
            emit("mov bp,1234h");
            run();
            Assert.Equal(0x1234, bp);
        }

        [Fact]
        public void mov_si_iv()
        {
            emit("mov si,1234h");
            run();
            Assert.Equal(0x1234, si);
        }

        [Fact]
        public void mov_di_iv()
        {
            emit("mov di,1234h");
            run();
            Assert.Equal(0x1234, di);
        }

    }

}
