using UnityEngine;

namespace DeviceAutoRotator
{
    public class PortraitOrientationAutoRotator : BaseOrientationRotator
    {
        private ScreenOrientation CurrentPortraitOrientation
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
                    if (orientation == DeviceOrientation.Portrait || orientation == DeviceOrientation.PortraitUpsideDown)
                    {
                        switch (orientation)
                        {
                            case DeviceOrientation.PortraitUpsideDown:
                            {
                                RotateOnPortraitUpsideDownIfAutoRotationEnable();
                                break;
                            }
                            default:
                            {
                                RotateOnPortraitIfAutoRotationEnable();
                                break;
                            }
                        }
                    }

                    NextOrientationCheckTime = Time.realtimeSinceStartup + OrientationCheckInterval;
                    break;
                }
            }
        }

        private void RotateOnPortraitIfAutoRotationEnable()
        {
            Screen.autorotateToPortrait = DeviceAutoRotationIsOn();
            if (Screen.autorotateToPortrait)
                CurrentPortraitOrientation = ScreenOrientation.Portrait;
        }

        private void RotateOnPortraitUpsideDownIfAutoRotationEnable()
        {
            Screen.autorotateToPortraitUpsideDown = DeviceAutoRotationIsOn();
            if (Screen.autorotateToPortraitUpsideDown)
                CurrentPortraitOrientation = ScreenOrientation.PortraitUpsideDown;
        }
    }
}
