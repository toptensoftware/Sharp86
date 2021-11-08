using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTests50 : CPUUnitTests
    {
        [Fact]
        public void Push_ax()
        {
            ax = 4000;
            sp = 0x8008;
            emit("push ax");
            run();
            Assert.Equal(4000, ReadWord(ss, sp));
            Assert.Equal(0x8006, sp);
        }

        [Fact]
        public void Push_cx()
        {
            cx = 4000;
            sp = 0x8008;
            emit("push cx");
            run();
            Assert.Equal(4000, ReadWord(ss, sp));
            Assert.Equal(0x8006, sp);
        }

        [Fact]
        public void Push_dx()
        {
            dx = 4000;
            sp = 0x8008;
            emit("push dx");
            run();
            Assert.Equal(4000, ReadWord(ss, sp));
            Assert.Equal(0x8006, sp);
        }

        [Fact]
        public void Push_bx()
        {
            bx = 4000;
            sp = 0x8008;
            emit("push bx");
            run();
            Assert.Equal(4000, ReadWord(ss, sp));
            Assert.Equal(0x8006, sp);
        }

        [Fact]
        public void Push_sp()
        {
            sp = 4000;
            emit("push sp");
            run();
            Assert.Equal(4000, ReadWord(ss, sp));
            Assert.Equal(3998, sp);
        }

        [Fact]
        public void Push_bp()
        {
            bp = 4000;
            sp = 0x8008;
            emit("push bp");
            run();
            Assert.Equal(4000, ReadWord(ss, sp));
            Assert.Equal(0x8006, sp);
        }

        [Fact]
        public void Push_si()
        {
            si = 4000;
            sp = 0x8008;
            emit("push si");
            run();
            Assert.Equal(4000, ReadWord(ss, sp));
            Assert.Equal(0x8006, sp);
        }

        [Fact]
        public void Push_di()
        {
            di = 4000;
            sp = 0x8008;
            emit("push di");
            run();
            Assert.Equal(4000, ReadWord(ss, sp));
            Assert.Equal(0x8006, sp);
        }

    }
}
