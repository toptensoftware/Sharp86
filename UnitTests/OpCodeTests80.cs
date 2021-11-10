using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTests80 : CPUUnitTests
    {
        [Fact]
        public void Add_Eb_Ib()
        {
            MMU.WriteByte(0, 100, 40);
            emit("add byte [100], 20");
            run();
            Assert.Equal(60, MMU.ReadByte(0, 100));
        }

        [Fact]
        public void Or_Eb_Ib()
        {
            MMU.WriteByte(0, 100, 0x41);
            emit("or byte [100], 21h");
            run();
            Assert.Equal(0x61, MMU.ReadByte(0, 100));
        }

        [Fact]
        public void Adc_Eb_Ib()
        {
            MMU.WriteByte(0, 100, 40);
            FlagC = true;
            emit("adc byte [100], 20");
            run();
            Assert.Equal(61, MMU.ReadByte(0, 100));
        }

        [Fact]
        public void Sbb_Eb_Ib()
        {
            MMU.WriteByte(0, 100, 40);
            FlagC = true;
            emit("sbb byte [100], 10");
            run();
            Assert.Equal(29, MMU.ReadByte(0, 100));
        }

        [Fact]
        public void And_Eb_Ib()
        {
            MMU.WriteByte(0, 100, 0x60);
            emit("and byte [100], 20h");
            run();
            Assert.Equal(0x20, MMU.ReadByte(0, 100));
        }

        [Fact]
        public void Sub_Eb_Ib()
        {
            MMU.WriteByte(0, 100, 40);
            FlagC = true;
            emit("sub byte [100], 10");
            run();
            Assert.Equal(30, MMU.ReadByte(0, 100));
        }

        [Fact]
        public void Xor_Eb_Ib()
        {
            MMU.WriteByte(0, 100, 0x60);
            emit("xor byte [100], 0x20");
            run();
            Assert.Equal(0x40, MMU.ReadByte(0, 100));
        }

        [Fact]
        public void Cmp_Eb_Ib()
        {
            MMU.WriteByte(0, 100, 40);
            FlagC = true;
            FlagZ = true;
            emit("cmp byte [100], 10");
            run();
            Assert.Equal(40, MMU.ReadByte(0, 100));
            Assert.False(FlagZ);
            Assert.False(FlagC);
        }





        [Fact]
        public void Add_Ev_Iv()
        {
            WriteWord(0, 100, 4000);
            emit("add word [100], 2000");
            run();
            Assert.Equal(6000, ReadWord(0, 100));
        }

        [Fact]
        public void Or_Ev_Iv()
        {
            WriteWord(0, 100, 0x4001);
            emit("or word [100], 2001h");
            run();
            Assert.Equal(0x6001, ReadWord(0, 100));
        }


        [Fact]
        public void Adc_Ev_Iv()
        {
            WriteWord(0, 100, 4000);
            FlagC = true;
            emit("adc word [100], 2000");
            run();
            Assert.Equal(6001, ReadWord(0, 100));
        }

        [Fact]
        public void Sbb_Ev_Iv()
        {
            WriteWord(0, 100, 4000);
            FlagC = true;
            emit("sbb word [100], 1000");
            run();
            Assert.Equal(2999, ReadWord(0, 100));
        }

        [Fact]
        public void And_Ev_Iv()
        {
            WriteWord(0, 100, 0x6000);
            emit("and word [100], 2000h");
            run();
            Assert.Equal(0x2000, ReadWord(0, 100));
        }

        [Fact]
        public void Sub_Ev_Iv()
        {
            WriteWord(0, 100, 4000);
            FlagC = true;
            emit("sub word [100], 1000");
            run();
            Assert.Equal(3000, ReadWord(0, 100));
        }

        [Fact]
        public void Xor_Ev_Iv()
        {
            WriteWord(0, 100, 0x6000);
            emit("xor word [100], 2000h");
            run();
            Assert.Equal(0x4000, ReadWord(0, 100));
        }

        [Fact]
        public void Cmp_Ev_Iv()
        {
            WriteWord(0, 100, 4000);
            FlagC = true;
            FlagZ = true;
            emit("cmp word [100], 1000");
            run();
            Assert.Equal(4000, ReadWord(0, 100));
            Assert.False(FlagZ);
            Assert.False(FlagC);
        }





        [Fact]
        public void Add_Ev_Ib()
        {
            WriteWord(0, 100, 4000);
            emit("add word [100], byte 0xFF");
            run();
            Assert.Equal(3999, ReadWord(0, 100));
        }

        [Fact]
        public void Or_Ev_Ib()
        {
            WriteWord(0, 100, 0x4001);
            emit("or word [100], byte 0xFE");
            run();
            Assert.Equal(0xFFFF, ReadWord(0, 100));
        }


        [Fact]
        public void Adc_Ev_Ib()
        {
            WriteWord(0, 100, 4000);
            FlagC = true;
            emit("adc word [100], byte 0xFE");
            run();
            Assert.Equal(3999, ReadWord(0, 100));
        }

        [Fact]
        public void Sbb_Ev_Ib()
        {
            WriteWord(0, 100, 4000);
            FlagC = true;
            emit("sbb word [100], byte 0xFE");
            run();
            Assert.Equal(4001, ReadWord(0, 100));
        }

        [Fact]
        public void And_Ev_Ib()
        {
            WriteWord(0, 100, 0x6000);
            emit("and word [100], byte 0xFE");
            run();
            Assert.Equal(0x6000, ReadWord(0, 100));
        }

        [Fact]
        public void Sub_Ev_Ib()
        {
            WriteWord(0, 100, 4000);
            FlagC = true;
            emit("sub word [100], byte 0xFF");
            run();
            Assert.Equal(4001, ReadWord(0, 100));
        }

        [Fact]
        public void Xor_Ev_Ib()
        {
            WriteWord(0, 100, 0x6000);
            emit("xor word [100], byte 0xFF");
            run();
            Assert.Equal(0x9FFF, ReadWord(0, 100));
        }

        [Fact]
        public void Cmp_Ev_Ib()
        {
            WriteWord(0, 100, 4000);
            FlagC = true;
            FlagZ = true;
            emit("cmp word [100], byte 0xFE");
            run();
            Assert.Equal(4000, ReadWord(0, 100));
            Assert.False(FlagZ);
            Assert.True(FlagC);
        }

        [Fact]
        public void Test_Eb_Gb()
        {
            MMU.WriteByte(0, 100, 0x60);
            al = 0x20;
            emit("test byte [100], al");
            run();
            Assert.Equal(0x60, MMU.ReadByte(0, 100));
            Assert.False(FlagZ);
        }

        [Fact]
        public void Test_Ev_Gv()
        {
            WriteWord(0, 100, 0x6000);
            ax = 0x2000;
            emit("test word [100], ax");
            run();
            Assert.Equal(0x6000, ReadWord(0, 100));
            Assert.False(FlagZ);
        }

        [Fact]
        public void Xchg_Eb_Gb()
        {
            MMU.WriteByte(0, 100, 0x60);
            al = 0x20;
            emit("xchg byte [100], al");
            run();
            Assert.Equal(0x20, MMU.ReadByte(0, 100));
            Assert.Equal(0x60, al);
        }

        [Fact]
        public void Xchg_Ev_Gv()
        {
            WriteWord(0, 100, 0x6000);
            ax = 0x2000;
            emit("xchg word [100], ax");
            run();
            Assert.Equal(0x2000, ReadWord(0, 100));
            Assert.Equal(0x6000, ax);
        }


    }
}
