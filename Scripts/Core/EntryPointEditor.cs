using System;
using UI;

namespace Core
{
    public partial class EntryPoint
    {
        private void OnValidate()
        {
            viewDataBase = FindObjectOfType<ViewDataBase>();
        }
    }
}