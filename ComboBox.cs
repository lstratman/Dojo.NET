using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dojo.Net
{
    public class ComboBox : DojoControl
    {
        protected int _selectedIndex = -1;

        public ComboBox()
        {
            Items = new ListItemCollection();
        }

        protected override string DojoType
        {
            get
            {
                return "dijit.form.ComboBox";
            }
        }

        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }

            set
            {
                if (_selectedIndex != -1)
                    Items[_selectedIndex].Selected = false;

                if (value != -1)
                    Items[value].Selected = true;

                _selectedIndex = value;
            }
        }

        public ListItem SelectedItem
        {
            get
            {
                return _selectedIndex == -1 ? null : Items[SelectedIndex];
            }
        }

        public string SelectedValue
        {
            get
            {
                return SelectedItem.Value;
            }

            set
            {
                SelectedIndex = Items.IndexOf(Items.FindByValue(value));
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ListItemCollection Items
        {
            get;
            set;
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Select;
            }
        }

        public virtual string Name
        {
            get;
            set;
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            if (String.IsNullOrEmpty(Name))
                writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);

            base.RenderBeginTag(writer);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (i == SelectedIndex)
                    writer.AddAttribute("selected", "selected");

                writer.AddAttribute("value", Items[i].Value ?? Items[i].Text);
                writer.RenderBeginTag(HtmlTextWriterTag.Option);
                writer.Write(Items[i].Text ?? Items[i].Value);
                writer.RenderEndTag();
            }

            base.RenderContents(writer);
        }
    }
}
