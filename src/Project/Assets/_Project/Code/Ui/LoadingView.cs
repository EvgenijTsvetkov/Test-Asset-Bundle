using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Ui
{
    public class LoadingView : SimpleUiElement
    {
        [SerializeField] private Image _fillArea;
         
        private float _duration;

        private CancellationTokenSource _tokenSource;
            
        public void SetDuration(float value)
        {
            _duration = value;
        }
            
        public override void Show()
        {
            base.Show();

            UpdateTimerAsync();
        }

        public override void Hide()
        {
            base.Hide();
                
            _tokenSource?.TryCancellationToken();
        }

        private async UniTaskVoid UpdateTimerAsync()
        {
            _tokenSource = new CancellationTokenSource();
                
            SetFillArea(0f);
                
            await UniTask.DelayFrame(1, cancellationToken: _tokenSource.Token);
                
            var starTime = Time.time;
            var time = 0f;
            while (time < _duration)
            {
                await UniTask.DelayFrame(1, cancellationToken: _tokenSource.Token);

                time = Time.time - starTime;
                SetFillArea(time / _duration);
            }
        }

        private void SetFillArea(float value)
        {
            _fillArea.fillAmount = value;
        }
    }
}