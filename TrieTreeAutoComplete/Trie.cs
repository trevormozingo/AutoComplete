/* Trevor Mozingo
 * Trie Tree 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace TrieTreeAutoComplete
{
    public class Trie
    {
        Node _root;

        public Trie()
        {
            _root = new Node('/');
            StreamReader data = new StreamReader("wordsEn.txt");
            while (!data.EndOfStream)                                      /* for all of the words in the text file */
            {
                addString(data.ReadLine());                                /* add each word to the tree */
            }
        }

        private void addString(Node curr, string toAdd)
        {
            if (toAdd == "")                                                /* if you have reached the end of a word */
            {
                foreach (Node branch in curr._childChr)                     /* check to see if the end of this word already exists */
                {
                    if (branch._parentChr == '\0')                          /* if the end is already marked, then return */
                    {
                        return;
                    }
                }
                curr._childChr.Add(new Node('\0'));                         /* otherwise it is a new word, so mark the end */
                return;                                                     /* return */
            }
            foreach (Node branch in curr._childChr)                         /* if you are in the middle of the word, match it with already existing chars in the tree */
            {
                if (branch._parentChr == toAdd[0])                          /* if match the word chars with tree chars, continue to traverse and match */
                {
                    addString(branch, toAdd.Substring(1));
                    return;
                }
            }
            curr._childChr.Add(new Node(toAdd[0]));                         /* if the tree does not yet contain this ordering, add it to the tree */
            addString(curr._childChr[curr._childChr.Count - 1], toAdd.Substring(1)); /* continue to add the word chars to the tree */
        }

        private void addString(string toAdd)                                /* starting at the root of the tree */
        {
            addString(_root, toAdd);
        }

        private void stringSearch(Node curr, string prefix, List<string> suggestions)   /* given the prefix, recursively get all suffixes and add the word to the suggestions list */
        {
            if (curr._parentChr == '\0')                                                /* if you have reached the end of a word end, return */
            {
                return;
            }
            string suggestion = prefix;                                                 /* add the prefix to the new suggestion word you will add */
            suggestion += curr._parentChr.ToString();                                   /* add the current suffix character to the suggestion word */
            foreach (Node branch in curr._childChr)                                     /* now for all child suffixes, add them to the prefix, and add to suggestions */
            {
                if (branch._parentChr == '\0')                                          /* if the current position is the end of a word, add to suggestions */
                {
                    suggestions.Add(suggestion);
                }
            }
            foreach (Node branch in curr._childChr)                                     /* given prefix, get all the child suffixes, and add each word suggestions */
            {
                stringSearch(branch, suggestion, suggestions);
            }
        }

        public Node stringSearch(Node curr, string prefix)                              /* given the user input, search for the suffix search start point */
        {
            if (prefix == "" && curr == _root)                                          /* if user entered nothing */
            {
                return null;
            }
            if (prefix == "")                                                           /* if you found the start point of the suffix (end of prefix), then return */
            {
                return curr;
            }
            foreach (Node branch in curr._childChr)                                     /* match the prefix path in the tree to find suffix start */
            {
                if (branch._parentChr == prefix[0])
                {
                    return stringSearch(branch, prefix.Substring(1));                   /* recursively traverse */
                }
            }
            return null;                                                                /* if no word, return null */
        }

        public List<string> stringSearch(string prefix)
        {
            List<string> suggestions = new List<string>();                              /* to store all suggestions */
            Node start = stringSearch(_root, prefix);                                   /* first find the suffix start point */
            if (start == null)                                                          /* if no word, return empty list */
            {
                return suggestions;
            }
            stringSearch(start, prefix.Substring(0, prefix.Length - 1), suggestions);   /* otherwise, get all suggestions (prefix + all suffixes) */
            return suggestions;
        }
    }
    
    public class Node
    {
        public char _parentChr;             /* hold the char data */
        public List<Node> _childChr;        /* all child characters stored in list */

        public Node() { }

        public Node(char toAdd)             /* trivial constructor */
        {
            _parentChr = toAdd;
            _childChr = new List<Node>();
        }
    }
}
