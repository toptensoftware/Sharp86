using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTests08 : CPUUnitTests
    {
        [Fact]
        public void Or_Eb_Gb()
        {
            WriteByte(0, 100, 0x41);
            al = 0x21;
            emit("or byte [100], al");
            run();
            Assert.Equal(0x61, ReadByte(0, 100));
        }

        [Fact]
        public void Or_Ev_Gv()
        {
            WriteWord(0, 100, 0x4001);
            ax = 0x2001;
            emit("or word [100], ax");
            run();
            Assert.Equal(0x6001, ReadWord(0, 100));
        }

        [Fact]
        public void Or_Gb_Eb()
        {
            WriteByte(0, 100, 0x41);
            al = 0x21;
            emit("or al, byte [100]");
            run();
            Assert.Equal(0x61, al);
        }

        [Fact]
        public void Or_Gv_Ev()
        {
            WriteWord(0, 100, 0x4001);
            ax = 0x2001;
            emit("or ax, word [100]");
            run();
            Assert.Equal(0x6001, ax);
        }

        [Fact]
        public void Or_AL_Ib()
        {
            al = 0x41;
            emit("or al, 21h");
            run();
            Assert.Equal(0x61, al);
        }

        [Fact]
        public void Or_AX_Iv()
        {
            ax = 0x4001;
            emit("or ax, 0x2001");
            run();
            Assert.Equal(0x6001, ax);
        }

        [Fact]
        public void PUSH_CS()
        {
            WriteWord(ss, (ushort)(sp - 2), 0x1234);
            sp = 0x8008;
            emit("push cs");
            run();
            Assert.Equal(0, ReadWord(ss, sp));
            Assert.Equal(0x8006, sp);
        }

    }
}
