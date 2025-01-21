using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Ui
{
    public class MainView : SimpleUiElement
    {
        [Header("Texts")]
        [SerializeField] private TMP_Text _counter;
        
        [Header("Buttons")]
        [SerializeField] private Button _incrementCounterButton;
        [SerializeField] private Button _updateContentButton;

        protected override void Awake()
        {
            base.Awake();
            
            SubscribeOnEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeOnEvents();
        }

        private void OnClickIncrementButtonHandler()
        {
            
        }

        private void OnClickUpdateContentButtonHandler()
        {
            
        }

        private void SubscribeOnEvents()
        {
            _incrementCounterButton.onClick.AddListener(OnClickIncrementButtonHandler);
            _updateContentButton.onClick.AddListener(OnClickUpdateContentButtonHandler);
        }

        private void UnsubscribeOnEvents()
        {
            _incrementCounterButton.onClick.RemoveListener(OnClickIncrementButtonHandler);
            _updateContentButton.onClick.RemoveListener(OnClickUpdateContentButtonHandler); 
        }
    }
}