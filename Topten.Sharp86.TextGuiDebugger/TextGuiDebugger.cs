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

using ConFrames;
using System;
using Topten.JsonKit;

namespace Topten.Sharp86
{
    public class TextGuiDebugger : DebuggerCore
    {
        public TextGuiDebugger()
        {
            CommandDispatcher.RegisterCommandHandler(new TextGuiDebuggerCommands(this));

            _desktop = new ConFrames.Desktop(120, 40);
            _desktop.PreviewKey = OnPreviewKey;

            _codeWindow = new CodeWindow(this);
            _codeWindow.Open(_desktop);
            _registersWindow = new RegistersWindow(this);
            _registersWindow.Open(_desktop);
            _watchWindow = new WatchWindow(this);
            _watchWindow.Open(_desktop);
            _consoleWindow = new CommandWindow(this);
            _consoleWindow.Open(_desktop);
            _memoryWindow = new MemoryWindow(this);
            _memoryWindow.Open(_desktop);

            _codeWindow.Activate();
        }

        Desktop _desktop;
        CodeWindow _codeWindow;
        RegistersWindow _registersWindow;
        ConsoleWindow _consoleWindow;
        WatchWindow _watchWindow;

        [Json("memory1", KeepInstance = true)]
        MemoryWindow _memoryWindow;

        bool OnPreviewKey(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.F10 && key.Modifiers == 0)
            {
                ExecuteCommand("o");
                return true;
            }

            if (key.Key == ConsoleKey.F8)
            {
                if (key.Modifiers == ConsoleModifiers.Shift)
                {
                    ExecuteCommand("t");
                }
                else
                {
                    ExecuteCommand("s");
                }
                return true;
            }

            if (key.Key == ConsoleKey.F5)
            {
                ExecuteCommand("r");
                return true;
            }

            return false;
        }

        protected override bool OnBreak()
        {
            _codeWindow.MoveToIP();
            _registersWindow.Invalidate();
            _memoryWindow.OnBreak();
            _watchWindow.OnBreak();

            _desktop.Process();

            _registersWindow.OnResume();
            _memoryWindow.OnResume();

            return true;
        }

        public override void WriteConsole(string output)
        {
            _consoleWindow.Write(output);
            //base.Write(output);
        }

        public override void PromptConsole(string str)
        {
            _consoleWindow.SetInputBuffer(str);
            _consoleWindow.Activate();
        }

        public void ExecuteCommand(string command)
        {
            base.CommandDispatcher.ExecuteCommand(command);
            if (base.ShouldContinue)
            {
                if (!base.IsStepping)
                {
                    _desktop.ViewMode = ViewMode.StdOut;
                    _desktop.RestoreForegroundWindow();
                }

                _desktop.EndProcessing();
            }
        }

        public CodeWindow CodeWindow
        {
            get { return _codeWindow; }
        }

        public MemoryWindow MemoryWindow
        {
            get { return _memoryWindow; }
        }
    }
}
