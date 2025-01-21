using System;
using Cysharp.Threading.Tasks;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using UnityEngine;

namespace Project.Services
{
    public class RemoteConfig
    {
        private IFirebaseService _firebaseService;
        
        private SettingsConfig _settings;
        private DescriptionsConfig _descriptions;
     
        public SettingsConfig Settings => _settings;
        
        public DescriptionsConfig Descriptions => _descriptions;
        public bool IsLoaded { get; private set; }

        public event Action OnUpdateConfigs;

        private const string SettingsKey = "settings";
        private const string DescriptionKey = "descriptions";
        
        public async UniTask UpdateConfigs()
        {
            IsLoaded = false;
            
            var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            await remoteConfig.FetchAsync(System.TimeSpan.Zero).ContinueWithOnMainThread(
                async previousTask =>
                {
                    if (!previousTask.IsCompleted)
                    {
                        Debug.LogError($"{nameof(remoteConfig.FetchAsync)} incomplete: Status '{previousTask.Status}'");
                        return;
                    }

                    await ActivateRetrievedRemoteConfigValues();
                });
        }

        private async UniTask ActivateRetrievedRemoteConfigValues()
        {
            var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            var info = remoteConfig.Info;
            if(info.LastFetchStatus == LastFetchStatus.Success)
            {
                await remoteConfig.ActivateAsync().ContinueWithOnMainThread(
                    previousTask =>
                    {
                        Debug.Log($"Remote data loaded and ready (last fetch time {info.FetchTime}).");

                        string configStringValue = remoteConfig.GetValue(SettingsKey).StringValue;
                        _settings = JsonUtility.FromJson<SettingsConfig>(configStringValue);

                        configStringValue = remoteConfig.GetValue(DescriptionKey).StringValue;
                        _descriptions = JsonUtility.FromJson<DescriptionsConfig>(configStringValue);

                        IsLoaded = true;

                        OnUpdateConfigs?.Invoke();
                    });
            }
        }
    }
}
