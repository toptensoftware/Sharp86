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
    class HistoryBuffer<T>
    {
        public HistoryBuffer(int length)
        {
            _buffer = new T[length];
        }

        T[] _buffer;
        long _pos;

        public int Count
        {
            get
            {
                if (_pos > _buffer.Length)
                    return _buffer.Length;
                else
                    return (int)_pos;
            }
        }

        public void Write(T val)
        {
            _buffer[_pos % _buffer.Length] = val;
            _pos++;
        }

        public T this[int index]
        {
            get
            {
                if (index >= Count)
                    throw new ArgumentException("Index out of range");
                if (_pos < _buffer.Length)
                    return _buffer[index];

                return _buffer[(_pos + index) % _buffer.Length];
            }
        }

        public int Capacity
        {
            get
            {
                return _buffer.Length;
            }
            set
            {
                var h = new HistoryBuffer<T>(value);
                for (int i = 0; i < Count; i++)
                {
                    h.Write(this[i]);
                }

                _pos = h._pos;
                _buffer = h._buffer;
            }
        }

        public void Clear()
        {
            _pos = 0;
        }
    }
}
