using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTestsC0 : CPUUnitTests
    {
        [Fact]
        public void rol_eb_Ib()
        {
            al = 0x81;
            emit("rol al,2");
            step();
            Assert.Equal(0x06, al);
        }

        [Fact]
        public void ror_eb_Ib()
        {
            al = 0x81;
            emit("ror al,2");
            step();
            Assert.Equal(0x60, al);
        }

        [Fact]
        public void rcl_eb_Ib()
        {
            FlagC = true;
            al = 0x01;
            emit("rcl al,2");
            step();
            Assert.Equal(0x06, al);
            Assert.False(FlagC);
        }

        [Fact]
        public void rcr_eb_Ib()
        {
            FlagC = true;
            al = 0x80;
            emit("rcr al,2");
            step();
            Assert.Equal(0x60, al);
            Assert.False(FlagC);
        }

        [Fact]
        public void shl_eb_Ib()
        {
            al = 0x81;
            emit("shl al,2");
            step();
            Assert.Equal(0x04, al);
        }

        [Fact]
        public void shr_eb_Ib()
        {
            al = 0x81;
            emit("shr al,2");
            step();
            Assert.Equal(0x20, al);
        }

        [Fact]
        public void sar_eb_Ib()
        {
            al = 0x84;
            emit("sar al,2");
            step();
            Assert.Equal(0xE1, al);
        }



        [Fact]
        public void rol_ev_Ib()
        {
            ax = 0x8001;
            emit("rol ax,2");
            step();
            Assert.Equal(0x06, al);
        }

        [Fact]
        public void ror_ev_Ib()
        {
            ax = 0x8001;
            emit("ror ax,2");
            step();
            Assert.Equal(0x6000, ax);
        }

        [Fact]
        public void rcl_Ev_Ib()
        {
            FlagC = true;
            ax = 0x01;
            emit("rcl ax,2");
            step();
            Assert.Equal(0x06, ax);
            Assert.False(FlagC);
        }

        [Fact]
        public void rcr_ev_Ib()
        {
            FlagC = true;
            ax = 0x8000;
            emit("rcr ax,2");
            step();
            Assert.Equal(0x6000, ax);
            Assert.False(FlagC);
        }

        [Fact]
        public void shl_ev_Ib()
        {
            ax = 0x8001;
            emit("shl ax,2");
            step();
            Assert.Equal(0x04, ax);
        }

        [Fact]
        public void shr_ev_Ib()
        {
            ax = 0x8001;
            emit("shr ax,2");
            step();
            Assert.Equal(0x2000, ax);
        }

        [Fact]
        public void sar_ev_Ib()
        {
            ax = 0x8004;
            emit("sar ax,2");
            step();
            Assert.Equal(0xE001, ax);
        }



        [Fact]
        public void ret_Iv()
        {
            sp = 0xFFE;
            WriteWord(ss, sp, 0x8000);
            emit("ret 0x1000");
            step();
            Assert.Equal(0x8000, ip);
            Assert.Equal(0x2000, sp);
        }

        [Fact]
        public void ret()
        {
            sp = 0xFFE;
            WriteWord(ss, sp, 0x8000);
            emit("ret");
            step();
            Assert.Equal(0x8000, ip);
            Assert.Equal(0x1000, sp);
        }

        [Fact]
        public void les()
        {
            bx = 0x1000;
            WriteWord(ds, bx, 0x1234);
            WriteWord(ds, (ushort)(bx + 2), 0x5678);

            emit("les si,[bx]");
            step();

            Assert.Equal(0x1234, si);
            Assert.Equal(0x5678, es);
        }

        [Fact]
        public void lds()
        {
            bx = 0x1000;
            WriteWord(ds, bx, 0x1234);
            WriteWord(ds, (ushort)(bx + 2), 0x5678);

            emit("lds si,[bx]");
            step();

            Assert.Equal(0x1234, si);
            Assert.Equal(0x5678, ds);
        }

        [Fact]
        public void mov_Eb_Ib()
        {
            bx = 0x1000;

            emit("mov byte [bx],012h");
            step();

            Assert.Equal(0x12, ReadByte(ds, bx));
        }

        [Fact]
        public void mov_Ev_Iv()
        {
            bx = 0x1000;

            emit("mov word [bx],01234h");
            step();

            Assert.Equal(0x1234, ReadWord(ds, bx));
        }

    }

}
