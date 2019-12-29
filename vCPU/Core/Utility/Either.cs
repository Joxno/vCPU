using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utility
{
    public class Either<T>
    {
        private readonly Exception m_Error = null;
        public Exception Error =>
            m_Error ?? 
            throw new Exception("Either does not contain an Error");

        private readonly T m_Value = default;

        public T Value => !HasError() ? 
            m_Value : 
            throw new Exception("Unable to access value due to Either containing an Error");

        public Either(Exception Error)
        {
            m_Error = Error;
        }

        public Either(T Value)
        {
            m_Value = Value;
        }

        public bool HasError()
        {
            return m_Error != null;
        }

        public static implicit operator Either<T>(Exception E)
        {
            return new Either<T>(E);
        }

        public static implicit operator Either<T>(T Value)
        {
            return new Either<T>(Value);
        }
    }
}
