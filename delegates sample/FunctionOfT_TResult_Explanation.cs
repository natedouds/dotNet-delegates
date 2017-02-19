using System;
using System.Collections.Generic;
using System.Linq;

namespace delegates_sample
{
    class FunctionOfT_TResult_Explanation
    {
        private static void UseFuncT_TResult()
        {
            //Func<T,TResult> accepts incoming parameter(s) and return type
            //i.e. roundtrip pipeline

            //pass an IList<string> to a handler
            Func<IList<string>, int> messageTarget;

            //test data
            var myList = new List<string> { "first", "second", "third" };

            //dynamically check which handler to use
            if (myList.Count > 1)
            {
                messageTarget = WriteLargeList;
            }
            else
            {
                messageTarget = WriteSingleItemList;
            }

            //Invoke delegate
            Console.WriteLine("List Length: " + messageTarget(myList));
        }

        private static int WriteLargeList(IList<string> myList)
        {
            foreach (var item in myList)
            {
                Console.WriteLine(item);
            }
            return myList.Count;
        }

        private static int WriteSingleItemList(IList<string> myList)
        {
            Console.WriteLine(myList.FirstOrDefault());
            return 1;
        }
    }
}
