using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresDemo
{
    public class Trie
    {
        private TrieNode? _root;

        //private const int _alphabetSize = 26;

        public Trie()
        {
            _root = new TrieNode(char.MinValue);
        }

        public void Insert(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                throw new InvalidOperationException(nameof(word));

            var node = _root;

            foreach (var ch in word)
            {
                if (!node.HasChild(ch))
                    node.Add(ch);

                node = node.Children[ch];
            }

            node.IsEndOfWord = true;
        }

        public bool LookUp(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return false;

            var node = _root;

            foreach (var ch in word)
            {
                if (node.HasChild(ch))
                    node = node.Children[ch];
                else
                    return false;
            }

            return node.IsEndOfWord;
        }

        public void Remove(string word)
        {
            if (word == null)
                throw new ArgumentNullException();

            Remove(word, _root, 0);
        }

        public List<string> AutoCompletion(string prefix)
        {
            var lastNode = GetLastNode(prefix);
            var listOfWords = new List<string>();

            if (lastNode != null)
                AutoCompletion(prefix, lastNode, listOfWords);

            return listOfWords;
        }

        private void AutoCompletion(string prefix, TrieNode node, List<string> listOfWords)
        {
            if (node.IsEndOfWord)
                listOfWords.Add(prefix);

            foreach (var child in node.Children.Values)
            {
                if (child != null)
                    AutoCompletion(prefix + child.Value.ToString(), child, listOfWords);
            }
        }

        private TrieNode? GetLastNode(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                return null;

            var trimmedPrefix = prefix.Trim();
            var node = _root;
            foreach (var ch in prefix)
            {
                if (node.HasChild(ch))
                    node = node.Children[ch];
                else
                    return null;
            }

            return node;
        }

        public void RemoveIteration(string word)
        {
            int len = word.Length;
            var node = _root;
            var path = new TrieNode[len + 1];
            path[0] = _root;

            for (int i = 0; i < len; i++)
            {
                if (node.HasChild(word[i]))
                {
                    node = node.Children[word[i]];
                    path[i + 1] = node;
                }
                else
                    return;
            }

            if (node.IsEndOfWord)
                node.IsEndOfWord = false;

            for (int i = len; i > 0; i--)
            {
                if (path[i].Children.Count == 0)
                {
                    //path[i].Children.Clear();
                    path[i - 1].Children.Remove(word[i - 1]);
                }
            }
        }

        private void Remove(string word, TrieNode? node, int index)
        {
            if (index == word.Length)
            {
                node.IsEndOfWord = false;
                return;
            }

            var ch = word[index];
            if (!node.HasChild(ch))
                return;

            var child = node.Children[ch];

            //if (child == null)
            //    return;

            Remove(word, child, index + 1);

            if (child.Children.Count == 0 && !child.IsEndOfWord)
                node.Children.Remove(ch);
        }

        private class TrieNode
        {

            internal char Value { get; set; }
            internal IDictionary<char, TrieNode?> Children { get; set; }
            internal bool IsEndOfWord { get; set; }

            internal TrieNode(char value)
            {
                Value = value;
                Children = new Dictionary<char, TrieNode?>();
                IsEndOfWord = false;
            }

            internal bool HasChild(char ch)
            {
                return Children.ContainsKey(ch);
            }

            internal void Add(char ch)
            {
                Children.Add(ch, new TrieNode(ch));
            }
        }
    }
}
