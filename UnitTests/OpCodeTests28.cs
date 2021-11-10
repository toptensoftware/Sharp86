using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTests28 : CPUUnitTests
    {
        [Fact]
        public void Sub_Eb_Gb()
        {
            MMU.WriteByte(0, 100, 40);
            FlagC = true;
            al = 10;
            emit("sub byte [100], al");
            run();
            Assert.Equal(30, MMU.ReadByte(0, 100));
        }

        [Fact]
        public void Sub_Ev_Gv()
        {
            WriteWord(0, 100, 4000);
            FlagC = true;
            ax = 1000;
            emit("sub word [100], ax");
            run();
            Assert.Equal(3000, ReadWord(0, 100));
        }

        [Fact]
        public void Sub_Gb_Eb()
        {
            MMU.WriteByte(0, 100, 10);
            al = 40;
            FlagC = true;
            emit("sub al, byte [100]");
            run();
            Assert.Equal(30, al);
        }

        [Fact]
        public void Sub_Gv_Ev()
        {
            WriteWord(0, 100, 1000);
            ax = 4000;
            FlagC = true;
            emit("sub ax, word [100]");
            run();
            Assert.Equal(3000, ax);
        }

        [Fact]
        public void Sub_AL_Ib()
        {
            al = 40;
            FlagC = true;
            emit("sub al, 10");
            run();
            Assert.Equal(30, al);
        }

        [Fact]
        public void Sub_AX_Iv()
        {
            ax = 4000;
            FlagC = true;
            emit("sub ax, 1000");
            run();
            Assert.Equal(3000, ax);
        }

        [Fact]
        public void DAS()
        {
            al = 0xCC;
            emit("das");
            run();
            Assert.Equal(0x66, al);
        }


    }
}
