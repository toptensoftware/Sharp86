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
using Topten.JsonKit;

namespace Topten.Sharp86
{
    public class CodeBreakPoint : BreakPoint
    {
        public CodeBreakPoint()
        {
        }

        public CodeBreakPoint(ushort segment, ushort offset)
        {
            Segment = segment;
            Offset = offset;
        }

        [Json("address")]
        public uint Address
        {
            get
            {
                return (uint)(Segment << 16 | Offset);
            }
            set
            {
                Segment = (ushort)(value >> 16);
                Offset = (ushort)(value & 0xFFFF);
            }
        }

        public ushort Segment;
        public ushort Offset;

        public override bool ShouldBreak(DebuggerCore debugger)
        {
            var cpu = debugger.CPU;
            return Segment == cpu.cs && Offset == cpu.ip;
        }

        public override string ToString()
        {
            return base.ToString(string.Format("at {0:X4}:{1:X4}", Segment, Offset));
        }

        public override string EditString
        {
            get
            {
                return string.Format("0x{0:X4}:0x{1:X4}", Segment, Offset);
            }
        }
    }

}
