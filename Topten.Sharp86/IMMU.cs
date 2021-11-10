using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topten.Sharp86
{
    /// <summary>
    /// Memory management unit
    /// </summary>
    public interface IMMU
    {
        /// <summary>
        /// Read a byte
        /// </summary>
        /// <param name="seg">Segment</param>
        /// <param name="ofs">Offset</param>
        /// <returns>The read value</returns>
        public abstract byte ReadByte(ushort seg, ushort ofs);

        /// <summary>
        /// Write a byte
        /// </summary>
        /// <param name="seg">Segment</param>
        /// <param name="ofs">Offset</param>
        /// <param value="value">The value to write</param>
        public abstract void WriteByte(ushort seg, ushort ofs, byte value);

        /// <summary>
        /// Read a word
        /// </summary>
        /// <param name="seg">Segment</param>
        /// <param name="ofs">Offset</param>
        /// <returns>The read value</returns>
        public ushort ReadWord(ushort seg, ushort ofs)
        {
            return (ushort)(ReadByte(seg, ofs) | ReadByte(seg, (ushort)(ofs + 1)) << 8);
        }

        /// <summary>
        /// Write a word
        /// </summary>
        /// <param name="seg">Segment</param>
        /// <param name="ofs">Offset</param>
        /// <param value="value">The value to write</param>
        public void WriteWord(ushort seg, ushort ofs, ushort value)
        {
            WriteByte(seg, ofs, (byte)(value & 0xFF));
            WriteByte(seg, (ushort)(ofs + 1), (byte)((value >> 8) & 0xFF));
        }

        /// <summary>
        /// Read a dword
        /// </summary>
        /// <param name="seg">Segment</param>
        /// <param name="ofs">Offset</param>
        /// <returns>The read value</returns>
        public uint ReadDWord(ushort seg, ushort ofs)
        {
            return (uint)(
                ReadByte(seg, ofs) |
                ReadByte(seg, (ushort)(ofs + 1)) << 8 | 
                ReadByte(seg, (ushort)(ofs + 2)) << 16 | 
                ReadByte(seg, (ushort)(ofs + 3)) << 24
                );
        }

        /// <summary>
        /// Write a dword
        /// </summary>
        /// <param name="seg">Segment</param>
        /// <param name="ofs">Offset</param>
        /// <param value="value">The value to write</param>
        public void WriteWord(ushort seg, ushort ofs, uint value)
        {
            WriteByte(seg, ofs, (byte)(value & 0xFF));
            WriteByte(seg, (ushort)(ofs + 1), (byte)((value >> 8) & 0xFF));
            WriteByte(seg, (ushort)(ofs + 2), (byte)((value >> 16) & 0xFF));
            WriteByte(seg, (ushort)(ofs + 3), (byte)((value >> 24) & 0xFF));
        }
    }
}
