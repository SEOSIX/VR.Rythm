using UnityEngine;

public class DisplayCorrection : DisplayError
{
    private void CorrectionCamera()
    {
        if (timeToDisplayCamera != 0f)
        {
            return;
        } 
        if (timeToDisplayCamera<= 0f)
        {
            timeToDisplayCamera = baseTimeToDisplayCamera;
            if (TimeDisplay != null)
                TimeDisplay.value = timeToDisplayCamera;
        }
    }

    private void CorrectionRythm()
    {
        if (numberUsageRythmActivator != 0f)
        {
            return;
        } 
        if (numberUsageRythmActivator <= 0f)
        {
            numberUsageRythmActivator = baseNumberUsageRythmActivator;
        }
    }
}
