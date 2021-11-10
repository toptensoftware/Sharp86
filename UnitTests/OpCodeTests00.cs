using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTests00 : CPUUnitTests
    {
        [Fact]
        public void Add_Eb_Gb()
        {
            MMU.WriteByte(0, 100, 40);
            al = 20;
            emit("add byte [100], al");
            run();
            Assert.Equal(60, MMU.ReadByte(0, 100));
        }

        [Fact]
        public void Add_Ev_Gv()
        {
            WriteWord(0, 100, 4000);
            ax = 2000;
            emit("add word [100], ax");
            run();
            Assert.Equal(6000, this.ReadWord(0, 100));
        }

        [Fact]
        public void Add_Gb_Eb()
        {
            MMU.WriteByte(0, 100, 40);
            al = 20;
            emit("add al, byte [100]");
            run();
            Assert.Equal(60, al);
        }

        [Fact]
        public void Add_Gv_Ev()
        {
            WriteWord(0, 100, 4000);
            ax = 2000;
            emit("add ax, word [100]");
            run();
            Assert.Equal(6000, ax);
        }

        [Fact]
        public void Add_AL_Ib()
        {
            al = 40;
            emit("add al, 20");
            run();
            Assert.Equal(60, al);
        }

        [Fact]
        public void Add_AX_Iv()
        {
            ax = 4000;
            emit("add ax, 2000");
            run();
            Assert.Equal(6000, ax);
        }

        [Fact]
        public void PUSH_ES()
        {
            sp = 0x8008;
            es = 0x1234;
            emit("push es");
            run();
            Assert.Equal(0x1234, ReadWord(ss, sp));
            Assert.Equal(0x8006, sp);
        }

        [Fact]
        public void POP_ES()
        {
            sp = 0x8006;
            WriteWord(ss, sp, 0x1234);
            emit("pop es");
            run();
            Assert.Equal(0x1234, es);
            Assert.Equal(0x8008, sp);
        }

    }
}
