using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTests18 : CPUUnitTests
    {
        [Fact]
        public void Sbb_Eb_Gb()
        {
            MMU.WriteByte(0, 100, 40);
            FlagC = true;
            al = 10;
            emit("sbb byte [100], al");
            run();
            Assert.Equal(29, MMU.ReadByte(0, 100));
        }

        [Fact]
        public void Sbb_Ev_Gv()
        {
            WriteWord(0, 100, 4000);
            FlagC = true;
            ax = 1000;
            emit("sbb word [100], ax");
            run();
            Assert.Equal(2999, ReadWord(0, 100));
        }

        [Fact]
        public void Sbb_Gb_Eb()
        {
            MMU.WriteByte(0, 100, 10);
            al = 40;
            FlagC = true;
            emit("sbb al, byte [100]");
            run();
            Assert.Equal(29, al);
        }

        [Fact]
        public void Sbb_Gv_Ev()
        {
            WriteWord(0, 100, 1000);
            ax = 4000;
            FlagC = true;
            emit("sbb ax, word [100]");
            run();
            Assert.Equal(2999, ax);
        }

        [Fact]
        public void Sbb_AL_Ib()
        {
            al = 40;
            FlagC = true;
            emit("sbb al, 10");
            run();
            Assert.Equal(29, al);
        }

        [Fact]
        public void Sbb_AX_Iv()
        {
            ax = 4000;
            FlagC = true;
            emit("sbb ax, 1000");
            run();
            Assert.Equal(2999, ax);
        }

        [Fact]
        public void PUSH_DS()
        {
            sp = 0x8008;
            ds = 0x1234;
            emit("push ds");
            run();
            Assert.Equal(0x1234, ReadWord(ss, sp));
            Assert.Equal(0x8006, sp);
        }

        [Fact]
        public void POP_DS()
        {
            sp = 0x8006;
            WriteWord(ss, sp, 0x1234);
            emit("pop ds");
            run();
            Assert.Equal(0x1234, ds);
            Assert.Equal(0x8008, sp);
        }

    }
}
