using System;
using UnityEngine;

namespace UI
{
    public interface IView
    {
        public event Action OnOpening;
        public void Show();
        public void Hide();
    }
    public abstract partial class View : MonoBehaviour, IView
    {
        [Header("Choose Window Type")]
        [SerializeField] public WindowType WindowType;

        public event Action OnOpening;

        public virtual void Show()
        {
            gameObject.SetActive(true);
            OnOpening?.Invoke();
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        protected virtual void Validation()
        {
            
        }
    }
}