using UnityEngine;
using Utilities;

namespace UI
{
    public partial class View
    {
#if UNITY_EDITOR
        private void OnValidate()
        {
            Validation();
            var viewDB = FindObjectOfType<ViewDataBase>();
            if (viewDB == null)
                CreateViewDB();
            

            if(!CheckForError(viewDB))
                return;

            if(!viewDB.views.Contains(this))
                viewDB.views.Add(this);
            var result = viewDB.Views.TryAdd(WindowType, this);
            AddViewInDebugList(result, viewDB);
        }

        private void CreateViewDB()
        {
            Debug.Log("s");
            var viewDB = new GameObject("ViewDataBase");
            viewDB.AddComponent<ViewDataBase>();
        }

        private bool CheckForError(ViewDataBase viewDB)
        {
            if (WindowType == WindowType.None)
            {
                Debug.Log($"<color={C.Error}>Window type is not set, check {this}</color>");
                return false;
            }
            return true;
        }

        private void AddViewInDebugList(bool result, ViewDataBase viewDB)
        {
            var config = AssetsLoader.LoadConfig();
            if (result)
            {
                if(config.viewLog)
                    Debug.Log($"<color={C.Additional}>Add View in DB{this}</color>");
            }
        }
#endif
    }
}