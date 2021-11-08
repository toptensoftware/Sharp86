using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTestsE0 : CPUUnitTests
    {
        [Fact]
        public void loopnz_1()
        {
            cx = 1;
            emit("label1:");
            emit("nop");
            emit("nop");
            emit("loopnz label1");
            step();
            step();
            step();

            Assert.Equal(0x104, ip);
            Assert.Equal(0, cx);
        }

        [Fact]
        public void loopnz_2()
        {
            cx = 0;
            emit("label1:");
            emit("nop");
            emit("nop");
            emit("loopnz label1");
            step();
            step();
            step();

            Assert.Equal(0x100, ip);
            Assert.Equal(0xFFFF, cx);
        }

        [Fact]
        public void loopnz_3()
        {
            cx = 1;
            FlagZ = true;
            emit("label1:");
            emit("nop");
            emit("nop");
            emit("loopnz label1");
            step();
            step();
            step();

            Assert.Equal(0x104, ip);
            Assert.Equal(0, cx);
        }



        [Fact]
        public void loopz_1()
        {
            FlagZ = true;
            cx = 1;
            emit("label1:");
            emit("nop");
            emit("nop");
            emit("loopz label1");
            step();
            step();
            step();

            Assert.Equal(0x104, ip);
            Assert.Equal(0, cx);
        }

        [Fact]
        public void loopz_2()
        {
            FlagZ = true;
            cx = 0;
            emit("label1:");
            emit("nop");
            emit("nop");
            emit("loopz label1");
            step();
            step();
            step();

            Assert.Equal(0x100, ip);
            Assert.Equal(0xFFFF, cx);
        }

        [Fact]
        public void loopz_3()
        {
            cx = 1;
            FlagZ = false;
            emit("label1:");
            emit("nop");
            emit("nop");
            emit("loopz label1");
            step();
            step();
            step();

            Assert.Equal(0x104, ip);
            Assert.Equal(0, cx);
        }


        [Fact]
        public void loop_1()
        {
            FlagZ = true;
            cx = 1;
            emit("label1:");
            emit("nop");
            emit("nop");
            emit("loop label1");
            step();
            step();
            step();

            Assert.Equal(0x104, ip);
            Assert.Equal(0, cx);
        }

        [Fact]
        public void loop_2()
        {
            FlagZ = true;
            cx = 1;
            emit("label1:");
            emit("nop");
            emit("nop");
            emit("loop label1");
            step();
            step();
            step();

            Assert.Equal(0x104, ip);
            Assert.Equal(0, cx);
        }

        [Fact]
        public void jcxz_1()
        {
            emit("mov cx,0");
            emit("jcxz $+0x10");
            step();
            step();

            Assert.Equal(0x113, ip);
        }

        [Fact]
        public void jcxz_2()
        {
            emit("mov cx,1");
            emit("jcxz $+0x10");
            step();
            step();

            Assert.Equal(0x105, ip);
        }

        [Fact]
        public void in_al_dx()
        {
            EnqueueReadPortByte(0x1234, 0x78);

            ax = 0;
            dx = 0x1234;
            emit("in al,dx");
            step();

            Assert.Equal(0x78, ax);
            Assert.True(WasPortAccessed(0x1234));
        }

        [Fact]
        public void in_ax_dx()
        {
            EnqueueReadPortByte(0x1234, 0x78);
            EnqueueReadPortByte(0x1235, 0x56);

            dx = 0x1234;
            emit("in ax,dx");
            step();

            Assert.Equal(0x5678, ax);
            Assert.True(WasPortAccessed(0x1234));
            Assert.True(WasPortAccessed(0x1235));
        }

        [Fact]
        public void out_dx_al()
        {
            ax = 0x5678;
            dx = 0x1234;

            emit("out dx,al");
            step();

            Assert.Equal(0x78, DequeueWrittenPortByte(0x1234));
            Assert.True(WasPortAccessed(0x1234));
            Assert.False(WasPortAccessed(0x1235));
        }

        [Fact]
        public void out_dx_ax()
        {
            ax = 0x5678;
            dx = 0x1234;

            emit("out dx,ax");
            step();

            Assert.Equal(0x78, DequeueWrittenPortByte(0x1234));
            Assert.Equal(0x56, DequeueWrittenPortByte(0x1235));
            Assert.True(WasPortAccessed(0x1234));
            Assert.True(WasPortAccessed(0x1235));
        }
    }
}
