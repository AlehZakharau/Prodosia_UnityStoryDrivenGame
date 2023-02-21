using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ViewDataBase : MonoBehaviour
    {
        public List<View> views;
        public readonly Dictionary<WindowType, IView> Views = new();


        public void Init()
        {
            foreach (var view in views)
            {
                Views.TryAdd(view.WindowType, view);
            }
        }

        public T GetView<T>() where T : class
        {
            foreach (var view in views)
            {
                if (view is T getView)
                    return getView;
            }
            return null;
        }
    }
}