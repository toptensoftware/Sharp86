using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topten.Sharp86
{
    /// <summary>
    /// Implements a real-mode flat MMU
    /// </summary>
    public class MMURealMode : IMMU
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bus">The underlying physical bus</param>
        public MMURealMode()
        {
        }

        /// </summary>
        public IMemoryBus MemoryBus
        {
            get => _memoryBus;
            set => _memoryBus = value;
        }


        /// <inheritdoc />
        public byte ReadByte(ushort seg, ushort ofs)
        {
            return _memoryBus.ReadByte((uint)((seg << 4) + ofs));
        }

        /// <inheritdoc />
        public void WriteByte(ushort seg, ushort ofs, byte value)
        {
            _memoryBus.WriteByte((uint)((seg << 4) + ofs), value);
        }

        IMemoryBus _memoryBus;
    }
}
