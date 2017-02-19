using System;
using System.Collections.Generic;

namespace delegates_sample
{
    class ActionOfTExplanation
    {
        private static void UseActionOfT_Delegate()
        {
            //Action<T> accepts a single parameter and returns no value
            //i.e. one way pipeline

            //pass an IList<string> to a handler
            Action<IList<string>> messageTarget;

            //test data
            var myList = new List<string> { "first", "second", "third" };

            //dynamically check which handler to use
            if (myList.Count > 1)
            {
                messageTarget = WriteLargeList;
            }
            else
            {
                messageTarget = Console.WriteLine;
            }

            //Invoke delegate
            messageTarget(myList);
        }

        private static void WriteLargeList(IList<string> myList)
        {
            foreach (var item in myList)
            {
                Console.WriteLine(item);
            }
        }
    }
}
