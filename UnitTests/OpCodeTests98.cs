using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTests98 : CPUUnitTests
    {
        [Fact]
        public void cbw()
        {
            ax = 0xFF;
            emit("cbw");
            run();
            Assert.Equal(0xFFFF, ax);
        }

        [Fact]
        public void cwd()
        {
            ax = 0xFFFF;
            emit("cwd");
            run();
            Assert.Equal(0xFFFFFFFF, dxax);
        }

        [Fact]
        public void call_Ap()
        {
            sp = 0x8008;
            emit("call 01:1234");
            step();
            Assert.Equal(0x8004, sp);
            Assert.Equal(0x105, ReadWord(ss, sp));
            Assert.Equal(0, ReadWord(ss, (ushort)(sp + 2)));
            Assert.Equal(1, cs);
            Assert.Equal(1234, ip);
        }

        [Fact]
        public void pushf()
        {
            sp = 0x8008;
            EFlags = 0x1234;
            emit("pushf");
            run();
            Assert.Equal(0x8006, sp);
            Assert.Equal(ReadWord(ss, sp), EFlags);
        }

        [Fact]
        public void popf()
        {
            sp = 0x8006;
            WriteWord(ss, sp, 0x1234);
            EFlags = 0;
            emit("popf");
            run();
            Assert.Equal(0x8008, sp);
            Assert.Equal(EFlags, (0x1234 & EFlag.SupportedBits) | EFlag.FixedBits);
        }

        [Fact]
        public void sahf()
        {
            Flags8 = 0;
            ah = 0xFF;
            emit("sahf");
            run();
            Assert.Equal(Flags8, (byte)(((0xFF & EFlag.SupportedBits) | EFlag.FixedBits) & 0xFF));
        }

        [Fact]
        public void lahf()
        {
            Flags8 = 0xFF;
            ah = 0;
            emit("lahf");
            run();
            Assert.Equal(ah, (byte)(((0xFF & EFlag.SupportedBits) | EFlag.FixedBits) & 0xFF));
        }

    }
}
