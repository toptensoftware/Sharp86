using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTests68 : CPUUnitTests
    {
        [Fact]
        public void push_Ib()
        {
            sp = 0x1000;
            emit("push -1");
            step();

            Assert.Equal(0x0FFE, sp);
            Assert.Equal(0xFFFF, ReadWord(ss, sp));  // byte will be sign extended
        }

        [Fact]
        public void push_Iv()
        {
            sp = 0x1000;
            emit("push 01ffh");
            step();

            Assert.Equal(0x0FFE, sp);
            Assert.Equal(0x01ff, ReadWord(ss, sp));
        }

        [Fact]
        public void imul_Gv_Ev_Ib()
        {
            ax = 0;
            bx = 10;
            emit("imul ax,bx,20");
            step();
            Assert.Equal(200, ax);

            bx = 0xFFFF;
            emit("imul ax,bx,10");
            step();
            Assert.Equal(0xFFF6, ax);
        }

        [Fact]
        public void imul_Gv_Ev_Iv()
        {
            ax = 0;
            bx = 10;
            emit("imul ax,bx,200");
            step();
            Assert.Equal(2000, ax);

            bx = 0xFFFF;
            emit("imul ax,bx,1000");
            step();
            Assert.Equal(ax, unchecked((ushort)(short)-1000));
        }


        [Fact]
        public void insb()
        {
            EnqueueReadPortByte(0x5678, 0x12);

            di = 0x1000;
            dx = 0x5678;
            emit("insb");
            step();

            Assert.True(WasPortAccessed(0x5678));
            Assert.Equal(0x1001, di);
            Assert.Equal(0x12, ReadByte(ds, 0x1000));
        }

        [Fact]
        public void rep_insb()
        {
            for (int i=0; i<5; i++)
            {
                EnqueueReadPortByte(0x5678, (byte)(0x12 + i));
            }

            di = 0x1000;
            dx = 0x5678;
            cx = 5;
            emit("rep insb");
            step();

            Assert.True(WasPortAccessed(0x5678));
            Assert.Equal(0x1005, di);
            Assert.Equal(0, cx);
            for (int i=0; i<5; i++)
            {
                Assert.Equal(ReadByte(ds, (ushort)(0x1000 + i)), (byte)(0x12 + i));
            }
        }

        [Fact]
        public void rep_insb_cx0()
        {
            di = 0x1000;
            dx = 0x5678;
            cx = 0;
            emit("rep insb");
            step();

            Assert.False(WasPortAccessed(0x5678));
            Assert.Equal(0x1000, di);
            Assert.Equal(0, cx);
        }

        [Fact]
        public void insw()
        {
            EnqueueReadPortByte(0x5678, 0x34);
            EnqueueReadPortByte(0x5679, 0x12);

            di = 0x1000;
            dx = 0x5678;
            emit("insw");
            step();

            Assert.True(WasPortAccessed(0x5678));
            Assert.True(WasPortAccessed(0x5679));
            Assert.Equal(0x1002, di);
            Assert.Equal(0x1234, ReadWord(ds, 0x1000));
        }

        [Fact]
        public void rep_insw()
        {
            for (int i=0; i<5; i++)
            {
                EnqueueReadPortByte(0x5678, (byte)(0x34 + i));
                EnqueueReadPortByte(0x5679, 0x12);
            }

            di = 0x1000;
            dx = 0x5678;
            cx = 5;
            emit("rep insw");
            step();

            Assert.True(WasPortAccessed(0x5678));
            Assert.True(WasPortAccessed(0x5679));
            Assert.Equal(0x100A, di);
            Assert.Equal(0, cx);
            for (int i = 0; i < 5; i++)
            {
                Assert.Equal(ReadWord(ds, (ushort)(0x1000 + i * 2)), (ushort)(0x1234 + i));
            }
        }

        [Fact]
        public void rep_insw_cx0()
        {
            di = 0x1000;
            dx = 0x5678;
            cx = 0;
            emit("rep insw");
            step();

            Assert.False(WasPortAccessed(0x5678));
            Assert.False(WasPortAccessed(0x5679));
            Assert.Equal(0x1000, di);
            Assert.Equal(0, cx);
        }

        [Fact]
        public void outsb()
        {
            si = 0x1000;
            dx = 0x5678;
            WriteByte(ds, si, 0x12);
            emit("outsb");
            step();

            Assert.True(WasPortAccessed(0x5678));
            Assert.Equal(0x1001, si);
            Assert.Equal(0x12, DequeueWrittenPortByte(0x5678));
        }

        [Fact]
        public void rep_outsb()
        {
            si = 0x1000;
            dx = 0x5678;
            cx = 5;
            for (int i = 0; i < 5; i++)
            {
                WriteByte(ds, (ushort)(si + i), (byte)(0x12 + i));
            }
            emit("rep outsb");
            step();

            Assert.True(WasPortAccessed(0x5678));
            Assert.Equal(0x1005, si);
            for (int i = 0; i < 5; i++)
            {
                Assert.Equal(DequeueWrittenPortByte(0x5678), (byte)(0x12 + i));
            }
        }

        [Fact]
        public void rep_outsb_cx0()
        {
            si = 0x1000;
            dx = 0x5678;
            cx = 0;
            emit("rep outsb");
            step();

            Assert.False(WasPortAccessed(0x5678));
            Assert.Equal(0x1000, si);
        }

        [Fact]
        public void outsw()
        {
            si = 0x1000;
            dx = 0x5678;
            WriteWord(ds, si, 0x1234);
            emit("outsw");
            step();

            Assert.True(WasPortAccessed(0x5678));
            Assert.True(WasPortAccessed(0x5679));
            Assert.Equal(0x1002, si);
            Assert.Equal(0x34, DequeueWrittenPortByte(0x5678));
            Assert.Equal(0x12, DequeueWrittenPortByte(0x5679));
        }

        [Fact]
        public void rep_outsw()
        {
            si = 0x1000;
            dx = 0x5678;
            cx = 5;
            for (int i = 0; i < 5; i++)
            {
                WriteWord(ds, (ushort)(si + i * 2), (ushort)(0x1234 + i));
            }
            emit("rep outsw");
            step();

            Assert.True(WasPortAccessed(0x5678));
            Assert.Equal(0x100A, si);
            for (int i = 0; i < 5; i++)
            {
                Assert.Equal(DequeueWrittenPortByte(0x5678), (byte)(0x34 + i));
                Assert.Equal(0x12, DequeueWrittenPortByte(0x5679));
            }
        }

        [Fact]
        public void rep_outsw_cx0()
        {
            si = 0x1000;
            dx = 0x5678;
            cx = 0;
            emit("rep outsw");
            step();

            Assert.False(WasPortAccessed(0x5678));
            Assert.Equal(0x1000, si);
        }

    }
}
