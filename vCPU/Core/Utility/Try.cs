using System;

namespace Core.Utility
{
    public static class Try
    {
        public static Either<Unit> Call(Action F)
        {
            try
            {
                F();
                return new Unit();
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public static Either<T> Call<T>(Func<T> F)
        {
            try
            {
                return F();
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public static Either<TR> Call<TR, T1>(Func<T1, TR> F, T1 P)
        {
            try
            {
                return F(P);
            }
            catch (Exception e)
            {
                return e;
            }
        }
    }
}