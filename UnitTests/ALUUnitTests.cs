using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Topten.Sharp86;

namespace UnitTests
{
    public class ALUUnitTests
    {
        public ALUUnitTests()
        {
            _alu = new ALU();
        }

        ALU _alu;

        uint Signed32(int s)
        {
            return unchecked((uint)s);
        }

        ushort Signed16(int s)
        {
            return unchecked((ushort)s);
        }

        byte Signed8(int s)
        {
            return unchecked((byte)s);
        }


        [Fact]
        public void alu_Add16_c()
        {
            Assert.Equal(30, _alu.Add16(10, 20));
            Assert.False(_alu.FlagC);

            Assert.Equal(0x30, _alu.Add16(0x8010, 0x8020));
            Assert.True(_alu.FlagC);
        }

        [Fact]
        public void alu_Add8_c()
        {
            Assert.Equal(30, _alu.Add8(10, 20));
            Assert.False(_alu.FlagC);

            Assert.Equal(0x20, _alu.Add8(0x90, 0x90));
            Assert.True(_alu.FlagC);
        }

        [Fact]
        public void alu_Add16_z()
        {
            Assert.Equal(30, _alu.Add16(10, 20));
            Assert.False(_alu.FlagZ);

            Assert.Equal(0, _alu.Add16(0x8000, 0x8000));
            Assert.True(_alu.FlagZ);
        }

        [Fact]
        public void alu_Add8_z()
        {
            Assert.Equal(30, _alu.Add8(10, 20));
            Assert.False(_alu.FlagZ);

            Assert.Equal(0, _alu.Add8(0x80, 0x80));
            Assert.True(_alu.FlagZ);
        }

        [Fact]
        public void alu_Add16_s()
        {
            Assert.Equal(30, _alu.Add16(10, 20));
            Assert.False(_alu.FlagS);

            Assert.Equal(0x9000, _alu.Add16(0x7000, 0x2000));
            Assert.True(_alu.FlagS);
        }

        [Fact]
        public void alu_Add8_s()
        {
            Assert.Equal(30, _alu.Add8(10, 20));
            Assert.False(_alu.FlagS);

            Assert.Equal(0x90, _alu.Add8(0x70, 0x20));
            Assert.True(_alu.FlagS);
        }

        [Fact]
        public void alu_Add16_o()
        {
            // Positive + positive
            Assert.Equal(0xE000, _alu.Add16(0x7000, 0x7000));
            Assert.True(_alu.FlagO);

            // Negative + negative
            Assert.Equal(0x7FFF, _alu.Add16(0x8000, 0xFFFF));
            Assert.True(_alu.FlagO);

            // Positive + positive
            Assert.Equal(0x7050, _alu.Add16(0x7000, 0x50));
            Assert.False(_alu.FlagO);

            // Negative + negative
            Assert.Equal(0xFFFE, _alu.Add16(0xFFFF, 0xFFFF));
            Assert.False(_alu.FlagO);

            // Negative + negative
            Assert.Equal(0x8000, _alu.Add16(0x8001, 0xFFFF));
            Assert.False(_alu.FlagO);
        }

        [Fact]
        public void alu_Add8_o()
        {
            // Positive + positive
            Assert.Equal(0xE0, _alu.Add8(0x70, 0x70));
            Assert.True(_alu.FlagO);

            // Negative + negative
            Assert.Equal(0x7F, _alu.Add8(0x80, 0xFF));
            Assert.True(_alu.FlagO);

            // Positive + positive
            Assert.Equal(0x75, _alu.Add8(0x70, 0x5));
            Assert.False(_alu.FlagO);

            // Negative + negative
            Assert.Equal(0xFE, _alu.Add8(0xFF, 0xFF));
            Assert.False(_alu.FlagO);

            // Negative + negative
            Assert.Equal(0x80, _alu.Add8(0x81, 0xFF));
            Assert.False(_alu.FlagO);
        }

        [Fact]
        public void alu_Sub16_c()
        {
            Assert.Equal(5, _alu.Sub16(10, 5));
            Assert.False(_alu.FlagC);

            Assert.Equal(0x10000 + 5 - 10, _alu.Sub16(5, 10));
            Assert.True(_alu.FlagC);
        }

        [Fact]
        public void alu_Sub16_o()
        {
            Assert.Equal(5, _alu.Sub16(10, 5));
            Assert.False(_alu.FlagO);

            Assert.Equal(Signed16(-15), _alu.Sub16(10, 25));
            Assert.False(_alu.FlagO);

            Assert.Equal(32767, _alu.Sub16(32766, Signed16(-1)));
            Assert.False(_alu.FlagO);

            Assert.Equal(Signed16(-32768), _alu.Sub16(Signed16(-32767), 1));
            Assert.False(_alu.FlagO);

            Assert.Equal(32768, _alu.Sub16(32766, Signed16(-2)));
            Assert.True(_alu.FlagO);

            Assert.Equal(Signed16(-32769), _alu.Sub16(Signed16(-32767), 2));
            Assert.True(_alu.FlagO);
        }

        [Fact]
        public void alu_Sub8_o()
        {
            Assert.Equal(5, _alu.Sub8(10, 5));
            Assert.False(_alu.FlagO);

            Assert.Equal(Signed8(-15), _alu.Sub8(10, 25));
            Assert.False(_alu.FlagO);

            Assert.Equal(127, _alu.Sub8(126, Signed8(-1)));
            Assert.False(_alu.FlagO);

            Assert.Equal(Signed8(-128), _alu.Sub8(Signed8(-127), 1));
            Assert.False(_alu.FlagO);

            Assert.Equal(128, _alu.Sub8(126, Signed8(-2)));
            Assert.True(_alu.FlagO);

            Assert.Equal(Signed8(-129), _alu.Sub8(Signed8(-127), 2));
            Assert.True(_alu.FlagO);
        }

        [Fact]
        public void alu_Inc16_z()
        {
            Assert.Equal(0, _alu.Inc16(0xFFFF));
            Assert.True(_alu.FlagZ);

            Assert.Equal(1, _alu.Inc16(0));
            Assert.False(_alu.FlagZ);
        }

        [Fact]
        public void alu_Inc16_c()
        {
            _alu.FlagC = true;
            Assert.Equal(0, _alu.Inc16(0xFFFF));
            Assert.True(_alu.FlagC);

            _alu.FlagC = false;
            Assert.Equal(0, _alu.Inc16(0xFFFF));
            Assert.False(_alu.FlagC);
        }

        [Fact]
        public void alu_Dec16_z()
        {
            Assert.Equal(0, _alu.Dec16(1));
            Assert.True(_alu.FlagZ);

            Assert.Equal(1, _alu.Dec16(2));
            Assert.False(_alu.FlagZ);
        }

        [Fact]
        public void alu_Dec16_c()
        {
            _alu.FlagC = true;
            Assert.Equal(0xFFFF, _alu.Dec16(0));
            Assert.True(_alu.FlagC);

            _alu.FlagC = false;
            Assert.Equal(0xFFFF, _alu.Dec16(0));
            Assert.False(_alu.FlagC);
        }


        [Fact]
        public void alu_Inc8_z()
        {
            Assert.Equal(0, _alu.Inc8(0xFF));
            Assert.True(_alu.FlagZ);

            Assert.Equal(1, _alu.Inc8(0));
            Assert.False(_alu.FlagZ);
        }

        [Fact]
        public void alu_Inc8_c()
        {
            _alu.FlagC = true;
            Assert.Equal(0, _alu.Inc8(0xFF));
            Assert.True(_alu.FlagC);

            _alu.FlagC = false;
            Assert.Equal(0, _alu.Inc8(0xFF));
            Assert.False(_alu.FlagC);
        }

        [Fact]
        public void alu_Dec8_z()
        {
            Assert.Equal(0, _alu.Dec8(1));
            Assert.True(_alu.FlagZ);

            Assert.Equal(1, _alu.Dec8(2));
            Assert.False(_alu.FlagZ);
        }

        [Fact]
        public void alu_Dec8_c()
        {
            _alu.FlagC = true;
            Assert.Equal(0xFF, _alu.Dec8(0));
            Assert.True(_alu.FlagC);

            _alu.FlagC = false;
            Assert.Equal(0xFF, _alu.Dec8(0));
            Assert.False(_alu.FlagC);
        }

        [Fact]
        public void alu_Rol16()
        {
            Assert.Equal(2, _alu.Rol16(0x8000, 2));
            Assert.False(_alu.FlagC);

            Assert.Equal(1, _alu.Rol16(0x8000, 1));
            Assert.True(_alu.FlagC);
        }

        [Fact]
        public void alu_Ror16()
        {
            Assert.Equal(0x4000, _alu.Ror16(1, 2));
            Assert.False(_alu.FlagC);

            Assert.Equal(0x8000, _alu.Ror16(1, 1));
            Assert.True(_alu.FlagC);
        }

        [Fact]
        public void alu_Rcl16()
        {
            _alu.FlagC = false;
            Assert.Equal(1 << 2, _alu.Rcl16(1, 2));
            Assert.False(_alu.FlagC);

            Assert.Equal(0, _alu.Rcl16(0x8000, 1));
            Assert.True(_alu.FlagC);

            _alu.FlagC = false;
            Assert.Equal(1, _alu.Rcl16(0x8000, 2));
            Assert.False(_alu.FlagC);

            _alu.FlagC = true;
            Assert.Equal(1, _alu.Rcl16(0, 1));
            Assert.False(_alu.FlagC);
        }

        [Fact]
        public void alu_Rcr16()
        {
            _alu.FlagC = false;
            Assert.Equal(0x8000, _alu.Rcr16(1, 2));
            Assert.False(_alu.FlagC);

            Assert.Equal(0, _alu.Rcr16(1, 1));
            Assert.True(_alu.FlagC);

            Assert.Equal(0x8000, _alu.Rcr16(0, 1));
            Assert.False(_alu.FlagC);
        }

        [Fact]
        public void alu_Rcl8()
        {
            _alu.FlagC = false;
            Assert.Equal(_alu.Rcl8(1, 2), 1 << 2);
            Assert.False(_alu.FlagC);

            Assert.Equal(0, _alu.Rcl8(0x80, 1));
            Assert.True(_alu.FlagC);

            _alu.FlagC = false;
            Assert.Equal(1, _alu.Rcl8(0x80, 2));
            Assert.False(_alu.FlagC);

            _alu.FlagC = true;
            Assert.Equal(1, _alu.Rcl8(0, 1));
            Assert.False(_alu.FlagC);
        }

        [Fact]
        public void alu_Rcr8()
        {
            _alu.FlagC = false;
            Assert.Equal(0x80, _alu.Rcr8(1, 2));
            Assert.False(_alu.FlagC);

            Assert.Equal(0, _alu.Rcr8(1, 1));
            Assert.True(_alu.FlagC);

            Assert.Equal(0x80, _alu.Rcr8(0, 1));
            Assert.False(_alu.FlagC);
        }

        [Fact]
        public void alu_Shl16()
        {
            Assert.Equal(_alu.Shl16(1, 2), 1 << 2);
            Assert.False(_alu.FlagC);
            Assert.False(_alu.FlagZ);

            Assert.Equal(0, _alu.Shl16(0x4000, 2));
            Assert.True(_alu.FlagC);
            Assert.True(_alu.FlagZ);
        }

        [Fact]
        public void alu_Shr16()
        {
            Assert.Equal(1 << 1, _alu.Shr16(1 << 5, 4));
            Assert.False(_alu.FlagC);
            Assert.False(_alu.FlagZ);
        }

        [Fact]
        public void alu_Sar16()
        {
            Assert.Equal(_alu.Sar16(Signed16(-40), 1), Signed16(-20));
        }

        [Fact]
        public void alu_Aaa()
        {
            _alu.FlagA = false;
            Assert.Equal(0x0201, _alu.Aaa(0x0201));
            Assert.False(_alu.FlagA);
            Assert.False(_alu.FlagC);

            _alu.FlagA = true;
            Assert.Equal(0x0307, _alu.Aaa(0x0201));
            Assert.True(_alu.FlagA);
            Assert.True(_alu.FlagC);

            _alu.FlagA = false;
            Assert.Equal(0x301, _alu.Aaa(0x020b));
            Assert.True(_alu.FlagA);
            Assert.True(_alu.FlagC);
        }

        [Fact]
        public void alu_Aad()
        {
            Assert.Equal(23, _alu.Aad(0x0203, 10));
            Assert.False(_alu.FlagS);
        }

        [Fact]
        public void alu_Cbw()
        {
            Assert.Equal(0xFFFF, _alu.Cbw(0xFF));
            Assert.Equal(0x000F, _alu.Cbw(0x0F));
        }

        [Fact]
        public void alu_Cwd()
        {
            Assert.Equal(0xFFFFFFFFU, _alu.Cwd(0xFFFF));
            Assert.Equal(0x0000000FU, _alu.Cwd(0x000F));
        }

        [Fact]
        public void alu_Div16()
        {
            Assert.Equal(_alu.Div16(60005, 200), (uint)(300 | (5 << 16)));
        }

        [Fact]
        public void alu_Div8()
        {
            Assert.Equal(_alu.Div8(205, 20), 10 | 5 << 8);
        }

        [Fact]
        public void alu_IDiv16()
        {
            Assert.Equal(_alu.IDiv16(Signed32(-60005), 200), (uint)(Signed16(-300) | (Signed16(-5) << 16)));
        }

        [Fact]
        public void alu_IDiv8()
        {
            Assert.Equal(_alu.IDiv8(Signed16(-6005), 100), (uint)(Signed8(-60) | (Signed8(-5) << 8)));
        }

        [Fact]
        public void alu_Mul16()
        {
            Assert.Equal(20000U, _alu.Mul16(400, 50));
            Assert.False(_alu.FlagC);
            Assert.False(_alu.FlagO);

            Assert.Equal(2000000000U, _alu.Mul16(40000, 50000));
            Assert.True(_alu.FlagC);
            Assert.True(_alu.FlagO);
        }

        [Fact]
        public void alu_Mul8()
        {
            Assert.Equal(20U, _alu.Mul8(4, 5));
            Assert.False(_alu.FlagC);
            Assert.False(_alu.FlagO);

            Assert.Equal(16000U, _alu.Mul8(200, 80));
            Assert.True(_alu.FlagC);
            Assert.True(_alu.FlagO);
        }

        [Fact]
        public void alu_IMul16()
        {
            Assert.Equal(_alu.IMul16(Signed16(-200), Signed16(50)), Signed32(-200*50));
            Assert.False(_alu.FlagC);
            Assert.False(_alu.FlagO);

            Assert.Equal(_alu.IMul16(Signed16(-300), Signed16(500)), Signed32(-300 * 500));
            Assert.True(_alu.FlagC);
            Assert.True(_alu.FlagO);
        }

        [Fact]
        public void alu_IMul8()
        {
            Assert.Equal(_alu.IMul8(Signed8(-2), Signed8(5)), Signed16(-10));
            Assert.False(_alu.FlagC);
            Assert.False(_alu.FlagO);

            Assert.Equal(_alu.IMul8(Signed8(-3), Signed8(50)), Signed16(-150));
            Assert.True(_alu.FlagC);
            Assert.True(_alu.FlagO);
        }

        [Fact]
        public void alu_Neg16()
        {
            Assert.Equal(_alu.Neg16(Signed16(-10)), Signed16(10));
            Assert.True(_alu.FlagC);
            Assert.False(_alu.FlagZ);

            Assert.Equal(_alu.Neg16(Signed16(10)), Signed16(-10));
            Assert.True(_alu.FlagC);
            Assert.False(_alu.FlagZ);

            Assert.Equal(_alu.Neg16(Signed16(0)), Signed16(0));
            Assert.False(_alu.FlagC);
            Assert.True(_alu.FlagZ);
        }

        [Fact]
        public void alu_Neg8()
        {
            Assert.Equal(_alu.Neg8(Signed8(-10)), Signed8(10));
            Assert.True(_alu.FlagC);
            Assert.False(_alu.FlagZ);

            Assert.Equal(_alu.Neg8(Signed8(10)), Signed8(-10));
            Assert.True(_alu.FlagC);
            Assert.False(_alu.FlagZ);

            Assert.Equal(_alu.Neg8(Signed8(0)), Signed8(0));
            Assert.False(_alu.FlagC);
            Assert.True(_alu.FlagZ);
        }

        [Fact]
        public void alu_Not16()
        {
            Assert.Equal(0x5555U, _alu.Not16(0xAAAA));
        }

        [Fact]
        public void alu_Not8()
        {
            Assert.Equal(0x55U, _alu.Not8(0xAA));
        }
    }
}
