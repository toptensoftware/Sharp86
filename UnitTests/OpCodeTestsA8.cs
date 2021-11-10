using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTestsA8 : CPUUnitTests
    {
        [Fact]
        public void test_al_Ib()
        {
            al = 0x30;
            emit("test al,0x10");
            run();
            Assert.Equal(0x30, al);
            Assert.False(FlagZ);
        }

        [Fact]
        public void test_ax_Iv()
        {
            ax = 0x3000;
            emit("test ax,0x1000");
            run();
            Assert.Equal(0x3000, ax);
            Assert.False(FlagZ);
        }

        [Fact]
        public void stosb()
        {
            es = 0x0020;
            di = 0x200;
            al = 0x12;
            emit("stosb");
            run();
            Assert.Equal(0x12, MMU.ReadByte(es, (ushort)(di - 1)));
            Assert.Equal(0x201, di);
        }

        [Fact]
        public void rep_stosb()
        {
            es = 0x0020;
            di = 0x200;
            al = 0x12;
            cx = 16;

            emit("rep stosb");
            run();

            Assert.Equal(0x210, di);
            Assert.Equal(0, cx);
            for (int i = 0; i < 16; i++)
            {
                Assert.Equal(MMU.ReadByte(es, (ushort)(di - 16 + i)), al);
            }
        }

        [Fact]
        public void rep_stosb_cx0()
        {
            es = 0xFFFF;
            di = 0x200;
            al = 0x12;
            cx = 0;

            emit("rep stosb");
            run();

            Assert.Equal(0x200, di);
            Assert.Equal(0, cx);
        }


        [Fact]
        public void stosw()
        {
            es = 0x0020;
            di = 0x200;
            ax = 0x1234;
            emit("stosw");
            run();
            Assert.Equal(0x1234, ReadWord(es, (ushort)(di - 2)));
            Assert.Equal(0x202, di);
        }

        [Fact]
        public void rep_stosw()
        {
            es = 0x0020;
            di = 0x200;
            ax = 0x1234;
            cx = 16;

            emit("rep stosw");
            run();

            Assert.Equal(0x220, di);
            Assert.Equal(0, cx);
            for (int i = 0; i < 16; i++)
            {
                Assert.Equal(ReadWord(es, (ushort)(di - 32 + i*2)), ax);
            }
        }

        [Fact]
        public void rep_stosw_cx0()
        {
            es = 0xFFFF;
            di = 0x200;
            ax = 0x1234;
            cx = 0;

            emit("rep stosw");
            run();

            Assert.Equal(0x200, di);
            Assert.Equal(0, cx);
        }

        [Fact]
        public void lodsb()
        {
            ds = 0x0020;
            si = 0x200;
            MMU.WriteByte(ds, si, 0x12);
            emit("lodsb");
            run();
            Assert.Equal(0x12, al);
            Assert.Equal(0x201, si);
        }

        [Fact]
        public void rep_lodsb()
        {
            ds = 0x0020;
            si = 0x200;
            cx = 16;

            for (int i=0; i<16; i++)
            {
                MMU.WriteByte(ds, (ushort)(si + i), (byte)(0x10 + i));
            }

            emit("rep lodsb");
            run();

            Assert.Equal(0x210, si);
            Assert.Equal(0, cx);
            Assert.Equal(0x1f, al);
        }

        [Fact]
        public void rep_lodsb_cx0()
        {
            ds = 0xFFFF;
            si = 0x200;
            cx = 0;

            emit("rep lodsb");
            run();

            Assert.Equal(0x200, si);
            Assert.Equal(0, cx);
        }

        [Fact]
        public void lodsw()
        {
            ds = 0x0020;
            si = 0x200;
            WriteWord(ds, si, 0x1234);
            emit("lodsw");
            run();
            Assert.Equal(0x1234, ax);
            Assert.Equal(0x202, si);
        }

        [Fact]
        public void rep_lodsw()
        {
            ds = 0x0020;
            si = 0x200;
            cx = 16;

            for (int i = 0; i < 16; i++)
            {
                WriteWord(ds, (ushort)(si + i * 2), (ushort)(0x1234 + i));
            }

            emit("rep lodsw");
            run();

            Assert.Equal(0x220, si);
            Assert.Equal(0, cx);
            Assert.Equal(ax, 0x1234 + 15);
        }

        [Fact]
        public void rep_lodsw_cx0()
        {
            ds = 0xFFFF;
            si = 0x200;
            cx = 0;

            emit("rep lodsw");
            run();

            Assert.Equal(0x200, si);
            Assert.Equal(0, cx);
        }


        [Fact]
        public void scasb()
        {
            es = 0x0020;
            di = 0x200;
            MMU.WriteByte(es, di, 0x12);
            al = 0x11;
            emit("scasb");
            run();
            Assert.Equal(0x11, al);
            Assert.Equal(0x201, di);
            Assert.True(FlagC);
        }


        [Fact]
        public void rep_scasb()
        {
            es = 0x0020;
            di = 0x200;

            for (int i = 0; i < 16; i++)
            {
                MMU.WriteByte(es, (ushort)(di + i), 0x11);
            }

            al = 0x11;
            cx = 16;
            emit("repz scasb");
            run();
            Assert.Equal(0x11, al);
            Assert.Equal(0x210, di);
            Assert.True(FlagZ);
        }

        [Fact]
        public void rep_scasb_cx0()
        {
            es = 0xFFFF;
            di = 0x200;

            al = 0x11;
            cx = 0;
            emit("repz scasb");
            run();
            Assert.Equal(0x11, al);
            Assert.Equal(0x200, di);
        }

        [Fact]
        public void rep_scasb_2()
        {
            es = 0x0020;
            di = 0x200;

            for (int i = 0; i < 16; i++)
            {
                MMU.WriteByte(es, (ushort)(di + i), 0x11);
            }
            MMU.WriteByte(es, (ushort)(di + 2), 0x12);

            al = 0x11;
            cx = 16;
            emit("repz scasb");
            run();
            Assert.Equal(0x11, al);
            Assert.Equal(0x203, di);
            Assert.False(FlagZ);
        }

        [Fact]
        public void rep_scasb_3()
        {
            es = 0x0020;
            di = 0x200;

            for (int i = 0; i < 16; i++)
            {
                MMU.WriteByte(es, (ushort)(di + i), 0x11);
            }
            MMU.WriteByte(es, (ushort)(di + 2), 0x12);

            al = 0x12;
            cx = 16;
            emit("repnz scasb");
            run();
            Assert.Equal(0x12, al);
            Assert.Equal(0x203, di);
            Assert.True(FlagZ);
        }

    }

}
