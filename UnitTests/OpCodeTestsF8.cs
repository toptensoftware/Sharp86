using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTestsF8 : CPUUnitTests
    {
        [Fact]
        public void clc()
        {
            FlagC = true;
            emit("clc");
            step();
            Assert.False(FlagC);
        }

        [Fact]
        public void stc()
        {
            FlagC = false;
            emit("stc");
            step();
            Assert.True(FlagC);
        }

        [Fact]
        public void cli()
        {
            FlagI = true;
            emit("cli");
            step();
            Assert.False(FlagI);
        }

        [Fact]
        public void sti()
        {
            FlagI = false;
            emit("sti");
            step();
            Assert.True(FlagI);
        }

        [Fact]
        public void cld()
        {
            FlagD = true;
            emit("cld");
            step();
            Assert.False(FlagD);
        }

        [Fact]
        public void std()
        {
            FlagD = false;
            emit("std");
            step();
            Assert.True(FlagD);
        }

        [Fact]
        public void inc_Eb()
        {
            bx = 0x1000;
            MMU.WriteByte(ds, bx, 0x12);
            emit("inc byte [bx]");
            step();
            Assert.Equal(0x13, MMU.ReadByte(ds, bx));
        }

        [Fact]
        public void dec_Eb()
        {
            bx = 0x1000;
            MMU.WriteByte(ds, bx, 0x12);
            emit("dec byte [bx]");
            step();
            Assert.Equal(0x11, MMU.ReadByte(ds, bx));
        }

        [Fact]
        public void inc_Ev()
        {
            bx = 0x1000;
            WriteWord(ds, bx, 0x1234);
            emit("inc word [bx]");
            step();
            Assert.Equal(0x1235, ReadWord(ds, bx));
        }

        [Fact]
        public void dec_Ev()
        {
            bx = 0x1000;
            WriteWord(ds, bx, 0x1234);
            emit("dec word [bx]");
            step();
            Assert.Equal(0x1233, ReadWord(ds, bx));
        }

        [Fact]
        public void call_Ev()
        {
            sp = 0x2000;
            ax = 0x1000;
            emit("call ax");
            step();
            Assert.Equal(0x1000, ip);
            Assert.Equal(0x1ffe, sp);
            ushort retaddr = ReadWord(ss, sp);
            Assert.Equal(0x0102, retaddr);
        }

        [Fact]
        public void call_far_Mp()
        {
            sp = 0x2000;
            bx = 0x1000;
            WriteWord(ds, bx, 0x2000);
            WriteWord(ds, (ushort)(bx+2), 0x4000);

            emit("call word far [bx]");
            step();

            Assert.Equal(0x2000, ip);
            Assert.Equal(0x4000, cs);
            Assert.Equal(0x1ffc, sp);
            Assert.Equal(0x102, ReadWord(ss, sp));
            Assert.Equal(0, ReadWord(ss, (ushort)(sp + 2)));
        }

        [Fact]
        public void jmp_Ev()
        {
            ax = 0x1000;
            emit("jmp ax");
            step();
            Assert.Equal(0x1000, ip);
        }

        [Fact]
        public void jmp_far_Mp()
        {
            bx = 0x1000;
            WriteWord(ds, bx, 0x2000);
            WriteWord(ds, (ushort)(bx + 2), 0x4000);

            emit("jmp word far [bx]");
            step();

            Assert.Equal(0x2000, ip);
            Assert.Equal(0x4000, cs);
        }

        [Fact]
        public void push_Ev()
        {
            sp = 0x1000;
            bx = 0x1000;
            WriteWord(ds, bx, 0x1234);

            emit("push word [bx]");
            step();

            Assert.Equal(0xFFE, sp);
            Assert.Equal(0x1234, ReadWord(ss, sp));
        }


    }
}
