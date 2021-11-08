using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class OpCodeTests70 : CPUUnitTests
    {
        void testJump(string str)
        {
            emit(str + " $+0x40");
            step();
        }

        bool didJump
        {
            get
            {
                return ip == 0x140;
            }
        }

        [Fact]
        public void jo_true()
        {
            FlagO = true;
            testJump("jo");
            Assert.True(didJump);
        }

        [Fact]
        public void jo_false()
        {
            FlagO = false;
            testJump("jo");
            Assert.False(didJump);
        }

        [Fact]
        public void jno_true()
        {
            FlagO = false;
            testJump("jno");
            Assert.True(didJump);
        }

        [Fact]
        public void jno_false()
        {
            FlagO = true;
            testJump("jno");
            Assert.False(didJump);
        }

        [Fact]
        public void jb_true()
        {
            FlagC = true;
            testJump("jb");
            Assert.True(didJump);
        }

        [Fact]
        public void jb_false()
        {
            FlagC = false;
            testJump("jb");
            Assert.False(didJump);
        }

        [Fact]
        public void jnb_true()
        {
            FlagC = false;
            testJump("jnb");
            Assert.True(didJump);
        }

        [Fact]
        public void jnb_false()
        {
            FlagC = true;
            testJump("jnb");
            Assert.False(didJump);
        }

        [Fact]
        public void jz_true()
        {
            FlagZ = true;
            testJump("jz");
            Assert.True(didJump);
        }

        [Fact]
        public void jz_false()
        {
            FlagZ = false;
            testJump("jz");
            Assert.False(didJump);
        }

        [Fact]
        public void jnz_true()
        {
            FlagZ = false;
            testJump("jnz");
            Assert.True(didJump);
        }

        [Fact]
        public void jnz_false()
        {
            FlagZ = true;
            testJump("jnz");
            Assert.False(didJump);
        }


        [Fact]
        public void jbe_true_1()
        {
            FlagZ = false;
            FlagC = true;
            testJump("jbe");
            Assert.True(didJump);
        }

        [Fact]
        public void jbe_true_2()
        {
            FlagZ = true;
            FlagC = false;
            testJump("jbe");
            Assert.True(didJump);
        }

        [Fact]
        public void jbe_true_3()
        {
            FlagZ = true;
            FlagC = true;
            testJump("jbe");
            Assert.True(didJump);
        }

        [Fact]
        public void jbe_false()
        {
            FlagZ = false;
            FlagC = false;
            testJump("jbe");
            Assert.False(didJump);
        }

        [Fact]
        public void ja_true()
        {
            FlagZ = false;
            FlagC = false;
            testJump("ja");
            Assert.True(didJump);
        }

        [Fact]
        public void ja_false_1()
        {
            FlagZ = false;
            FlagC = true;
            testJump("ja");
            Assert.False(didJump);
        }

        [Fact]
        public void ja_false_2()
        {
            FlagZ = true;
            FlagC = false;
            testJump("ja");
            Assert.False(didJump);
        }

        [Fact]
        public void ja_false_3()
        {
            FlagZ = true;
            FlagC = true;
            testJump("ja");
            Assert.False(didJump);
        }


        [Fact]
        public void js_false()
        {
            FlagS = false;
            testJump("js");
            Assert.False(didJump);
        }

        [Fact]
        public void jns_true()
        {
            FlagS = false;
            testJump("jns");
            Assert.True(didJump);
        }

        [Fact]
        public void jns_false()
        {
            FlagS = true;
            testJump("jns");
            Assert.False(didJump);
        }


        [Fact]
        public void jp_false()
        {
            FlagP = false;
            testJump("jp");
            Assert.False(didJump);
        }

        [Fact]
        public void jnp_true()
        {
            FlagP = false;
            testJump("jnp");
            Assert.True(didJump);
        }

        [Fact]
        public void jnp_false()
        {
            FlagP = true;
            testJump("jnp");
            Assert.False(didJump);
        }

        [Fact]
        public void jl_true_1()
        {
            FlagS = true;
            FlagO = false;
            testJump("jl");
            Assert.True(didJump);
        }

        [Fact]
        public void jl_true_2()
        {
            FlagS = false;
            FlagO = true;
            testJump("jl");
            Assert.True(didJump);
        }

        [Fact]
        public void jl_false_1()
        {
            FlagS = false;
            FlagO = false;
            testJump("jl");
            Assert.False(didJump);
        }

        [Fact]
        public void jl_false_2()
        {
            FlagS = true;
            FlagO = true;
            testJump("jl");
            Assert.False(didJump);
        }

        [Fact]
        public void jge_true_1()
        {
            FlagS = false;
            FlagO = false;
            testJump("jge");
            Assert.True(didJump);
        }

        [Fact]
        public void jge_true_2()
        {
            FlagS = false;
            FlagO = false;
            testJump("jge");
            Assert.True(didJump);
        }

        [Fact]
        public void jge_false_1()
        {
            FlagS = true;
            FlagO = false;
            testJump("jge");
            Assert.False(didJump);
        }

        [Fact]
        public void jge_false_2()
        {
            FlagS = false;
            FlagO = true;
            testJump("jge");
            Assert.False(didJump);
        }

        [Fact]
        public void jle_0()
        {
            FlagZ = false;
            FlagS = false;
            FlagO = false;
            testJump("jle");
            Assert.False(didJump);
        }

        [Fact]
        public void jle_1()
        {
            FlagZ = false;
            FlagS = false;
            FlagO = true;
            testJump("jle");
            Assert.True(didJump);
        }

        [Fact]
        public void jle_2()
        {
            FlagZ = false;
            FlagS = true;
            FlagO = false;
            testJump("jle");
            Assert.True(didJump);
        }

        [Fact]
        public void jle_3()
        {
            FlagZ = false;
            FlagS = true;
            FlagO = true;
            testJump("jle");
            Assert.False(didJump);
        }

        [Fact]
        public void jle_4()
        {
            FlagZ = true;
            FlagS = false;
            FlagO = false;
            testJump("jle");
            Assert.True(didJump);
        }

        [Fact]
        public void jle_5()
        {
            FlagZ = true;
            FlagS = false;
            FlagO = true;
            testJump("jle");
            Assert.True(didJump);
        }

        [Fact]
        public void jle_6()
        {
            FlagZ = true;
            FlagS = true;
            FlagO = false;
            testJump("jle");
            Assert.True(didJump);
        }

        [Fact]
        public void jle_7()
        {
            FlagZ = true;
            FlagS = true;
            FlagO = true;
            testJump("jle");
            Assert.True(didJump);
        }


        [Fact]
        public void jg_0()
        {
            FlagZ = false;
            FlagS = false;
            FlagO = false;
            testJump("jg");
            Assert.True(didJump);
        }

        [Fact]
        public void jg_1()
        {
            FlagZ = false;
            FlagS = false;
            FlagO = true;
            testJump("jg");
            Assert.False(didJump);
        }

        [Fact]
        public void jg_2()
        {
            FlagZ = false;
            FlagS = true;
            FlagO = false;
            testJump("jg");
            Assert.False(didJump);
        }

        [Fact]
        public void jg_3()
        {
            FlagZ = false;
            FlagS = true;
            FlagO = true;
            testJump("jg");
            Assert.True(didJump);
        }

        [Fact]
        public void jg_4()
        {
            FlagZ = true;
            FlagS = false;
            FlagO = false;
            testJump("jg");
            Assert.False(didJump);
        }

        [Fact]
        public void jg_5()
        {
            FlagZ = true;
            FlagS = false;
            FlagO = true;
            testJump("jg");
            Assert.False(didJump);
        }

        [Fact]
        public void jg_6()
        {
            FlagZ = true;
            FlagS = true;
            FlagO = false;
            testJump("jg");
            Assert.False(didJump);
        }

        [Fact]
        public void jg_7()
        {
            FlagZ = true;
            FlagS = true;
            FlagO = true;
            testJump("jg");
            Assert.False(didJump);
        }







    }
}
