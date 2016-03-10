/* Trevor Mozingo
 * Trie Tree 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrieTreeAutoComplete
{
    public partial class Form1 : Form
    {
        public Trie _wordTree;
        public Form1()
        {
            _wordTree = new Trie();                                                 /* build the tree */
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();                                                 /* citing mr olds for the list box */
            List<string> suggestions = _wordTree.stringSearch(textBox1.Text);
            listBox1.Items.AddRange(suggestions.ToArray());                         /* Thanks to Mr. Old's for the magical lightning speed list box */ 
        }
    }
}
