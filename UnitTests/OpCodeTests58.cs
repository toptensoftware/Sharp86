using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTests58 : CPUUnitTests
    {
        [Fact]
        public void Pop_ax()
        {
            sp = 0x8006;
            WriteWord(ss, sp, 4000);
            emit("pop ax");
            run();
            Assert.Equal(4000, ax);
            Assert.Equal(0x8008, sp);
        }

        [Fact]
        public void Pop_cx()
        {
            sp = 0x8006;
            WriteWord(ss, sp, 4000);
            emit("pop cx");
            run();
            Assert.Equal(4000, cx);
            Assert.Equal(0x8008, sp);
        }

        [Fact]
        public void Pop_dx()
        {
            sp = 0x8006;
            WriteWord(ss, sp, 4000);
            emit("pop dx");
            run();
            Assert.Equal(4000, dx);
            Assert.Equal(0x8008, sp);
        }

        [Fact]
        public void Pop_bx()
        {
            sp = 0x8006;
            WriteWord(ss, sp, 4000);
            emit("pop bx");
            run();
            Assert.Equal(4000, bx);
            Assert.Equal(0x8008, sp);
        }

        [Fact]
        public void Pop_sp()
        {
            sp = 0x8006;
            WriteWord(ss, sp, 4000);
            emit("pop sp");
            run();
            Assert.Equal(4000, sp);
        }

        [Fact]
        public void Pop_bp()
        {
            sp = 0x8006;
            WriteWord(ss, sp, 4000);
            emit("pop bp");
            run();
            Assert.Equal(4000, bp);
            Assert.Equal(0x8008, sp);
        }

        [Fact]
        public void Pop_si()
        {
            sp = 0x8006;
            WriteWord(ss, sp, 4000);
            emit("pop si");
            run();
            Assert.Equal(4000, si);
            Assert.Equal(0x8008, sp);
        }

        [Fact]
        public void Pop_di()
        {
            sp = 0x8006;
            WriteWord(ss, sp, 4000);
            emit("pop di");
            run();
            Assert.Equal(4000, di);
            Assert.Equal(0x8008, sp);
        }

    }
}
