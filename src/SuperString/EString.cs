using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Versioning;

namespace System
{

    public delegate string AllocateStringDelegate(int length);

    /// <summary>
    /// 扩展string类，提高字符串拼接性能
    /// </summary>
    public class EString
    {
        internal static readonly AllocateStringDelegate FastAllocateString;

        internal static int CharSize;

        private string _value;
        static EString()
        {
            CharSize = sizeof(char);
            MethodInfo fastAllocateString = typeof(string).GetMethod("FastAllocateString", BindingFlags.NonPublic | BindingFlags.Static);
            FastAllocateString = (AllocateStringDelegate)Delegate.CreateDelegate(typeof(AllocateStringDelegate), fastAllocateString);
        }

        public EString(string value)
        {
            this._value = value;
        }

        public override string ToString()
        {
            return _value;
        }

        public static implicit operator EString(string value)
        {
            return new EString(value);
        }

        public static EString operator +(EString source,string dest)
        {
            source.ContactString(dest);
            return source;
        }
        public static EString operator +(EString source, EString dest)
        {
            source.ContactString(dest._value);
            return source;
        }
        [System.Security.SecurityCritical]
        public unsafe string Append(params string[] values)
        {
            int length = 0;
            int count = values.Length;
            for (int i = 0; i < count; i += 1)
            {
                length += values[i].Length;
            }
            string joinString = FastAllocateString(length);

            fixed (char* pointerToResult = joinString)
            {
                int step = 0;
                for (int i = 0; i < count; i += 1)
                {
                    fixed (char* pointerToTempString = values[i])
                    {
                        Memmove((byte*)(pointerToResult + step), (byte*)pointerToTempString, (uint)(values[i].Length * CharSize));
                    }
                    step += values[i].Length;
                }
            }

            return ContactString(joinString);
        }

        [System.Security.SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal unsafe string ContactString(string newString)
        {
            int newLength = newString.Length;
            int oldLength = _value.Length;
            string joinString = EString.FastAllocateString(oldLength + newLength);
            fixed (char* pointerToResult = joinString)
            {
                if (oldLength != 0)
                {
                    fixed (char* pointerToOldString = _value)
                    {
                        Memmove((byte*)(pointerToResult), (byte*)pointerToOldString, (uint)(oldLength * CharSize));
                    }
                }
                fixed (char* pointerToNewString = newString)
                {
                    Memmove((byte*)(pointerToResult + oldLength), (byte*)pointerToNewString, (uint)(newLength * CharSize));
                }
            }
            _value = joinString;
            return joinString;
        }

        [System.Security.SecurityCritical]
        [ResourceExposure(ResourceScope.None)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if WIN64
        internal unsafe static void Memmove(byte* dest, byte* src, ulong len)
#else
        internal unsafe static void Memmove(byte* dest, byte* src, uint len)
#endif
        {

#if WIN64
            if ((ulong)dest - (ulong)src < len) goto PInvoke;
#else
            if (((uint)dest - (uint)src) < len) goto PInvoke;
#endif

            switch (len)
            {
                case 0:
                    return;
                case 1:
                    *dest = *src;
                    return;
                case 2:
                    *(short*)dest = *(short*)src;
                    return;
                case 3:
                    *(short*)dest = *(short*)src;
                    *(dest + 2) = *(src + 2);
                    return;
                case 4:
                    *(int*)dest = *(int*)src;
                    return;
                case 5:
                    *(int*)dest = *(int*)src;
                    *(dest + 4) = *(src + 4);
                    return;
                case 6:
                    *(int*)dest = *(int*)src;
                    *(short*)(dest + 4) = *(short*)(src + 4);
                    return;
                case 7:
                    *(int*)dest = *(int*)src;
                    *(short*)(dest + 4) = *(short*)(src + 4);
                    *(dest + 6) = *(src + 6);
                    return;
                case 8:
#if WIN64
                *(long*)dest = *(long*)src;
#else
                    *(int*)dest = *(int*)src;
                    *(int*)(dest + 4) = *(int*)(src + 4);
#endif
                    return;
                case 9:
#if WIN64
                *(long*)dest = *(long*)src;
#else
                    *(int*)dest = *(int*)src;
                    *(int*)(dest + 4) = *(int*)(src + 4);
#endif
                    *(dest + 8) = *(src + 8);
                    return;
                case 10:
#if WIN64
                *(long*)dest = *(long*)src;
#else
                    *(int*)dest = *(int*)src;
                    *(int*)(dest + 4) = *(int*)(src + 4);
#endif
                    *(short*)(dest + 8) = *(short*)(src + 8);
                    return;
                case 11:
#if WIN64
                *(long*)dest = *(long*)src;
#else
                    *(int*)dest = *(int*)src;
                    *(int*)(dest + 4) = *(int*)(src + 4);
#endif
                    *(short*)(dest + 8) = *(short*)(src + 8);
                    *(dest + 10) = *(src + 10);
                    return;
                case 12:
#if WIN64
                *(long*)dest = *(long*)src;
#else
                    *(int*)dest = *(int*)src;
                    *(int*)(dest + 4) = *(int*)(src + 4);
#endif
                    *(int*)(dest + 8) = *(int*)(src + 8);
                    return;
                case 13:
#if WIN64
                *(long*)dest = *(long*)src;
#else
                    *(int*)dest = *(int*)src;
                    *(int*)(dest + 4) = *(int*)(src + 4);
#endif
                    *(int*)(dest + 8) = *(int*)(src + 8);
                    *(dest + 12) = *(src + 12);
                    return;
                case 14:
#if WIN64
                *(long*)dest = *(long*)src;
#else
                    *(int*)dest = *(int*)src;
                    *(int*)(dest + 4) = *(int*)(src + 4);
#endif
                    *(int*)(dest + 8) = *(int*)(src + 8);
                    *(short*)(dest + 12) = *(short*)(src + 12);
                    return;
                case 15:
#if WIN64
                *(long*)dest = *(long*)src;
#else
                    *(int*)dest = *(int*)src;
                    *(int*)(dest + 4) = *(int*)(src + 4);
#endif
                    *(int*)(dest + 8) = *(int*)(src + 8);
                    *(short*)(dest + 12) = *(short*)(src + 12);
                    *(dest + 14) = *(src + 14);
                    return;
                case 16:
#if WIN64
                *(long*)dest = *(long*)src;
                *(long*)(dest + 8) = *(long*)(src + 8);
#else
                    *(int*)dest = *(int*)src;
                    *(int*)(dest + 4) = *(int*)(src + 4);
                    *(int*)(dest + 8) = *(int*)(src + 8);
                    *(int*)(dest + 12) = *(int*)(src + 12);
#endif
                    return;
                default:
                    break;
            }

            // P/Invoke into the native version for large lengths
            if (len >= 512) goto PInvoke;

            if (((int)dest & 3) != 0)
            {
                if (((int)dest & 1) != 0)
                {
                    *dest = *src;
                    src++;
                    dest++;
                    len--;
                    if (((int)dest & 2) == 0)
                        goto Aligned;
                }
                *(short*)dest = *(short*)src;
                src += 2;
                dest += 2;
                len -= 2;
                Aligned:;
            }

#if WIN64
            if (((int)dest & 4) != 0)
            {
                *(int *)dest = *(int *)src;
                src += 4;
                dest += 4;
                len -= 4;
            }
#endif

#if WIN64
            ulong count = len / 16;
#else
            uint count = len / 16;
#endif
            while (count > 0)
            {
#if WIN64
                ((long*)dest)[0] = ((long*)src)[0];
                ((long*)dest)[1] = ((long*)src)[1];
#else
                ((int*)dest)[0] = ((int*)src)[0];
                ((int*)dest)[1] = ((int*)src)[1];
                ((int*)dest)[2] = ((int*)src)[2];
                ((int*)dest)[3] = ((int*)src)[3];
#endif
                dest += 16;
                src += 16;
                count--;
            }

            if ((len & 8) != 0)
            {
#if WIN64
                ((long*)dest)[0] = ((long*)src)[0];
#else
                ((int*)dest)[0] = ((int*)src)[0];
                ((int*)dest)[1] = ((int*)src)[1];
#endif
                dest += 8;
                src += 8;
            }
            if ((len & 4) != 0)
            {
                ((int*)dest)[0] = ((int*)src)[0];
                dest += 4;
                src += 4;
            }
            if ((len & 2) != 0)
            {
                ((short*)dest)[0] = ((short*)src)[0];
                dest += 2;
                src += 2;
            }
            if ((len & 1) != 0)
                *dest = *src;

            return;

            PInvoke:
            void* ret = dest;

            if (dest <= src || dest >= (src + len))
            {
                //Non-Overlapping Buffers
                //copy from lower addresses to higher addresses

                while (len > 0)
                {
                    *dest++ = *src++;
                    len -= 1;
                }
            }
            else

            {
                //Overlapping Buffers
                //copy from higher addresses to lower addresses
                dest += len - 1;
                src += len - 1;

                while (len > 0)
                {
                    *dest-- = *src--;
                    len -= 1;
                }
            }
        }
    }
}