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

namespace Topten.Sharp86
{
    class MemoryChangeBreakPoint : BaseMemoryBreakPoint, IBreakPointMemWrite
    {
        public MemoryChangeBreakPoint()
        {
        }

        public MemoryChangeBreakPoint(ushort segment, ushort offset, ushort length) : 
            base(segment, offset, length)
        {
        }

        bool _tripped;

        public override bool ShouldBreak(DebuggerCore debugger)
        {
            bool retv = _tripped;
            _tripped = false;
            return retv;
        }

        public override string ToString()
        {
            return base.ToString(string.Format("mem 0x{0:X4}:{1:X4} - 0x{2:X4}:{3:X4} ({4} bytes)",
                Segment, Offset,
                Segment, Offset + Length,
                Length
                ));

        }

        void IBreakPointMemWrite.WriteByte(ushort seg, ushort offset, byte oldValue, byte newValue)
        {
            if (oldValue != newValue)
            {
                if (seg == Segment && offset >= Offset && offset < Offset + Length)
                {
                    _tripped = true;
                }
            }
        }

        public override string EditString
        {
            get
            {
                return string.Format("mem 0x{0:X4}:0x{1:X4},{2}", Segment, Offset, Length);
            }
        }
    }
}
