using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTestsF0 : CPUUnitTests
    {
        [Fact]
        public void hlt()
        {
            cx = 1;
            emit("hlt");
            step();
            Assert.True(Halted);
        }

        [Fact]
        public void cmc()
        {
            FlagC = false;
            emit("cmc");
            step();
            Assert.True(FlagC);

            emit("cmc");
            step();
            Assert.False(FlagC);
        }

        [Fact]
        public void test_Eb_Ib()
        {
            si = 0x8000;
            MMU.WriteByte(ds, si, 0x88);
            emit("test byte [si],0x80");
            step();
            Assert.False(FlagZ);

            emit("test byte [si],0x1");
            step();
            Assert.True(FlagZ);
        }

        [Fact]
        public void not_Eb_Ib()
        {
            si = 0x8000;
            MMU.WriteByte(ds, si, 0x88);
            emit("not byte [si]");
            step();
            Assert.Equal(0x77, MMU.ReadByte(ds, si));
        }

        [Fact]
        public void neg_Eb_Ib()
        {
            si = 0x8000;
            MMU.WriteByte(ds, si, 1);
            emit("neg byte [si]");
            step();
            Assert.Equal(0xFF, MMU.ReadByte(ds, si));
        }

        [Fact]
        public void mul_Eb_Ib()
        {
            al = 100;
            MMU.WriteByte(ds, si, 200);
            emit("mul byte [si]");
            step();
            Assert.Equal(20000, ax);
        }

        [Fact]
        public void imul_Eb_Ib()
        {
            al = 0xFF;
            MMU.WriteByte(ds, si, 0xFF);
            emit("imul byte [si]");
            step();
            Assert.Equal(1, ax);
        }

        [Fact]
        public void div_Eb_Ib()
        {
            ax = 20001;
            MMU.WriteByte(ds, si, 200);
            emit("div byte [si]");
            step();
            Assert.Equal(100, al);
            Assert.Equal(1, ah);
        }

        [Fact]
        public void idiv_Eb_Ib()
        {
            ax = 10001;
            MMU.WriteByte(ds, si, 100);
            emit("idiv byte [si]");
            step();
            Assert.Equal(100, al);
            Assert.Equal(1, ah);
        }



        [Fact]
        public void test_Ev_Iv()
        {
            si = 0x8000;
            WriteWord(ds, si, 0x8888);
            emit("test word [si],0x8000");
            step();
            Assert.False(FlagZ);

            emit("test word [si],0x1");
            step();
            Assert.True(FlagZ);
        }

        [Fact]
        public void not_Ev_Iv()
        {
            si = 0x8000;
            WriteWord(ds, si, 0x8888);
            emit("not word [si]");
            step();
            Assert.Equal(0x7777, ReadWord(ds, si));
        }

        [Fact]
        public void neg_Ev_Iv()
        {
            si = 0x8000;
            WriteWord(ds, si, 1);
            emit("neg word [si]");
            step();
            Assert.Equal(0xFFFF, ReadWord(ds, si));
        }

        [Fact]
        public void mul_Ev_Iv()
        {
            ax = 100;
            dx = 0xFFFF;
            WriteWord(ds, si, 200);
            emit("mul word [si]");
            step();
            Assert.Equal(20000, ax);
            Assert.Equal(0, dx);
        }

        [Fact]
        public void imul_Ev_Iv()
        {
            ax = 0xFFFF;
            dx = 0xFFFF;
            WriteWord(ds, si, 0xFFFF);
            emit("imul word [si]");
            step();
            Assert.Equal(1, ax);
            Assert.Equal(0, dx);
        }

        [Fact]
        public void div_Ev_Iv()
        {
            ax = 20001;
            dx = 0;
            WriteWord(ds, si, 200);
            emit("div word [si]");
            step();
            Assert.Equal(100, ax);
            Assert.Equal(1, dx);
        }

        [Fact]
        public void idiv_Ev_Iv()
        {
            ax = 20001;
            dx = 0;
            WriteWord(ds, si, 100);
            emit("idiv word [si]");
            step();
            Assert.Equal(200, ax);
            Assert.Equal(1, dx);
        }

    }
}
