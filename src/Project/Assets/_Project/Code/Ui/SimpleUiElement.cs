using UnityEngine;

namespace Project.Ui
{
    public class SimpleUiElement : UiElement
    {
        [SerializeField] private GameObject _content;
        
        private bool IsActive => _content != null && _content.activeInHierarchy && _content.activeSelf;
        
        protected virtual void Awake()
        {
            if (IsActive) 
                Hide();
        }

        public override void Show()
        {
            if (_content != null)
                _content.SetActive(true);
        }

        public override void Hide()
        {
            if (_content != null)
                _content.SetActive(false);
        }
    }
}