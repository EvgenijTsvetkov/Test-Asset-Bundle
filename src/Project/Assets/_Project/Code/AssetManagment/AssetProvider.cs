using System.IO;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Services
{
    public class AssetProvider
    {
        private AssetBundle _bundle;
        
        private const string BundleName = "buttons";
        
        public Sprite GetSprite(string assetName)
        {
            return _bundle != null
                ? _bundle.LoadAsset<Sprite>(assetName) 
                : null;
        }

        public async UniTask LoadBundle()
        {
            Unload();

            _bundle = await AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, BundleName));
        }

        public void Unload()
        {
            if (_bundle != null)
                _bundle.Unload(false);
        }
    }
}