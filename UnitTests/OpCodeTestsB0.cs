using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTestsB0 : CPUUnitTests
    {
        [Fact]
        public void mov_al_ib()
        {
            emit("mov al,12h");
            run();
            Assert.Equal(0x12, al);
        }

        [Fact]
        public void mov_cl_ib()
        {
            emit("mov cl,12h");
            run();
            Assert.Equal(0x12, cl);
        }

        [Fact]
        public void mov_dl_ib()
        {
            emit("mov dl,12h");
            run();
            Assert.Equal(0x12, dl);
        }

        [Fact]
        public void mov_bl_ib()
        {
            emit("mov bl,12h");
            run();
            Assert.Equal(0x12, bl);
        }

        [Fact]
        public void mov_ah_ib()
        {
            emit("mov ah,12h");
            run();
            Assert.Equal(0x12, ah);
        }

        [Fact]
        public void mov_ch_ib()
        {
            emit("mov ch,12h");
            run();
            Assert.Equal(0x12, ch);
        }

        [Fact]
        public void mov_dh_ib()
        {
            emit("mov dh,12h");
            run();
            Assert.Equal(0x12, dh);
        }

        [Fact]
        public void mov_bh_ib()
        {
            emit("mov bh,12h");
            run();
            Assert.Equal(0x12, bh);
        }

    }

}
