// Sharp86 - 8086 Emulator
// Copyright © 2017-2021 Topten Software. All Rights Reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may 
// not use this product except in compliance with the License. You may obtain 
// a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT 
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
// License for the specific language governing permissions and limitations 
// under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConFrames;

namespace Topten.Sharp86
{
    public class RegistersWindow : Window
    {
        public RegistersWindow(TextGuiDebugger debugger) 
            : base("Registers", new Rect(100, 0, 20, 18))
        {
            _debugger = debugger;
        }

        TextGuiDebugger _debugger;

        void WriteReg(PaintContext ctx, string name, ushort value, ushort prevValue)
        {
            if (value != prevValue)
                ctx.ForegroundColor = ConsoleColor.White;
            else
                ctx.ForegroundColor = ConsoleColor.Gray;

            ctx.Write("{0}: {1:X4}", name, value);
        }

        void WriteFlag(PaintContext ctx, ushort value, ushort prevValue, ushort mask, string nameOn, string nameOff)
        {
            if ((value & mask) != (prevValue & mask))
                ctx.ForegroundColor = ConsoleColor.White;
            else
                ctx.ForegroundColor = ConsoleColor.Gray;

            if ((value & mask) != 0)
                ctx.Write(nameOn);
            else
                ctx.Write(nameOff);
        }

        public override void OnPaint(PaintContext ctx)
        {
            var cpu = _debugger.CPU;
            ctx.Write("        ");            WriteReg(ctx, " AX", cpu.ax, _ax); ctx.WriteLine();
            ctx.Write("        ");            WriteReg(ctx, " BX", cpu.bx, _bx); ctx.WriteLine();
            ctx.Write("        ");            WriteReg(ctx, " CX", cpu.cx, _cx); ctx.WriteLine();
            ctx.Write("        ");            WriteReg(ctx, " DX", cpu.dx, _dx); ctx.WriteLine();
            WriteReg(ctx, "DS", cpu.ds, _ds); WriteReg(ctx, " SI", cpu.si, _si); ctx.WriteLine();
            WriteReg(ctx, "ES", cpu.es, _es); WriteReg(ctx, " DI", cpu.di, _di); ctx.WriteLine();
            WriteReg(ctx, "SS", cpu.ss, _ss); WriteReg(ctx, " SP", cpu.sp, _sp); ctx.WriteLine();
            ctx.Write("        ");            WriteReg(ctx, " BP", cpu.bp, _bp); ctx.WriteLine();
            WriteReg(ctx, "CS", cpu.cs, _cs); WriteReg(ctx, " IP", cpu.ip, _ip); ctx.WriteLine();
            WriteReg(ctx, "FL", cpu.EFlags, _EFlags);

            ctx.WriteLine();
            ctx.WriteLine();

            WriteFlag(ctx, cpu.EFlags, _EFlags, (ushort)EFlag.ZF, "Z  ", "NZ ");
            WriteFlag(ctx, cpu.EFlags, _EFlags, (ushort)EFlag.CF, "C  ", "NC ");
            WriteFlag(ctx, cpu.EFlags, _EFlags, (ushort)EFlag.OF, "O  ", "NO ");
            WriteFlag(ctx, cpu.EFlags, _EFlags, (ushort)EFlag.SF, "S  ", "NS ");
            WriteFlag(ctx, cpu.EFlags, _EFlags, (ushort)EFlag.DF, "UP ", "DN");
            ctx.WriteLine();
            ctx.WriteLine();

            ctx.WriteLine("CT: {0}", cpu.CpuTime);
            ctx.WriteLine("DT: {0}", cpu.CpuTime - _cpuTime);
        }

        public void OnResume()
        {
            var cpu = _debugger.CPU;
            _ax = cpu.ax; 
            _bx = cpu.bx; 
            _cx = cpu.cx; 
            _dx = cpu.dx; 
            _si = cpu.si; 
            _di = cpu.di; 
            _bp = cpu.bp; 
            _sp = cpu.sp; 
            _ds = cpu.ds; 
            _es = cpu.es; 
            _ss = cpu.ss; 
            _cs = cpu.cs; 
            _ip = cpu.ip;
            _EFlags = cpu.EFlags;
            _cpuTime = cpu.CpuTime;
        }


        ushort _ax;
        ushort _bx;
        ushort _cx;
        ushort _dx;
        ushort _si;
        ushort _di;
        ushort _bp;
        ushort _sp;
        ushort _ds;
        ushort _es;
        ushort _ss;
        ushort _cs;
        ushort _ip;
        ushort _EFlags;
        ulong _cpuTime;
    }
}
