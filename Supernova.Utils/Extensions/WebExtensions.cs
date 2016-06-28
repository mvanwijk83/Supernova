using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Supernova.Utils.Extensions
{
    public static class WebExtensions
    {
        /// <summary>
        /// Writes a HTML linebreak to the HTTP response stream.
        /// </summary>
        /// <param name="response">The current HTTP response stream.</param>
        public static void WriteLine(this HttpResponse response)
        {
            response.Write("<br>");
        }

        /// <summary>
        /// Writes the given string to the HTTP response stream, followed by a HTML linebreak.
        /// </summary>
        /// <param name="response">The current HTTP response stream.</param>
        /// <param name="s">The System.String to write.</param>
        public static void WriteLine(this HttpResponse response, string s)
        {
            response.Write(s + "<br>");
        }

        /// <summary>
        /// Renders the given control to HTML and returns it as a string.
        /// </summary>
        /// <param name="control">The current control.</param>
        /// <returns>The HTML output of the current control.</returns>
        public static string RenderHtml(this Control control)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htmlWriter = new HtmlTextWriter(sw);
            control.RenderControl(htmlWriter);
            return sb.ToString();
        }

        /// <summary>
        /// Searches the server control and its descendants for a child control with the given ID.
        /// </summary>
        /// <param name="control">The current server control.</param>
        /// <param name="id">The ID of the child contorol to search.</param>
        /// <returns>The child control matching the given ID, or null if no matching control could be found.</returns>
        public static Control FindControlRecursive(this Control control, string id)
        {
            Control root = control;
            LinkedList<Control> controls = new LinkedList<Control>();

            while (root != null)
            {
                if (root.ID == id) return root;

                foreach (Control child in root.Controls)
                {
                    if (child.ID == id) return child;
                    if (child.HasControls()) controls.AddLast(child);
                }

                root = controls.First();
                controls.Remove(root);
            }

            return null;
        }

        /// <summary>
        /// Attempts to select the ListControl item corresponding to the given string value, or the first item if no matching item is present.
        /// </summary>
        /// <param name="control">The current list control deriving from System.Web.UI.WebControls.ListControl.</param>
        /// <param name="value">The value to select.</param>
        public static void TrySelect(this ListControl control, string value)
        {
            try
            {
                control.SelectedValue = value;
            }
            catch
            {
                if (control.Items.Count > 0) control.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Searches the server control and its descendants for all controls of a specific type.
        /// </summary>
        /// <typeparam name="T">The control type to search for.</typeparam>
        /// <param name="parent">The current server control.</param>
        /// <returns>A generic list containing all matching child controls.</returns>
        public static List<T> FindControlByType<T>(this Control parent) where T : Control
        {
            List<T> found = new List<T>();
            FindControlByType<T>(parent, ref found);
            return found;
        }

        private static void FindControlByType<T>(Control parent, ref List<T> collection) where T : Control
        {
            foreach (Control control in parent.Controls)
            {
                if (control.GetType() == typeof(T))
                {
                    collection.Add((T)control);
                }
                else if (control.Controls != null && control.Controls.Count > 0)
                {
                    FindControlByType<T>(control, ref collection);
                }
            }
        }

    }
}
