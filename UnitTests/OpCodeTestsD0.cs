using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTestsD0 : CPUUnitTests
    {
        [Fact]
        public void rol_eb_1()
        {
            al = 0x81;
            emit("rol al,1");
            step();
            Assert.Equal(0x03, al);
        }

        [Fact]
        public void ror_eb_1()
        {
            al = 0x81;
            emit("ror al,1");
            step();
            Assert.Equal(0xc0, al);
        }

        [Fact]
        public void rcl_eb_1()
        {
            FlagC = true;
            al = 0x01;
            emit("rcl al,1");
            step();
            Assert.Equal(0x03, al);
            Assert.False(FlagC);
        }

        [Fact]
        public void rcr_eb_1()
        {
            FlagC = true;
            al = 0x80;
            emit("rcr al,1");
            step();
            Assert.Equal(0xC0, al);
            Assert.False(FlagC);
        }

        [Fact]
        public void shl_eb_1()
        {
            al = 0x81;
            emit("shl al,1");
            step();
            Assert.Equal(0x02, al);
        }

        [Fact]
        public void shr_eb_1()
        {
            al = 0x81;
            emit("shr al,1");
            step();
            Assert.Equal(0x40, al);
        }

        [Fact]
        public void sar_eb_1()
        {
            al = 0x82;
            emit("sar al,1");
            step();
            Assert.Equal(0xC1, al);
        }



        [Fact]
        public void rol_ev_1()
        {
            ax = 0x8001;
            emit("rol ax,1");
            step();
            Assert.Equal(0x03, al);
        }

        [Fact]
        public void ror_ev_1()
        {
            ax = 0x8001;
            emit("ror ax,1");
            step();
            Assert.Equal(0xc000, ax);
        }

        [Fact]
        public void rcl_Ev_1()
        {
            FlagC = true;
            ax = 0x01;
            emit("rcl ax,1");
            step();
            Assert.Equal(0x03, ax);
            Assert.False(FlagC);
        }

        [Fact]
        public void rcr_ev_1()
        {
            FlagC = true;
            ax = 0x8000;
            emit("rcr ax,1");
            step();
            Assert.Equal(0xC000, ax);
            Assert.False(FlagC);
        }

        [Fact]
        public void shl_ev_1()
        {
            ax = 0x8001;
            emit("shl ax,1");
            step();
            Assert.Equal(0x02, ax);
        }

        [Fact]
        public void shr_ev_1()
        {
            ax = 0x8001;
            emit("shr ax,1");
            step();
            Assert.Equal(0x4000, ax);
        }

        [Fact]
        public void sar_ev_1()
        {
            ax = 0x8002;
            emit("sar ax,1");
            step();
            Assert.Equal(0xC001, ax);
        }




        [Fact]
        public void rol_eb_cl()
        {
            al = 0x81;
            cl = 1;
            emit("rol al,cl");
            step();
            Assert.Equal(0x03, al);
        }

        [Fact]
        public void ror_eb_cl()
        {
            al = 0x81;
            cl = 1;
            emit("ror al,cl");
            step();
            Assert.Equal(0xc0, al);
        }

        [Fact]
        public void rcl_eb_cl()
        {
            FlagC = true;
            al = 0x01;
            cl = 1;
            emit("rcl al,cl");
            step();
            Assert.Equal(0x03, al);
            Assert.False(FlagC);
        }

        [Fact]
        public void rcr_eb_cl()
        {
            FlagC = true;
            al = 0x80;
            cl = 1;
            emit("rcr al,cl");
            step();
            Assert.Equal(0xC0, al);
            Assert.False(FlagC);
        }

        [Fact]
        public void shl_eb_cl()
        {
            al = 0x81;
            cl = 1;
            emit("shl al,cl");
            step();
            Assert.Equal(0x02, al);
        }

        [Fact]
        public void shr_eb_cl()
        {
            al = 0x81;
            cl = 1;
            emit("shr al,cl");
            step();
            Assert.Equal(0x40, al);
        }

        [Fact]
        public void sar_eb_cl()
        {
            al = 0x82;
            cl = 1;
            emit("sar al,cl");
            step();
            Assert.Equal(0xC1, al);
        }



        [Fact]
        public void rol_ev_cl()
        {
            ax = 0x8001;
            cl = 1;
            emit("rol ax,cl");
            step();
            Assert.Equal(0x03, al);
        }

        [Fact]
        public void ror_ev_cl()
        {
            ax = 0x8001;
            cl = 1;
            emit("ror ax,cl");
            step();
            Assert.Equal(0xc000, ax);
        }

        [Fact]
        public void rcl_Ev_cl()
        {
            FlagC = true;
            ax = 0x01;
            cl = 1;
            emit("rcl ax,cl");
            step();
            Assert.Equal(0x03, ax);
            Assert.False(FlagC);
        }

        [Fact]
        public void rcr_ev_cl()
        {
            FlagC = true;
            ax = 0x8000;
            cl = 1;
            emit("rcr ax,cl");
            step();
            Assert.Equal(0xC000, ax);
            Assert.False(FlagC);
        }

        [Fact]
        public void shl_ev_cl()
        {
            ax = 0x8001;
            cl = 1;
            emit("shl ax,cl");
            step();
            Assert.Equal(0x02, ax);
        }

        [Fact]
        public void shr_ev_cl()
        {
            ax = 0x8001;
            cl = 1;
            emit("shr ax,cl");
            step();
            Assert.Equal(0x4000, ax);
        }

        [Fact]
        public void sar_ev_cl()
        {
            ax = 0x8002;
            cl = 1;
            emit("sar ax,cl");
            step();
            Assert.Equal(0xC001, ax);
        }

        [Fact]
        public void Aam_test()
        {
            ax = 0xFF0E;
            emit("aam");
            step();
            Assert.Equal(0x104, ax);
        }

        [Fact]
        public void Aam_Im()
        {
            ax = 0xFF0E;
            emit("aam 8");
            step();
            Assert.Equal(0x106, ax);
        }

        [Fact]
        public void Aad_test()
        {
            ax = 0xFF0E;
            emit("aad");
            step();
            Assert.Equal(0x0004, ax);
        }

        [Fact]
        public void Aad_Im()
        {
            ax = 0xFF0E;
            emit("aad 8");
            step();
            Assert.Equal(0x006, ax);
        }

        [Fact]
        public void xlat()
        {
            bx = 0x1000;
            for (int i=0; i<256; i++)
            {
                WriteByte(ds, (ushort)(bx + i), (byte)i);
            }

            al = 0x20;
            emit("xlat");
            step();
            Assert.Equal(0x20, al);

            al = 0x90;      // al is signed
            emit("xlat");
            step();
            Assert.Equal(0x90, al);
        }
    }

}
