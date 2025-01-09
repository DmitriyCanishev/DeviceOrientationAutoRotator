using System.Threading.Tasks;
using DeviceAutoRotator;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace App
{
    public class AppState : MonoBehaviour
    {
        [SerializeField] private Transform[] _dontDestroyObjects = null;

        private const string AppSceneName = "AppScene";
        private const string LoadingSceneName = "LoadingScene";

        private PortraitOrientationAutoRotator _portraitRotator = null;
        private LandscapeOrientationAutoRotator _landscapeRotator = null;

        private void Awake()
        {
            Screen.orientation = ScreenOrientation.Portrait;

            foreach (var dontDestroyObject in _dontDestroyObjects)
            {
                if (dontDestroyObject != null)
                {
                    DontDestroyOnLoad(dontDestroyObject.gameObject);
                }
            }

            _portraitRotator = FindObjectOfType<PortraitOrientationAutoRotator>();
            _landscapeRotator = FindObjectOfType<LandscapeOrientationAutoRotator>();
        }

        private async void Start()
        {
            await LoadAppScene();
        }

        private async Task LoadAppScene()
        {
            await OpenLoadingScene(LoadSceneMode.Additive);
            ChangeOrientationRotator(false);
            await SceneManager.LoadSceneAsync(AppSceneName, LoadSceneMode.Single).ToTask();
        }

        private async Task OpenLoadingScene(LoadSceneMode loadSceneMode)
        {
            await SceneManager.LoadSceneAsync(LoadingSceneName, loadSceneMode).ToTask();
        }

        public async Task LoadSceneWithLoading(string sceneName, bool landscapeOrientation = false)
        {
            await OpenLoadingScene(LoadSceneMode.Single);
            ChangeOrientationRotator(landscapeOrientation);
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).ToTask();
        }

        private void ChangeOrientationRotator(bool landOrientation)
        {
            _landscapeRotator.gameObject.SetActive(landOrientation);
            _portraitRotator.gameObject.SetActive(!landOrientation);
        }
    }
}
