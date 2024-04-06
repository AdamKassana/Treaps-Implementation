
using System;

namespace COIS3020Assignment2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Case 1
            Treap<int> treapI = new Treap<int>();
            Console.WriteLine("If the code made it here, then that means that the Int Treap has been created.");
            Treap<char> treapC = new Treap<char>();
            Console.WriteLine("If the code made it here, then that means that the Char Treap has been created.");
            Console.ReadLine();

            // Cases 2-5
            treapI.Delete(1);
            Console.ReadLine();
            Console.WriteLine("Is 1 in treapI? " + treapI.Search(1));
            Console.ReadLine();
            Console.WriteLine("Range Query on treapI: Find numbers between 1 and 2\n" + treapI.RangeQuery(1, 2, true));
            Console.ReadLine();
            treapI.Split(1);
            Console.ReadLine();

            // Case 6
            Treap<int> treapOne = new Treap<int>();
            Treap<int> treapM = treapOne.Merge(treapOne, treapI);
            Console.WriteLine("Is the merged treap empty? " + treapM.IsEmpty());
            Console.ReadLine();

            // Case 7:Treap<int> treapIV = treapOne.Merge(treapI, treapC);
            // Gives an error before running, which is what it should do.

            // Cases 8-11 with fixed priorities
            treapI.InsertP(420, 70);
            Console.ReadLine();
            treapI.InsertP(69, 50);
            Console.ReadLine();
            treapI.InsertP(960, 25);
            Console.ReadLine();
            treapI.InsertP(256, 90);
            Console.ReadLine();

            // Cases 8-11 without fixed priorities
            treapOne.Insert(420);
            treapOne.Insert(69);
            treapOne.Insert(960);
            treapOne.Insert(256);
            treapOne.Insert(360);
            treapOne.Insert(96);
            treapOne.Insert(48);
            treapOne.Insert(1024);

            // Case 12:treapI.Insert("words");
            // Gives an error before running, which is what it should do.

            // Adding more values to treapI
            treapI.InsertP(360, 60);
            treapI.InsertP(96, 45);
            treapI.InsertP(48, 40);

            // Cases 13-18
            treapI.Delete(69);
            Console.ReadLine();
            treapI.Delete(420);
            Console.ReadLine();
            treapI.Delete(256);
            Console.ReadLine();
            treapI.Delete(960);
            Console.ReadLine();
            treapI.Delete(999);
            Console.ReadLine();
            treapI.Delete(960);
            Console.ReadLine();

            // Case 19:treapI.Delete("thisfunction");
            // Gives an error before running, which is what it should do.

            // Adding back in most of the deleted nodes
            treapI.InsertP(420, 70);
            treapI.InsertP(69, 50);
            treapI.InsertP(256, 90);

            // Cases 20-25
            Console.WriteLine("Is 69 in treapI? " + treapI.Search(69));
            Console.ReadLine();
            Console.WriteLine("Is 420 in treapI? " + treapI.Search(420));
            Console.ReadLine();
            Console.WriteLine("Is 256 in treapI? " + treapI.Search(256));
            Console.ReadLine();
            Console.WriteLine("Is 48 in treapI? " + treapI.Search(48));
            Console.ReadLine();
            Console.WriteLine("Is 0 in treapI? " + treapI.Search(0));
            Console.ReadLine();
            Console.WriteLine("Is 960 in treapI? " + treapI.Search(960));
            Console.ReadLine();

            // Case 26:Console.WriteLine("Is words in treapI? " + treapI.Search("forthe7seas"));
            // Gives an error before running, which is what it should do.

            // Case 27-32
            Treap<int> case27 = treapI.Split(96);
            Console.WriteLine("Is the treap \'case27\' empty? " + case27.IsEmpty());
            Console.WriteLine("Is the treap \'treapI\' empty? " + treapI.IsEmpty());
            Console.WriteLine("Is 256 in case27? " + case27.Search(256));
            Console.WriteLine("Is 256 in treapI? " + treapI.Search(256));
            Console.ReadLine();
            Treap<int> case28 = treapI.Split(420);
            Console.WriteLine("Is the treap \'case28\' empty? " + case28.IsEmpty());
            Console.WriteLine("Is the treap \'treapI\' empty? " + treapI.IsEmpty());
            Console.WriteLine("Is 256 in case28? " + case28.Search(256));
            Console.WriteLine("Is 256 in treapI? " + treapI.Search(256));
            Console.ReadLine();
            Treap<int> case29 = treapI.Split(256);
            Console.WriteLine("Is the treap \'case29\' empty? " + case29.IsEmpty());
            Console.WriteLine("Is the treap \'treapI\' empty? " + treapI.IsEmpty());
            Console.WriteLine("Is 256 in case29? " + case29.Search(256));
            Console.WriteLine("Is 256 in treapI? " + treapI.Search(256));
            Console.ReadLine();
            Treap<int> case30 = case28.Split(360);
            Console.WriteLine("Is the treap \'case30\' empty? " + case30.IsEmpty());
            Console.WriteLine("Is the treap \'case28\' empty? " + case28.IsEmpty());
            Console.WriteLine("Is 420 in case30? " + case30.Search(420));
            Console.WriteLine("Is 420 in case28? " + case28.Search(420));
            Console.ReadLine();
            Treap<int> case31 = treapI.Split(128);
            Console.WriteLine("Is the treap \'case31\' empty? " + case31.IsEmpty());
            Console.WriteLine("Is the treap \'treapI\' empty? " + treapI.IsEmpty());
            Console.ReadLine();
            Treap<int> case32 = treapI.Split(420);
            Console.WriteLine("Is the treap \'case32\' empty? " + case32.IsEmpty());
            Console.WriteLine("Is the treap \'treapI\' empty? " + treapI.IsEmpty());
            Console.ReadLine();

            // Case 33:treapI.Split("smoothoperator");
            // Gives an error before running, which is what it should do.

            // Cases 34-36
            Treap<int> case34 = case30.Merge(case30, case28);
            Console.WriteLine("Is the treap \'case34\' empty? " + case34.IsEmpty());
            Console.WriteLine("Is 420 in case34? " + case34.Search(420));
            Console.WriteLine("Is 360 in case34? " + case34.Search(360));
            Console.ReadLine();
            Treap<int> case35 = treapI.Merge(case27, case34);
            Console.WriteLine("Is the treap \'case35\' empty? " + case35.IsEmpty());
            Console.WriteLine("Is 420 in case35? " + case35.Search(420));
            Console.WriteLine("Is 96 in case35? " + case35.Search(96));
            Console.ReadLine();
            treapI.MakeEmpty(); // Making treapI empty for case 36.
            Console.WriteLine("Just did MakeEmpty on treapI.");
            Console.WriteLine("Is the treap \'treapI\' empty? " + treapI.IsEmpty());
            treapI = treapI.Merge(treapI, case35);
            Console.WriteLine("Is the treap \'treapI\' empty? " + treapI.IsEmpty());
            Console.ReadLine();

            // Case 37:Treap<int> treapIV = treapOne.Merge(treapI, treapC);
            // Gives an error before running, which is what it should do.

            // Adding back 256 and 960
            treapI.InsertP(256, 90);
            treapI.InsertP(960, 25);
            // Case 38
            Treap<int> splitTreap = treapI.Split(420);
            Console.WriteLine("Is 420 in treapI? " + treapI.Search(420));
            treapI = treapI.Merge(treapI, splitTreap);
            Console.WriteLine("Is 420 in treapI? " + treapI.Search(420));
            Console.ReadLine();

            // Case 39-41
            Console.WriteLine("Range Query on treapI: Find numbers between 96 and 360\n" + treapI.RangeQuery(96, 360, true));
            Console.ReadLine();
            Console.WriteLine("Range Query on treapI: Find numbers between 50 and 500\n" + treapI.RangeQuery(50, 500, true));
            Console.ReadLine();
            Console.WriteLine("Range Query on treapI: Find numbers between 50 and 360\n" + treapI.RangeQuery(50, 360, true));
            Console.ReadLine();

            // Case 42:Console.WriteLine("Range Query on treapI: Find numbers between fifty and three hundred\n" + treapI.RangeQuery("fifty", 300, true));
            // Gives an error before running, which is what it should do.
            Console.WriteLine("This concludes the testing of Treap.");
            Console.ReadLine();
        }




        public static void PrintInColour(string txt, ConsoleColor clr = ConsoleColor.Red)
        {
            Console.ForegroundColor = clr;
            Console.WriteLine(txt);
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
