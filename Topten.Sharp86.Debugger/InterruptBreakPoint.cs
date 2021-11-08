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
    public class InterruptBreakPoint : BreakPoint
    {
        public InterruptBreakPoint()
        {
        }

        public InterruptBreakPoint(byte interruptNumber)
        {
            _interruptNumber = interruptNumber;
        }

        [Json("interrupteNumber")]
        byte _interruptNumber;
        public byte InterruptNumber
        {
            get { return _interruptNumber; }
        }

        public override string EditString
        {
            get
            {
                return string.Format("int 0x{0:X2}", _interruptNumber);
            }
        }

        public override bool ShouldBreak(DebuggerCore debugger)
        {
            return false;
        }

        public override string ToString()
        {
            return base.ToString(string.Format("int 0x{0:X2}", _interruptNumber));
        }
    }
}
