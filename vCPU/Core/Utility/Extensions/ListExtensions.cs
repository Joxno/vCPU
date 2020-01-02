using System.Collections.Generic;

namespace Core.Utility.Extensions
{
    public static class ListExtensions
    {
        public static Stack<T> ToStack<T>(this List<T> List)
        {
            var t_Stack = new Stack<T>();
            for(int i = List.Count - 1; i >= 0; --i)
                t_Stack.Push(List[i]);
            return t_Stack;
        }
    }
}