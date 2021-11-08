using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTests30 : CPUUnitTests
    {
        [Fact]
        public void Xor_Eb_Gb()
        {
            WriteByte(0, 100, 0x60);
            al = 0x20;
            emit("xor byte [100], al");
            run();
            Assert.Equal(0x40, ReadByte(0, 100));
        }

        [Fact]
        public void Xor_Ev_Gv()
        {
            WriteWord(0, 100, 0x6000);
            ax = 0x2000;
            emit("xor word [100], ax");
            run();
            Assert.Equal(0x4000, ReadWord(0, 100));
        }

        [Fact]
        public void Xor_Gb_Eb()
        {
            WriteByte(0, 100, 0x60);
            al = 0x20;
            emit("xor al, byte [100]");
            run();
            Assert.Equal(0x40, al);
        }

        [Fact]
        public void Xor_Gv_Ev()
        {
            WriteWord(0, 100, 0x6000);
            ax = 0x2000;
            emit("xor ax, word [100]");
            run();
            Assert.Equal(0x4000, ax);
        }

        [Fact]
        public void Xor_AL_Ib()
        {
            al = 0x60;
            emit("xor al, 0x20");
            run();
            Assert.Equal(0x40, al);
        }

        [Fact]
        public void Xor_AX_Iv()
        {
            ax = 0x6000;
            emit("xor ax, 0x2000");
            run();
            Assert.Equal(0x4000, ax);
        }

        [Fact]
        public void AAA()
        {
            al = 0xCC;
            emit("aaa");
            run();
            Assert.Equal(2, al);
        }

    }
}
