using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTests38 : CPUUnitTests
    {
        [Fact]
        public void Cmp_Eb_Gb()
        {
            WriteByte(0, 100, 40);
            FlagC = true;
            FlagZ = true;
            al = 10;
            emit("cmp byte [100], al");
            run();
            Assert.Equal(40, ReadByte(0, 100));
            Assert.False(FlagZ);
            Assert.False(FlagC);
        }

        [Fact]
        public void Cmp_Ev_Gv()
        {
            WriteWord(0, 100, 4000);
            FlagC = true;
            FlagZ = true;
            ax = 1000;
            emit("cmp word [100], ax");
            run();
            Assert.Equal(4000, ReadWord(0, 100));
            Assert.False(FlagZ);
            Assert.False(FlagC);
        }

        [Fact]
        public void Cmp_Gb_Eb()
        {
            WriteByte(0, 100, 10);
            al = 40;
            FlagC = true;
            FlagZ = true;
            emit("cmp al, byte [100]");
            run();
            Assert.Equal(40, al);
            Assert.False(FlagZ);
            Assert.False(FlagC);
        }

        [Fact]
        public void Cmp_Gv_Ev()
        {
            WriteWord(0, 100, 1000);
            ax = 4000;
            FlagC = true;
            FlagZ = true;
            emit("cmp ax, word [100]");
            run();
            Assert.Equal(4000, ax);
            Assert.False(FlagZ);
            Assert.False(FlagC);
        }

        [Fact]
        public void Cmp_AL_Ib()
        {
            al = 40;
            FlagC = true;
            FlagZ = true;
            emit("cmp al, 10");
            run();
            Assert.Equal(40, al);
            Assert.False(FlagZ);
            Assert.False(FlagC);
        }

        [Fact]
        public void Cmp_AX_Iv()
        {
            ax = 4000;
            FlagC = true;
            FlagZ = true;
            emit("cmp ax, 1000");
            run();
            Assert.Equal(4000, ax);
            Assert.False(FlagZ);
            Assert.False(FlagC);
        }

        [Fact]
        public void Aas_test()
        {
            al = 0xCC;
            emit("aas");
            run();
            Assert.Equal(0, al);
        }


    }
}
