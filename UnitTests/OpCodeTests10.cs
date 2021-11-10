using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTests10 : CPUUnitTests
    {
        [Fact]
        public void Adc_Eb_Gb()
        {
            MMU.WriteByte(0, 100, 40);
            FlagC = true;
            al = 20;
            emit("adc byte [100], al");
            run();
            Assert.Equal(61, MMU.ReadByte(0, 100));
        }

        [Fact]
        public void Adc_Ev_Gv()
        {
            WriteWord(0, 100, 4000);
            FlagC = true;
            ax = 2000;
            emit("adc word [100], ax");
            run();
            Assert.Equal(6001, ReadWord(0, 100));
        }

        [Fact]
        public void Adc_Gb_Eb()
        {
            MMU.WriteByte(0, 100, 40);
            al = 20;
            FlagC = true;
            emit("adc al, byte [100]");
            run();
            Assert.Equal(61, al);
        }

        [Fact]
        public void Adc_Gv_Ev()
        {
            WriteWord(0, 100, 4000);
            ax = 2000;
            FlagC = true;
            emit("adc ax, word [100]");
            run();
            Assert.Equal(6001, ax);
        }

        [Fact]
        public void Adc_AL_Ib()
        {
            al = 40;
            FlagC = true;
            emit("adc al, 20");
            run();
            Assert.Equal(61, al);
        }

        [Fact]
        public void Adc_AX_Iv()
        {
            ax = 4000;
            FlagC = true;
            emit("adc ax, 2000");
            run();
            Assert.Equal(6001, ax);
        }

        [Fact]
        public void PUSH_SS()
        {
            sp = 0x8008;
            ss = 0x0001;
            emit("push ss");
            run();
            Assert.Equal(0x0001, ReadWord(ss, sp));
            Assert.Equal(0x8006, sp);
        }

        [Fact]
        public void POP_SS()
        {
            sp = 0x8006;
            WriteWord(ss, sp, 0x1234);
            emit("pop ss");
            run();
            Assert.Equal(0x1234, ss);
            Assert.Equal(0x8008, sp);
        }

    }
}
