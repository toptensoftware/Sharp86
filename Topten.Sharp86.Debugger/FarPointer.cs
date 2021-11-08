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
    public struct FarPointer
    {
        public FarPointer(ushort seg, ushort offset)
        {
            value = (uint)(seg << 16 | offset);
        }

        public FarPointer(uint ptr)
        {
            value = ptr;
        }

        public uint value;

        public ushort Segment { get { return (ushort)(value >> 16); } }
        public ushort Offset { get { return (ushort)(value & 0xFFFF); } }

        public override string ToString()
        {
            return string.Format("0x{0:X4}:0x{1:X4}", value >> 16, value & 0xFFFF);
        }
    }
}
