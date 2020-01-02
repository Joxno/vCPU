using System.Collections.Generic;
using System.Linq;

namespace Core.Utility.Extensions
{
    public static class StackExtensions
    {
        public static Stack<T> ToStack<T>(this Stack<T> Stack)
        {
            return Stack.ToList().ToStack();
        }
    }
}