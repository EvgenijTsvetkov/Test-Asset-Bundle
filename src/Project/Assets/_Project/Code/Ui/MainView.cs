using Cysharp.Threading.Tasks;
using Project.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Ui
{
    public class MainView : SimpleUiElement
    {
        [Header("Texts")]
        [SerializeField] private TMP_Text _counterText;
        [SerializeField] private TMP_Text _helloText;
        
        [Header("Buttons")]
        [SerializeField] private Button _incrementCounterButton;
        [SerializeField] private Button _updateContentButton;
       
        private UiProvider _uiProvider;
        private RemoteConfig _remoteConfig;

        private int _counter;

        private void OnDestroy()
        {
            UnsubscribeOnEvents();
        }
        
        public void SetDependency(UiProvider uiProvider, RemoteConfig remoteConfig)
        {
            _uiProvider = uiProvider;
            _remoteConfig = remoteConfig;
            
            SubscribeOnEvents();
        }

        private void OnUpdateConfigsHandler()
        {
            _counter = _remoteConfig.Settings.startingNumber;

            UpdateCounterText();
            UpdateHelloText();
        }

        private void OnClickIncrementButtonHandler()
        {
            _counter += 1;
            UpdateCounterText();
        }

        private void UpdateCounterText()
        {
            _counterText.text = $"{_counter}";
        }

        private void UpdateHelloText()
        {
            _helloText.text = _remoteConfig.Descriptions.helloText;
        }
        
        private void OnClickUpdateContentButtonHandler()
        {
            UpdateContentAsync();
        }

        private async UniTaskVoid UpdateContentAsync()
        {
            var delayTime = .5f;
            
            _uiProvider.LoadingView.SetDuration(delayTime);
            _uiProvider.LoadingView.Show();
            
            await _remoteConfig.UpdateConfigs();
            await UniTask.WaitForSeconds(delayTime);
            
            _uiProvider.LoadingView.Hide();
        }
        
        private void SubscribeOnEvents()
        {
            _remoteConfig.OnUpdateConfigs += OnUpdateConfigsHandler;
            
            _incrementCounterButton.onClick.AddListener(OnClickIncrementButtonHandler);
            _updateContentButton.onClick.AddListener(OnClickUpdateContentButtonHandler);
        }

        private void UnsubscribeOnEvents()
        {
            _remoteConfig.OnUpdateConfigs -= OnUpdateConfigsHandler;
            
            _incrementCounterButton.onClick.RemoveListener(OnClickIncrementButtonHandler);
            _updateContentButton.onClick.RemoveListener(OnClickUpdateContentButtonHandler); 
        }
    }
}