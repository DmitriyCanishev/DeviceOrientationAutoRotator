using UnityEngine;

namespace DeviceAutoRotator
{
    public class LandscapeOrientationAutoRotator : BaseOrientationRotator
    {
        private ScreenOrientation CurrentLandscapeOrientation
        {
            set
            {
                if (CurrentOrientation != value)
                {
                    CurrentOrientation = value;
                    Screen.orientation = value;
                }
            }
        }

        protected override void OnTick()
        {
            switch (Time.realtimeSinceStartup >= NextOrientationCheckTime)
            {
                case true:
                {
                    DeviceOrientation orientation = Input.deviceOrientation;
                    if (orientation == DeviceOrientation.LandscapeLeft || orientation == DeviceOrientation.LandscapeRight)
                    {
                        switch (orientation)
                        {
                            case DeviceOrientation.LandscapeLeft:
                            {
                                RotateOnLandscapeLeftIfAutoRotationEnable();
                                break;
                            }
                            case DeviceOrientation.LandscapeRight:
                            {
                                RotateOnLandscapeRightIfAutoRotationEnable();
                                break;
                            }
                        }
                    }

                    NextOrientationCheckTime = Time.realtimeSinceStartup + OrientationCheckInterval;
                    break;
                }
            }
        }

        private void RotateOnLandscapeRightIfAutoRotationEnable()
        {
            Screen.autorotateToLandscapeRight = DeviceAutoRotationIsOn();
            if (Screen.autorotateToLandscapeRight)
                CurrentLandscapeOrientation = ScreenOrientation.LandscapeRight;
        }

        private void RotateOnLandscapeLeftIfAutoRotationEnable()
        {
            Screen.autorotateToLandscapeLeft = DeviceAutoRotationIsOn();
            if (Screen.autorotateToLandscapeLeft)
                CurrentLandscapeOrientation = ScreenOrientation.LandscapeLeft;
        }
    }
}
