using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTestsA0 : CPUUnitTests
    {
        [Fact]
        public void mov_al_Ob()
        {
            es = 0x0020;
            MMU.WriteByte(es, 0x1000, 0x12);
            emit("mov al,[es:0x1000]");
            run();
            Assert.Equal(0x12, al);
        }

        [Fact]
        public void mov_ax_Ob()
        {
            es = 0x0020;
            WriteWord(es, 0x1000, 0x1234);
            emit("mov ax,[es:0x1000]");
            run();
            Assert.Equal(0x1234, ax);
        }

        [Fact]
        public void mov_Ob_al()
        {
            es = 0x0020;
            al = 0x12;
            emit("mov [es:0x1000],al");
            run();
            Assert.Equal(0x12, MMU.ReadByte(es, 0x1000));
        }

        [Fact]
        public void mov_Ob_ax()
        {
            es = 0x0020;
            ax = 0x1234;
            emit("mov [es:0x1000],ax");
            run();
            Assert.Equal(0x1234, ReadWord(es, 0x1000));
        }

        [Fact]
        public void movsb()
        {
            es = 0x0020;
            ds = 0x0120;
            si = 0x100;
            di = 0x200;

            MMU.WriteByte(ds, si, 0x12);
            emit("movsb");
            run();
            Assert.Equal(0x12, MMU.ReadByte(es, (ushort)(di - 1)));
            Assert.Equal(0x101, si);
            Assert.Equal(0x201, di);
        }

        [Fact]
        public void rep_movsb()
        {
            es = 0x0020;
            ds = 0x0120;
            si = 0x100;
            di = 0x200;
            cx = 16;

            for (int i = 0; i < 16; i++)
            {
                MMU.WriteByte(ds, (ushort)(si + i), (byte)(0x10 + i));
            }

            emit("rep movsb");
            run();

            Assert.Equal(0x110, si);
            Assert.Equal(0x210, di);
            Assert.Equal(0, cx);
            for (int i = 0; i < 16; i++)
            {
                Assert.Equal(MMU.ReadByte(es, (ushort)(di - 16 + i)), (byte)(0x10 + i));
            }
        }

        [Fact]
        public void rep_movsb_cx0()
        {
            es = 0xFFFF;
            ds = 0x0120;
            si = 0x100;
            di = 0x200;
            cx = 0;

            emit("rep movsb");
            run();

            Assert.Equal(0x100, si);
            Assert.Equal(0x200, di);
            Assert.Equal(0, cx);
        }

        [Fact]
        public void rep_movsb_overwrite()
        {
            es = 0x0020;
            ds = 0x0020;
            si = 0x100;
            di = 0x101;
            cx = 15;

            for (int i = 0; i < 16; i++)
            {
                MMU.WriteByte(ds, (ushort)(si + i), (byte)(0x10 + i));
            }

            emit("rep movsb");
            run();

            Assert.Equal(0x10f, si);
            Assert.Equal(0x110, di);
            Assert.Equal(0, cx);
            for (int i = 0; i < 16; i++)
            {
                Assert.Equal(MMU.ReadByte(es, (ushort)(di - 16 + i)), (byte)(0x10));
            }
        }

        [Fact]
        public void movsw()
        {
            es = 0x0020;
            ds = 0x0120;
            si = 0x100;
            di = 0x200;

            WriteWord(ds, si, 0x1234);
            emit("movsw");
            run();
            Assert.Equal(0x1234, ReadWord(es, (ushort)(di - 2)));
            Assert.Equal(0x102, si);
            Assert.Equal(0x202, di);
        }

        [Fact]
        public void rep_movsw()
        {
            es = 0x0020;
            ds = 0x0120;
            si = 0x100;
            di = 0x200;
            cx = 16;

            for (int i = 0; i < 16; i++)
            {
                WriteWord(ds, (ushort)(si + i * 2), (ushort)(0x1234 + i));
            }

            emit("rep movsw");
            run();

            Assert.Equal(0x120, si);
            Assert.Equal(0x220, di);
            Assert.Equal(0, cx);
            for (int i = 0; i < 16; i++)
            {
                Assert.Equal(ReadWord(es, (ushort)(di - 32 + i * 2)), (ushort)(0x1234 + i));
            }
        }

        [Fact]
        public void rep_movsw_cx0()
        {
            es = 0xFFFF;
            ds = 0x0120;
            si = 0x100;
            di = 0x200;
            cx = 0;

            emit("rep movsw");
            run();

            Assert.Equal(0x100, si);
            Assert.Equal(0x200, di);
            Assert.Equal(0, cx);
        }

        [Fact]
        public void rep_movsb_d()
        {
            es = 0x0020;
            ds = 0x0120;
            si = 0x100;
            di = 0x200;
            cx = 16;
            FlagD = true;

            for (int i = 0; i < 16; i++)
            {
                MMU.WriteByte(ds, (ushort)(si - i), (byte)(0x10 + i));
            }

            emit("rep movsb");
            run();

            Assert.Equal(0xF0, si);
            Assert.Equal(0x1F0, di);
            Assert.Equal(0, cx);
            for (int i = 0; i < 16; i++)
            {
                Assert.Equal(MMU.ReadByte(es, (ushort)(di + 16 - i)), (byte)(0x10 + i));
            }
        }

        [Fact]
        public void rep_movsw_d()
        {
            es = 0x0020;
            ds = 0x0120;
            si = 0x100;
            di = 0x200;
            cx = 16;
            FlagD = true;

            for (int i = 0; i < 16; i++)
            {
                WriteWord(ds, (ushort)(si - i *2), (ushort)(0x1234 + i));
            }

            emit("rep movsw");
            run();

            Assert.Equal(0xE0, si);
            Assert.Equal(0x1E0, di);
            Assert.Equal(0, cx);
            for (int i = 0; i < 16; i++)
            {
                Assert.Equal(ReadWord(es, (ushort)(di + 32 - i * 2)), (ushort)(0x1234 + i));
            }
        }


        [Fact]
        public void cmpsb()
        {
            es = 0x0020;
            ds = 0x0120;
            si = 0x100;
            di = 0x200;

            MMU.WriteByte(ds, si, 0x12);
            MMU.WriteByte(es, di, 0x56);
            emit("cmpsb");
            run();
            Assert.Equal(0x101, si);
            Assert.Equal(0x201, di);
            Assert.False(FlagZ);
            Assert.True(FlagC);
        }

        [Fact]
        public void rep_cmpsb()
        {
            es = 0x0020;
            ds = 0x0120;
            si = 0x100;
            di = 0x200;
            cx = 16;

            for (int i = 0; i < 16; i++)
            {
                MMU.WriteByte(ds, (ushort)(si + i), (byte)(0x10 + i));
                MMU.WriteByte(es, (ushort)(di + i), (byte)(0x10 + i));
            }

            MMU.WriteByte(es, (ushort)(di + 5), (byte)(0xFF));

            emit("repe cmpsb");
            run();

            Assert.Equal(0x106, si);
            Assert.Equal(0x206, di);
            Assert.Equal(10, cx);
            Assert.False(FlagZ);
        }

        [Fact]
        public void rep_cmpsb_cx0()
        {
            es = 0xFFFF;
            ds = 0x0120;
            si = 0x100;
            di = 0x200;
            cx = 0;

            emit("repe cmpsb");
            run();

            Assert.Equal(0x100, si);
            Assert.Equal(0x200, di);
            Assert.Equal(0, cx);
        }

        [Fact]
        public void rep_cmpsw()
        {
            es = 0x0020;
            ds = 0x0120;
            si = 0x100;
            di = 0x200;
            cx = 16;

            for (int i = 0; i < 16; i++)
            {
                WriteWord(ds, (ushort)(si + i * 2), (ushort)(0x1234 + i));
                WriteWord(es, (ushort)(di + i * 2), (ushort)(0x1234 + i));
            }

            WriteWord(es, (ushort)(di + 5 * 2), (ushort)(0xFFFF));

            emit("repe cmpsw");
            run();

            Assert.Equal(0x10c, si);
            Assert.Equal(0x20c, di);
            Assert.Equal(10, cx);
            Assert.False(FlagZ);
        }

        [Fact]
        public void rep_cmpsw_cx0()
        {
            es = 0xFFFF;
            ds = 0x0120;
            si = 0x100;
            di = 0x200;
            cx = 0;

            emit("repe cmpsw");
            run();

            Assert.Equal(0x100, si);
            Assert.Equal(0x200, di);
            Assert.Equal(0, cx);
        }

    }

}
