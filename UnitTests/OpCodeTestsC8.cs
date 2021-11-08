using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTestsC8 : CPUUnitTests
    {
        [Fact]
        public void enter_leave()
        {
            sp = 0x1000;
            bp = 0x1010;
            emit("enter 0x100, 0");
            step();

            Assert.Equal(0x1010, ReadWord(ss, 0x0FFE));      // BP on stack
            Assert.Equal(0xFFE, bp);
            Assert.Equal(0xEFE, sp);

            emit("leave");
            step();
            Assert.Equal(0x1000, sp);
            Assert.Equal(0x1010, bp);
        }

        [Fact]
        public void enter_leave_nested()
        {
            sp = 0x1000;
            bp = 0x1010;
            emit("enter 0x100, 0");
            step();

            Assert.Equal(0x1010, ReadWord(ss, 0x0FFE));      // BP on stack
            Assert.Equal(0xFFE, bp);
            Assert.Equal(0xEFE, sp);

            emit("enter 0x30, 1");
            step();
            Assert.Equal(0xEFC, bp);
            Assert.Equal(0xECA, sp);

            Assert.Equal(0xFFE, ReadWord(ss, bp));
            Assert.Equal(0xEFC, ReadWord(ss, (ushort)(bp-2)));

            emit("enter 0x20, 2");
            step();
            Assert.Equal(0xEC8, bp);
            Assert.Equal(0xEA4, sp);

            Assert.Equal(0xEFC, ReadWord(ss, bp));
            Assert.Equal(0xEFC, ReadWord(ss, (ushort)(bp - 2)));
            Assert.Equal(0xEC8, ReadWord(ss, (ushort)(bp - 4)));

            emit("leave");
            step();
            Assert.Equal(0xEFC, bp);
            Assert.Equal(0xECA, sp);

            emit("leave");
            step();
            Assert.Equal(0xFFE, bp);
            Assert.Equal(0xEFE, sp);

            emit("leave");
            step();
            Assert.Equal(0x1000, sp);
            Assert.Equal(0x1010, bp);
        }

        [Fact]
        public void retf_Iv()
        {
            sp = 0xFFC;
            WriteWord(ss, sp, 0x8000);
            WriteWord(ss, (ushort)(sp + 2), 0x1234);

            emit("retf 0x1000");
            step();
            Assert.Equal(0x8000, ip);
            Assert.Equal(0x1234, cs);
            Assert.Equal(0x2000, sp);
        }


        [Fact]
        public void retf()
        {
            sp = 0xFFC;
            WriteWord(ss, sp, 0x8000);
            WriteWord(ss, (ushort)(sp + 2), 0x1234);
            emit("retf");
            step();
            Assert.Equal(0x8000, ip);
            Assert.Equal(0x1234, cs);
            Assert.Equal(0x1000, sp);
        }

        byte _raisedInterrupt;

        [NotAFact]
        public override void RaiseInterrupt(byte interruptNumber)
        {
            _raisedInterrupt = interruptNumber;
        }

        [Fact]
        public void int3()
        {
            _raisedInterrupt = 0;
            emit("db 0xCC");        // int 3
            step();
            Assert.Equal(3, _raisedInterrupt);
        }

        [Fact]
        public void int_Ib()
        {
            _raisedInterrupt = 0;
            emit("int 21h");        // int 3
            step();
            Assert.Equal(0x21, _raisedInterrupt);
        }

        [Fact]
        public void into()
        {
            FlagO = false;
            _raisedInterrupt = 0;
            emit("into");
            step();
            Assert.Equal(0x00, _raisedInterrupt);

            FlagO = true;
            emit("into");
            step();
            Assert.Equal(0x04, _raisedInterrupt);
        }

        [Fact]
        public void iret()
        {
            sp = 0xFFA;
            WriteWord(ss, sp, 0x8000);
            WriteWord(ss, (ushort)(sp + 2), 0x1234);
            WriteWord(ss, (ushort)(sp + 4), 0xAAAA);
            emit("iret");
            step();
            Assert.Equal(0x8000, ip);
            Assert.Equal(0x1234, cs);
            Assert.Equal(EFlags, (0xAAAA & EFlag.SupportedBits) | EFlag.FixedBits);
            Assert.Equal(0x1000, sp);
        }


    }

}
