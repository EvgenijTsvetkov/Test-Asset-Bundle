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
       
        [Header("Image")]
        [SerializeField] private Image _incrementBackgroundButton;
        
        private AssetProvider _assetProvider;
        private UiProvider _uiProvider;
        private RemoteConfig _remoteConfig;
        private SaveSystem _saveSystem;
        
        private int _counter;

        private const string AssetName = "BackgroundButton";

        private void OnDestroy()
        {
            SaveCounter();
            UnsubscribeOnEvents();
        }

        public void SetDependency(AssetProvider assetProvider, UiProvider uiProvider, RemoteConfig remoteConfig,
            SaveSystem saveSystem)
        {
            _assetProvider = assetProvider;
            _uiProvider = uiProvider;
            _remoteConfig = remoteConfig;
            _saveSystem = saveSystem;
            
            SubscribeOnEvents();
        }

        public override void Show()
        {
            base.Show();

            UpdateBackgroundButton();
        }
        
        public void UpdateDisplay()
        {
            _counter = _saveSystem.SaveData.Counter != -1
                ? _saveSystem.SaveData.Counter 
                : _remoteConfig.Settings.startingNumber;

            UpdateCounterText();
            UpdateHelloText();
            UpdateBackgroundButton();
        }

        private void UpdateBackgroundButton()
        {
            var background = _assetProvider.GetSprite(AssetName);
            if (background != null) 
                _incrementBackgroundButton.sprite = background;
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
            await _assetProvider.LoadBundle();
            await UniTask.WaitForSeconds(delayTime);

            UpdateDisplay();
            
            _uiProvider.LoadingView.Hide();
        }

        private void SaveCounter()
        {
            _saveSystem.SaveData.Counter = _counter;
            _saveSystem.Save();
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