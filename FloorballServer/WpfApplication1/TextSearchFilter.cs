using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace WpfApplication1
{
    public class TextSearchFilter
    {
        public TextSearchFilter(ICollectionView filteredView, TextBox textBox)
        {
            string filterText = "";

            filteredView.Filter = delegate (object obj)
            {
                if (String.IsNullOrEmpty(filterText))
                    return true;

                ListItemModel model = obj as ListItemModel;

                string str = model.Name as string;

                if (String.IsNullOrEmpty(str))
                    return false;

                int index = str.IndexOf(filterText, 0, StringComparison.InvariantCultureIgnoreCase);

                return index > -1;
            };

            textBox.TextChanged += delegate
            {
                filterText = textBox.Text;
                filteredView.Refresh();
            };
        }
    }
}
