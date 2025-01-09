using UnityEngine;

namespace DeviceAutoRotator
{
    public class BaseOrientationRotator : MonoBehaviour
    {
        protected const float OrientationCheckInterval = 0.5f;

        protected float NextOrientationCheckTime;
        protected ScreenOrientation CurrentOrientation;

        private void Awake()
        {
            CurrentOrientation = Screen.orientation;
            NextOrientationCheckTime = Time.realtimeSinceStartup + 1f;
        }

        private void Update()
        {
            OnTick();
        }

        protected static bool DeviceAutoRotationIsOn()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            using var actClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var context = actClass.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaClass systemGlobal = new AndroidJavaClass("android.provider.Settings$System");
            var rotationOn =
                systemGlobal.CallStatic<int>("getInt", context.Call<AndroidJavaObject>("getContentResolver"), "accelerometer_rotation");

            return rotationOn==1;
#endif
            return true;
        }

        protected virtual void OnTick()
        {
        }
    }
}
