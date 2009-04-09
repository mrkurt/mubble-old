using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;
using ActiveObjects;

namespace Mubble.UI
{
    public class Control : System.Web.UI.Control
    {
        private Mubble.UI.Page page;

        /// <summary>
        /// Gets the container page for this control
        /// </summary>
        new public Mubble.UI.Page Page
        {
            get
            {
                if (this.page == null)
                {
                    this.page = base.Page as Mubble.UI.Page;
                }
                return page;
            }
        }

        private Controller controller;

        /// <summary>
        /// Gets the content for the current request
        /// </summary>
        public Controller Controller
        {
            get
            {
                if (controller == null)
                {
                    controller = (this.Page != null) ?
                        this.Page.Controller : null;
                }
                return controller;
            }
        }

        /// <summary>
        /// Gets the parsed URL of the current request
        /// </summary>
        public MubbleUrl Url
        {
            get
            {
                if (this.Page == null)
                {
                    return null;
                }
                else
                {
                    return this.Page.Url;
                }
            }
        }

        public static string GetReferencedValue(System.Web.UI.Control control, string reference)
        {
            if (string.IsNullOrEmpty(reference) || reference[0] != '$') return reference;
            string[] refParts = reference.Split(new char[]{'$', '[', ']'}, StringSplitOptions.RemoveEmptyEntries);

            IActiveObject obj = Control.GetCurrentScope<IActiveObject>(control);

            if (obj == null) return null;

            object value = null;

            if (refParts.Length > 1)
            {
                MetaDataCollection md = obj.DataManager[refParts[0]] as MetaDataCollection;

                value = (md != null) ? md.GetFirstStringValue(refParts[1]) : null;
            }
            else if (refParts.Length == 1)
            {
                value = obj.DataManager[refParts[0]];
            }

            string svalue = value as string;

            if (value is Link)
            {
                svalue = MubbleUrl.Url((Link)value, "HtmlHandler");
            }

            return svalue;
        }

        public static List<T> FindMatchingControls<T>(T current) where T : System.Web.UI.Control
        {
            return FindMatchingControls(current, current.Page);
        }

        public static List<T> FindMatchingControls<T>(T current, System.Web.UI.Control scope) where T : System.Web.UI.Control
        {
            return FindMatchingControls(current, scope, new List<T>());
        }

        private static List<T> FindMatchingControls<T>(T current, System.Web.UI.Control level, List<T> collection) where T : System.Web.UI.Control
        {
            foreach (System.Web.UI.Control c in level.Controls)
            {
                if (c.Equals(current)) break;
                if (c is T)
                {
                    collection.Add((T)c);
                }
                else
                {
                    FindMatchingControls(current, c, collection);
                }
            }
            return collection;
        }

        public static string ResolveUrl(System.Web.UI.Control control, string url)
        {
            System.Web.UI.Control p = control;
            while (!(p is System.Web.UI.Page || p is System.Web.UI.MasterPage) && p != null)
            {
                p = p.Parent;
            }
            return p != null ? p.ResolveUrl(url) : url;
        }

        public static T FindControl<T>(System.Web.UI.Control parent) where T : class
        {
            for (int i = 0; i < parent.Controls.Count; i++)
            {
                T control = parent.Controls[i] as T;
                if (control == null)
                {
                    control = FindControl<T>(parent.Controls[i]);
                }
                if (control != null)
                {
                    return control;
                }
            }
            return null;
        }

        /// <summary>
        /// Retrieves the current scope for type T of the control parameter.  If a scope
        /// of type T is not found in the ancestor path, returns null.
        /// </summary>
        /// <typeparam name="T">The scope type to search for</typeparam>
        /// <param name="control">The initial control to search</param>
        /// <returns>The first scope of type T found in the ancestor path, null
        /// if not available.</returns>
        public static T GetCurrentScope<T>(System.Web.UI.Control control) where T : class
        {
            return GetCurrentScope<T>(control, null);
        }
        public static T GetCurrentScope<T>(System.Web.UI.Control control, string filter) where T : class
        {
            int blah = 0;
            return GetCurrentScope<T>(control, filter, ref blah);
        }
        //TODO: This should really take an array of Types, but I need to figure out an intelligent way of returning the value
        public static T GetCurrentScope<T>(System.Web.UI.Control control, string filter, ref int depth) where T : class
        {
            //IScoped scope = control as IScoped;
            depth++;
            ScopeFilter<T> f = new ScopeFilter<T>(filter);
            T match = f.FindMatch(control);
            if (match != null)
            {
                return match;
            }
            else if (control.Parent != null)
            {
                return GetCurrentScope<T>(control.Parent, filter, ref depth);
            }
            else
            {
                return null;
            }
        }
        private class ScopeFilter<T> where T : class
        {
            string filter;
            ScopeFilterType type = ScopeFilterType.None;
            public ScopeFilter(string filter)
            {
                if (filter == null)
                {
                    type = ScopeFilterType.None;
                }
                else if (filter.Length > 0 && filter[0] == '#')
                {
                    type = ScopeFilterType.ID;
                    this.filter = filter.Substring(1);
                }
                else
                {
                    type = ScopeFilterType.Type;
                    this.filter = filter;
                }
            }
            public T FindMatch(object control)
            {
                IScoped scope = control as IScoped;
                T obj = null;
                
                if (control is T)
                {
                    obj = (T)control;
                }
                else if (scope != null && scope.Scope is T)
                {
                    obj = (T)scope.Scope;
                }

                if (obj != null)
                {
                    
                    Type t = obj.GetType();
                    if (scope != null && scope.Scope != null)
                    {
                        t = scope.Scope.GetType();
                    }
                    if(this.type == ScopeFilterType.Type && !this.typeContainsFilter(t))
                    {
                        obj = null;
                    }
                    if (this.type == ScopeFilterType.ID && 
                        (!(scope is System.Web.UI.Control) || ((System.Web.UI.Control)scope).ID != filter)
                        )
                    {
                        obj = null;
                    }
                }
                return obj;
            }

            private bool typeContainsFilter(Type type)
            {
                while (type != null)
                {
                    string typename = type.Name;
                    if (typename.LastIndexOf(filter) == typename.Length - filter.Length)
                    {
                        return true;
                    }
                    foreach (Type i in type.GetInterfaces())
                    {
                        typename = type.Name;
                        if (typename.LastIndexOf(filter) == typename.Length - filter.Length)
                        {
                            return true;
                        }
                    }
                    type = type.BaseType;
                }
                return false;
            }
        }
        private enum ScopeFilterType
        {
            None = 0,
            Type = 1,
            ID = 2
        }
    }
}
