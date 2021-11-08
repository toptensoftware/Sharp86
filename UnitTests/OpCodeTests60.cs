using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTests60 : CPUUnitTests
    {
        public OpCodeTests60()
        {
        }

        bool _boundsExceeded = false;

        [NotAFact]
        public override void RaiseInterrupt(byte interruptNumber)
        {
            if (interruptNumber==5)
            {
                _boundsExceeded = true;
                return;
            }

            base.RaiseInterrupt(interruptNumber);
        }

        [Fact]
        public void pusha_popa()
        {
            sp = 0x1000;
            ax = 1;
            bx = 2;
            cx = 3;
            dx = 4;
            bp = 5;
            si = 6;
            di = 7;

            emit("pusha");
            step();

            Assert.Equal(0x0FF0, sp);
            Assert.Equal(this.ReadWord(ss, (ushort)(sp + 0)), di);
            Assert.Equal(this.ReadWord(ss, (ushort)(sp + 2)), si);
            Assert.Equal(this.ReadWord(ss, (ushort)(sp + 4)), bp);
            Assert.Equal(0x1000, this.ReadWord(ss, (ushort)(sp + 6)));
            Assert.Equal(this.ReadWord(ss, (ushort)(sp + 8)), bx);
            Assert.Equal(this.ReadWord(ss, (ushort)(sp + 10)), dx);
            Assert.Equal(this.ReadWord(ss, (ushort)(sp + 12)), cx);
            Assert.Equal(this.ReadWord(ss, (ushort)(sp + 14)), ax);

            ax = 0;
            bx = 0;
            cx = 0;
            dx = 0;
            bp = 0;
            si = 0;
            di = 0;

            emit("popa");
            step();

            Assert.Equal(0x1000, sp);
            Assert.Equal(1, ax);
            Assert.Equal(2, bx);
            Assert.Equal(3, cx);
            Assert.Equal(4, dx);
            Assert.Equal(5, bp);
            Assert.Equal(6, si);
            Assert.Equal(7, di);
        }


        [Fact]
        public void bound_r16_m16()
        {
            di = 0x1000;
            WriteWord(ds, di, unchecked((ushort)(short)-10));
            WriteWord(ds, (ushort)(di + 2), unchecked((ushort)(short)20));

            _boundsExceeded = false;
            ax = 0;
            emit("bound ax,word [di]");
            step();
            Assert.False(_boundsExceeded);

            _boundsExceeded = false;
            ax = unchecked((ushort)(short)-11);
            emit("bound ax,word [di]");
            step();
            Assert.True(_boundsExceeded);

            _boundsExceeded = false;
            ax = unchecked((ushort)(short)-10);
            emit("bound ax,word [di]");
            step();
            Assert.False(_boundsExceeded);

            _boundsExceeded = false;
            ax = unchecked((ushort)(short)21);
            emit("bound ax,word [di]");
            step();
            Assert.True(_boundsExceeded);

            _boundsExceeded = false;
            ax = unchecked((ushort)(short)20);
            emit("bound ax,word [di]");
            step();
            Assert.False(_boundsExceeded);
        }
    }
}
