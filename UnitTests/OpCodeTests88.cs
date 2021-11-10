using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTests88 : CPUUnitTests
    {
        [Fact]
        public void Mov_Eb_Gb()
        {
            MMU.WriteByte(0, 100, 40);
            al = 20;
            bx = 100;
            emit("mov byte [bx], al");
            run();
            Assert.Equal(20, MMU.ReadByte(0, 100));
        }

        [Fact]
        public void Mov_Ev_Gv()
        {
            WriteWord(0, 100, 4000);
            ax = 2000;
            bx = 100;
            emit("mov word [bx], ax");
            run();
            Assert.Equal(2000, ReadWord(0, 100));
        }

        [Fact]
        public void Add_Gb_Eb()
        {
            MMU.WriteByte(0, 100, 40);
            al = 20;
            bx = 100;
            emit("mov al, byte [bx]");
            run();
            Assert.Equal(40, al);
        }

        [Fact]
        public void Add_Gv_Ev()
        {
            WriteWord(0, 100, 4000);
            ax = 2000;
            bx = 100;
            emit("mov ax, word [bx]");
            run();
            Assert.Equal(4000, ax);
        }

        [Fact]
        public void Mov_Ev_Sw()
        {
            WriteWord(0, 100, 4000);
            es = 2000;
            bx = 100;
            emit("mov word [bx], es");
            run();
            Assert.Equal(2000, ReadWord(0, 100));
        }

        [Fact]
        public void Lea_Gv_M()
        {
            WriteWord(0, 100, 4000);
            bx = 0x8000;
            si = 0x0400;
            emit("lea ax,word [bx+si+20h]");
            run();
            Assert.Equal(0x8420, ax);
        }

        [Fact]
        public void Add_Sw_Ev()
        {
            WriteWord(0, 100, 4000);
            es = 2000;
            bx = 100;
            emit("mov word [bx], es");
            run();
            Assert.Equal(2000, ReadWord(0, 100));
        }

        [Fact]
        public void Pop_Ev()
        {
            sp = 0x8006;
            WriteWord(ss, sp, 4000);
            bx = 0x1000;
            si = 0x0100;
            emit("pop word [bx+si]");
            run();
            Assert.Equal(4000, ReadWord(ds, 0x1100));
            Assert.Equal(0x8008, sp);
        }




    }
}
