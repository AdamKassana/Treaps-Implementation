/* 
 
  This file contains the constructors for both the TreapNode object and the Treap which
  consists of several treapnodes. */

using System;
using System.Collections.Generic;
using System.Linq;

namespace COIS3020Assignment2
{
    //Define our interface for IContainer<T>
    //-------------------------------------------
    public interface IContainer<T>
    {
        void MakeEmpty();
        bool IsEmpty();
        int Size();
    }

    //-------------------------------------------
    //Define our Interface for ISearchable<T> which inherits IContainer<T>
    public interface ISearchable<T> : IContainer<T>
    {
        //Part I------------------
        bool Insert(T item, bool print);    // Insert element into Treap, given element
        bool Delete(T item, bool print);    // Remove element into Treap, given element
        bool Search(T item);                // Check if Item exists in Treap, given element

        //Part II-----------------
        //Treap<T> Split(T item); // Splits a treap in two, Does not like generics.
        //Treap<T> Merge(Treap<T> lTreap, Treap<T> rTreap);     // merge Treap to another, Also doesn't like generics
        bool RangeQuery(T MinRange, T MaxRange, bool print, T item); // Search the Treap for an element within Min and Max range. Or just print the nodes between the two ranges.
    }

    //-------------------------------------------

    //Define our TreapNode<T> object class, which will contain all properties necessary for each
    //node that will make up our treap. This includes the Random priority and values.
    public class TreapNode<T> where T : IComparable
    {
        //Randomly generates priority upon constructor invokation
        private static Random random = new Random();

        // Read/Write properties
        public T Item { get; set; }
        //Define getters and setters for our priority, note that the priority should not be negative.
        public int Priority { get { return priority; } set { if (value >= 0) priority = value; } }
        private int priority;
        public TreapNode<T> left;
        public TreapNode<T> right;

        //Create our TreapNode constrcutor, this will set the values of the treap node to that of
        //the given key and randomly assign a priority.
        public TreapNode(T Key)
        {
            this.Item = Key;
            //Generate any possible non-negative integer.
            Priority = random.Next(0, int.MaxValue);
            //Set both left and right to null.
            left = right = null;

        }

        //For insertion when using Split and Merge
        //Treaps must be rebalanced, it's not enough to have them split off into separate ones or just merge into one. 
        
        //Define a second constructor which allows the user to provide a priority value,
        //thereby avoiding the random number.
        public TreapNode(T Key, int priority)
        {
            this.Item = Key;
            //This method allows the user to specify the priority.
            Priority = priority;
            left = right = null;
        }
    }


    //Define the Treap<T> class, this will consist of TreapNodes to construct the full data structure.
    class Treap<T> : ISearchable<T> where T : IComparable
    {
        // The Root Data member: Holds the root of the Treap.
        private TreapNode<T> Root;

        // The MakeEmpty Method: Creates an empty Treap

        // Time Complexity: O(1)
        public void MakeEmpty()
        {
            Root = null;
        }

        // The IsEmpty Method: Returns true if the Treap is empty; false otherwise
        // Time Complexity: O(1)
        public bool IsEmpty()
        {
            return Root == null;
        }

        // The Default Constructor: Constructs an empty treap.

        // Time Complexity: O(1)
        public Treap()
        {
            MakeEmpty();
        }

        // The LeftRotate Method: Performs a left rotation around the given root

        // Time complexity: O(1)
        private TreapNode<T> LeftRotate(TreapNode<T> root)
        {
            TreapNode<T> temp = root.right; // Make a Temporary Node holding the right child of the root.
            root.right = temp.left; // Then make the right child of the root equal to the Temp's left child.
            temp.left = root; // Finally, make the temp's left child as the new root,
            return temp; // Before returning the temporary node.
        }

        // The RightRotate Method: Performs a right rotation around the given root

        // Time complexity: O(1)
        private TreapNode<T> RightRotate(TreapNode<T> root)
        {
            TreapNode<T> temp = root.left; // Make a Temporary Node holding the left child of the root.
            root.left = temp.right; // Then make the left child of the root equal to the Temp's right child.
            temp.right = root; // Finally, make the temp's right child as the new root,
            return temp; // Before returning the temporary node.
        }

        // The Public InsertP Method: A variation of Insert that has a manual declaration of priority instead of a random one.
        // Returns true on sucessful insert.

        //Due to the recursive nature of this method,
        // Time Complexity: O(log(n))
        public bool InsertP(T item, int priority, bool print = true)
        {
            // Transfer the insertion into the private recursive method.
            Root = InsertP(item, priority, Root);
            // Then, if the insert was successful, then return true.
            if (this.Search(item) == true)
            {
                Console.WriteLine(item.ToString() + " has been inserted successfully.");
                return true;
            }
            // Otherwise, print an error message (if the user desires) and returns false.
            if (print)
                Program.PrintInColour(string.Format("Could not insert the item {0} with the priority {1}", item.ToString(), priority));
            return false;
        }

        // The Recursive InsertP Method: Inserts with manual priority and recursively balances the treap.
        // Returns the current root on sucessful insert.

        //Once again, due to this methods recursive nature,
        // Time Complexity: O(log(n))
        private TreapNode<T> InsertP(T item, int priority, TreapNode<T> node)
        {
            // Firstly, make a comparison integer.
            int cmp;

            // Then, check if the current node is null, and if it is, then return the now inserted node.
            if (node == null)
                return new TreapNode<T>(item, priority);
            else
            {
                // Otherwise, compare the specified item with the current node and store it in the comparison integer.
                cmp = item.CompareTo(node.Item);
                if (cmp > 0) // If the current item is lesser than the specified, move right and balance with LeftRotate.
                {
                    node.right = InsertP(item, priority, node.right);
                    if (node.right.Priority > node.Priority) node = LeftRotate(node);
                }
                else if (cmp < 0) // If the current item is greater than the specified, move left and balance with Right Rotate.
                {
                    node.left = InsertP(item, priority, node.left);
                    if (node.left.Priority > node.Priority) node = RightRotate(node);
                }
                // If the current item is equal to the specified then do nothing.

                // Once all of the movements have been done, return the current node (root at the end)
                return node;
            }
        }

        // The Public Insert Method: Inserts a new node with random priority.
        // Returns true on sucessful insert.

        // Due to the potential recursive call of the insert method,
        // Time Complexity: O(log(n))
        public bool Insert(T item, bool print = true)
        {
            // Firstly, check if the has been already inputted, and if it is, then print a message, if the user wants to, and return false.
            if (this.Search(item))
            {
                if (print)
                    Program.PrintInColour(item.ToString() + " already exists. Cannot insert duplicate keys");
                return false;
            }
            // Otherwise, Transfer the insertion into the private recursive method.
            else Root = Insert(item, Root);
            // Then print and return true.
            Console.WriteLine(item.ToString() + " has been inserted successfully.");
            return true;
        }

        // The Recursive Insert Method: Inserts a new node with random priority.
        // Returns the current root on sucessful insert.

        // This method requires itself to be recursively called,
        // Time Complexity: O(log(n))
        private TreapNode<T> Insert(T item, TreapNode<T> node)
        {
            // Firstly, make a comparison integer.
            int cmp;

            // Then, check if the current node is null, and if it is, then return the now inserted node.
            if (node == null)
                return new TreapNode<T>(item);
            else
            {
                // Otherwise, compare the specified item with the current node and store it in the comparison integer.
                cmp = item.CompareTo(node.Item);
                if (cmp > 0) // If the current item is lesser than the specified, move right and balance with LeftRotate.
                {
                    node.right = Insert(item, node.right);
                    if (node.right.Priority > node.Priority) node = LeftRotate(node);
                }
                else if (cmp < 0) // If the current item is greater than the specified, move left and balance with Right Rotate.
                {
                    node.left = Insert(item, node.left);
                    if (node.left.Priority > node.Priority) node = RightRotate(node);
                }
                // If the current item is equal to the specified then do nothing.

                // Once all of the movements have been done, return the current node (root at the end)
                return node;
            }
        }

        // The Public Delete Method: Removes the given item from the Treap, returns boolean if
        // deltion was successful.

        // Once again, method is recursively called
        // Time complexity: O(log n)
        public bool Delete(T item, bool print = true)
        {
            // Firstly, check if there is anything in the treap at all.
            if (Root == null)
            {
                // If the treap is empty, the return true and print if the user wants to.
                if (print)
                    Program.PrintInColour("Could not delete the item " + item.ToString() + " because there is no root.");
                return false;
            }
            // Then, check if the item is in the treap in the first place,
            if (this.Search(item) == false)
            {
                // If it's not, the return true and print if the user wants to.
                if (print)
                    Console.WriteLine("Could not delete the item " + item.ToString() + " because it is not in the treap.");
                return false;
            }
            Root = Delete(item, Root); // If there is nothing, then call the Private Remove to carry out the actual removal
            // Then, if the delete was successful, then return true.
            if (this.Search(item) == false)
            {
                Console.WriteLine(item.ToString() + " has been deleted successfully.");
                return true;
            }
            // Otherwise, print an error message (if the user desires) and returns false.
            else if (print)
                Program.PrintInColour("Could not delete the item " + item.ToString() + ".");
            return false;
        }

        // The Recursive Delete Method: Removes the given item from the Treap and returns the adjusted TreapNode

        // Method is obviously recursively called
        // Time Complexity: O(log(n))
        private TreapNode<T> Delete(T item, TreapNode<T> node)
        {
            // Firstly, make a comparison integer.
            int cmp;

            // Then, try to find the node that was specified.
            // If the current node is null, and if it is, then return null (This means that the node wasn't found).
            if (node == null)
            {
                Console.WriteLine("Could not delete the item " + item.ToString());
                return null;
            }
            else
            {
                // Otherwise, compare the specified item with the current node and store it in the comparison integer.
                cmp = item.CompareTo(node.Item);
                if (cmp < 0) node.left = Delete(item, node.left);        // If the current item is greater than the specified, move left.
                else if (cmp > 0) node.right = Delete(item, node.right); // If the current item is lesser than the specified, move right.
                else if (cmp == 0)                                       // If the current item is equal to the specified, then that means that the item is in the Treap.
                {
                    // This also means that it can be removed.
                    // If the chosen node has two children, then Rotate the child with the higher priority to the given node
                    if (node.left != null && node.right != null)
                    {
                        if (node.left.Priority > node.right.Priority)
                            node = RightRotate(node);
                        else
                            node = LeftRotate(node);
                    }
                    // If the chosen node has only a left child, then Rotate the left child to the given node
                    else if (node.left != null) node = RightRotate(node);
                    // If the chosen node has Rotate the right child to the given node
                    else if (node.right != null) node = LeftRotate(node);
                    // If the chosen node is a leaf, then Snip off the item
                    else return null;

                    // Then, Recursively move item down the Treap
                    node = Delete(item, node);
                }
                // Once the deletion has taken place, then return the node of the Treap.
                return node;
            }
        }

        // The In-Order Traversal Search Method: Returns true if the item is found.

        // There is a while loop through all nodes, however; we gradually work our way down
        // the treap by comparing the value, the time complexity is
        // Time Complexity: O(log(n))
        public bool Search(T item)
        {
            // Get the root and save it in a TreapNode named curr
            TreapNode<T> curr = Root;

            // Then loop through the Treap until curr is null
            while (curr != null)
            {
                if (item.CompareTo(curr.Item) == 0) return true;         // If curr is the item that was to be found, then return true.
                else                                                     // Otherwise, 
                    if (item.CompareTo(curr.Item) < 0) curr = curr.left; // Move left if the current item is less than the one specified,
                    else curr = curr.right;                              // and Move right if the current item is greater than the one specified.
            }
            // If the item can't be found, then return false.
            return false;
        }


        // Search Utility for Split Method
        // Searches for breakoff point in treap and returns it to scope of Split method
        // Removes breakoff node and all subsequent nodes from Source Treap

        // Despite having a while loop, we once again divide and search our way down the tree,
        // Time Complexity: O(log(n))
        public TreapNode<T> SplitSearchUtil(T item)
        {
            TreapNode<T> curr = Root;
            TreapNode<T> breakOff = null;

            // While the current node isn't null, compare the child items of the current with then chosen item.
            while (curr != null)
            {
                // If Found at Left Child 
                if (item.CompareTo(curr.left.Item) == 0)
                {
                    breakOff = curr.left; // Left child of current is assigned to node used as breakoff point 
                    curr.left = null; // Then make the left child null
                    break;
                }
                // If Found at Right Child
                else if (item.CompareTo(curr.right.Item) == 0)
                {
                    breakOff = curr.right; // Right child of current is assigned to node used as breakoff point 
                    curr.right = null; // Then make the eight child null
                    break;
                }
                // Otherwise, either go left or right depending if the chosen item is less than or greater than the current item
                else if (item.CompareTo(curr.Item) < 0)
                {
                    curr = curr.left;
                }
                else
                    curr = curr.right;
            }
            // Then return the break off node.
            return breakOff;

        }
        // The Storage Method: Uses in-order traversal
        // Treaps must be balanced and fit requirements even when splitting off. 
        // One broken off this will store the <value, priority> pairs
        // This will later be used to create the new Treap returned in Split() and Merge()
        // It will do this first creating a new Treap and iteratively adding the key,value pairs using the already defined insert method
        // e.g. Treap B = A.Split(42);

        // Because we iterate through all nodes within the treap to add to the dictionary,
        // Time Complexity: O(n)
        private void Storage(ref Dictionary<T, int> dict, TreapNode<T> current)
        {
            if (current != null)
            {
                //store key,value pair

                Storage(ref dict, current.left);
                dict.Add(current.Item, current.Priority);
                Storage(ref dict, current.right);

            }

        }

        // The Split Method: Splits a Treap into two separate Treaps

        // Even though we have two foreach loops, they are not nested so we can simplify O(2n) to O(n)
        // Time Complexity: O(n)
        public Treap<T> Split(T item)
        {
            // Creates two new treaps, one for the source treap to be splitted, and another to have after the split is complete.
            Treap<T> Destination = new Treap<T>();
            Treap<T> Source = new Treap<T>();

            // Check if Treap invoking the method has items
            if (!this.IsEmpty())
            {
                // If it does have items, then
                // Check if entered item is same as root key
                // If not: proceed; Else: return null
                if (Root.Item.CompareTo(item) != 0)
                {

                    if (this.Search(item)) //item found exists within Treap 
                    {
                        //Displace node for split point into breakOff
                        TreapNode<T> breakOff = this.SplitSearchUtil(item);

                        // Dictionaries for storing treap information
                        Dictionary<T, int> SourceTreapList = new Dictionary<T, int>();
                        Dictionary<T, int> DestTreaplist = new Dictionary<T, int>();

                        //Pass both dictionaries by reference and store Treap <key, value> pairs 
                        Storage(ref DestTreaplist, breakOff);
                        Storage(ref SourceTreapList, Root);


                        foreach (KeyValuePair<T, int> entry in DestTreaplist)
                        {
                            Destination.InsertP(entry.Key, entry.Value);
                        }
                        
                        foreach (KeyValuePair<T, int> entry in SourceTreapList)
                        {
                            Source.InsertP(entry.Key, entry.Value);
                        }
                        this.MakeEmpty();
                        this.Root = Source.Root;
                    }
                    else // Item is not found within Treap
                    {
                        Console.WriteLine(item.ToString() + " does not exist in current Treap");
                    }
                }
                else
                {
                    Console.WriteLine(item.ToString() + ": cannot enter key value same as root of source Treap");
                }
            }
            else // If it doesn't return a message and null.
            {
                Console.WriteLine("Treap is empty");
            }

            return Destination;
        }

        // Merge Method: Merges two Treaps together and outputs a new one.
        // Thanks to https://www.geeksforgeeks.org/merge-two-balanced-binary-search-trees/
        //      for the help with understanding how merge works.

        //This method could likely be optimized by more strategically using the Dictionary data structures
        //thereby eliminating the need for 3 of them, however; there might also be better objects entirely
        //to use instead of a dictionary for this task.

        // Time Complexity: O(m), where m is the amount of nodes in both treaps.
        public Treap<T> Merge(Treap<T> lTreap, Treap<T> rTreap)
        {
            // Makes three Dictionaries, one each for the left and right treap, and then one for the merged treap.
            Dictionary<T, int> lDiction = new Dictionary<T, int>();
            Dictionary<T, int> rDiction = new Dictionary<T, int>();
            Dictionary<T, int> mDiction = new Dictionary<T, int>();

            // Then fill up the left and right dictionaries,
            Storage(ref lDiction, lTreap.Root);
            Storage(ref rDiction, rTreap.Root);

            // Make a couple of items, a compare integer, and an indexer for both dictionaries, 
            T lKey = default(T);
            T rKey = default(T);
            int cmp;
            int lIn = 0;
            int rIn = 0;
            // And then loop through both dictionaries, doing the following:
            for (int i = 0; i < lDiction.Count + rDiction.Count; i++)
            {
                // Get the current items for both the left and right Treap if their indecies are below the amounts in the treap.
                if (lIn < lDiction.Count) 
                { 
                    lKey = lDiction.ElementAt(lIn).Key;
                }
                else
                {
                    // If the left has already inputted all of their nodes, then input the right inside the merge dictionary, add to the right indexer and continue.
                    rKey = rDiction.ElementAt(rIn).Key;
                    mDiction.Add(rKey, rDiction.ElementAt(rIn).Value);
                    rIn++;
                    continue;
                }
                if (rIn < rDiction.Count) 
                { 
                    rKey = rDiction.ElementAt(rIn).Key; 
                }
                else
                {
                    // If the right has already inputted all of their nodes, then input the left inside the merge dictionary and add to the left indexer and continue.
                    lKey = lDiction.ElementAt(lIn).Key;
                    mDiction.Add(lKey, lDiction.ElementAt(lIn).Value);
                    lIn++;
                    continue;
                }
                // Then compare them using CompareTo and store the result in the compare integer.
                cmp = lKey.CompareTo(rKey);
                // Then check what CompareTo gave:
                if (cmp < 0)
                {
                    // If the left is less than the right, then input the left inside the merge dictionary and add to the left indexer.
                    mDiction.Add(lKey, lDiction.ElementAt(lIn).Value);
                    lIn++;
                }
                else if (cmp > 0)
                {
                    // If the left is more than the right, then input the right inside the merge dictionary and add to the right indexer.
                    mDiction.Add(rKey, rDiction.ElementAt(rIn).Value);
                    rIn++;
                }
                else
                {
                    // If the left is equal to the right, then return an error message.
                    Console.WriteLine("Collision Error: Two key were the exact same.");
                    return null;
                }
            }

            // Once all of the values from both treaps are added, then make a new Treap,
            Treap<T> nTreap = new Treap<T>();
            // Insert all of the values with a fixed priority,
            for(int i = 0; i < mDiction.Count; i++)
            {
                nTreap.InsertP(mDiction.ElementAt(i).Key, mDiction.ElementAt(i).Value);
            }
            // And return the resulting new Treap.
            return nTreap;
        }

        // RangeQuery Method, this will essentially perform In-Order Traversal,
        // and will begin inclusively printing when reaching the minrange, and stop at the maxrange.
        // It will also take an item and search for the item within the tree and return true if it is found.

        // This could probably be optimized by limiting the checks beyond MaxRange, as there is no more
        // need to print or traverse elements once we have reached the MaxRange node.

        // We once again have multiple foreach loops,
        // but the most we will iterate over will be all elements, therefore
        // Time Complexity: O(n)
        public bool RangeQuery(T MinRange, T MaxRange, bool print = false, T item = default(T))
        {
            // Create a dictionary to store all values
            Dictionary<T, int> allvalues = new Dictionary<T, int>();
            // Store all values from our Treap in the Dictionary.
            Storage(ref allvalues, Root);
            // Extract all keys and convert to a list for easy sorting.
            List<T> keys = allvalues.Keys.ToList();
            // Create a second list of only values which are within our range.
            List<T> inRange = new List<T>();
            // Sort all values, is this cheating? We don't think so.
            inRange.Sort();
            // Foreach key, check that it is within our range.
            foreach (T key in keys)
            {
                if (key.CompareTo(MinRange) >= 0 && key.CompareTo(MaxRange) <= 0)
                {
                    // Add our key to the list of in range keys.
                    inRange.Add(key);
                }
            }
            // Check if the user wants us to print keys.
            if (print)
            {
                foreach (T key in inRange)
                {
                    //For every key print it's value.
                    Console.WriteLine(key.ToString());
                }
            }
            // Check if the user has specified a key or if it is simply the default and return true
            // to show the method succeeded.
            if (item.Equals(default(T)))
                return true;
            // Return true if the value being searched for is within our range.
            return inRange.Contains(item);
        }

        // Returns the quantity of nodes within the treap.

        // All this does is store all elements in the dictionary and return the count.
        // Since we iterate through every element once, 
        // Time Complexity: O(n)
        public int Size()
        {
            Dictionary<T, int> dict = new Dictionary<T, int>();
            Storage(ref dict, Root);

            return dict.Count; //Returns the count of all nodes.
            
        }
    }
}
