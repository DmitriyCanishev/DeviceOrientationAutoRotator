using UnityEngine;

namespace App.View
{
    public class OpenSceneButton : MonoBehaviour
    {
        private AppState _appState = null;

        private void Awake()
        {
            _appState = FindObjectOfType<AppState>();
        }

        public async void OpenPortraitScene(string sceneName)
        {
            await _appState.LoadSceneWithLoading(sceneName);
        }

        public async void OpenLandscapeScene(string sceneName)
        {
            await _appState.LoadSceneWithLoading(
                sceneName: sceneName,
                landscapeOrientation: true
            );
        }
    }
}
