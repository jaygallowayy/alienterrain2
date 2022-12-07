using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    private float timeMultiplier;
    [SerializeField]
    private float startHour;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private Light sunLight;
    [SerializeField]
    private float sunriseHour;
    [SerializeField]
    private float sunsetHour;

    private DateTime currentTime;
    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
    }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        if(timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan diff = toTime - fromTime;

        if(diff.TotalSeconds < 0)
        {
            diff += TimeSpan.FromHours(24);
        }

        return diff;
    }

    private void RotateSun()
    {
        float sunlightRotation;

        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetTime = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double percent = timeSinceSunrise.TotalMinutes / sunriseToSunsetTime.TotalMinutes;
            sunlightRotation = Mathf.Lerp(0,180, (float)percent);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunsetDuration = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percent = timeSinceSunsetDuration.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunlightRotation = Mathf.Lerp(180, 360, (float)percent);
        }
        sunLight.transform.rotation = Quaternion.AngleAxis(sunlightRotation, Vector3.right);
    }
}
