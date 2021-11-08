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
    public class WatchWindow : Window
    {
        public WatchWindow(TextGuiDebugger owner) : base("Watch", new Rect(80, 18, 40, 22))
        {
            _owner = owner;
            _owner.SettingsChanged += () => Invalidate();
        }

        public void OnBreak()
        {
            Invalidate();
        }

        public override void OnPaint(PaintContext ctx)
        {
            ctx.ForegroundColor = ConsoleColor.Gray;

            int y = 0;
            foreach (var w in _owner.WatchExpressions)
            {
                ctx.Position = new Point(0, y);
                ctx.Write(string.Format("#{0,2}: {1}", w.Number, string.IsNullOrEmpty(w.Name) ? w.ExpressionText : w.Name));

                var val = w.EvalAndFormat(_owner);
                var x = ClientSize.Width - val.Length;
                if (x < 5)
                    x = 5;
                ctx.Position = new Point(x, y);
                ctx.Write(val);
                y++;
            }
        }

        TextGuiDebugger _owner;
    }
}
