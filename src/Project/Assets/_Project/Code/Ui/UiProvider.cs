using UnityEngine;

namespace Project.Ui
{
    public class UiProvider : MonoBehaviour
    {
        [SerializeField] private MainView _mainView;
        [SerializeField] private LoadingView _loadingView;

        public MainView MainView => _mainView;
        public LoadingView LoadingView => _loadingView;
    }
}