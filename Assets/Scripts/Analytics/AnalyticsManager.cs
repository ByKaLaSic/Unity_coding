using UnityEngine;
using UnityEngine.Analytics;

public static class AnalyticsManager
{
    public static void OnPlayerDead()
    {
        Analytics.CustomEvent("Player Dead");
    }

    public static void OnLevelStarted()
    {
        Analytics.CustomEvent("Level Started");
    }

    public static void OnPlayerWin()
    {
        Analytics.CustomEvent("Player Win");
    }
}
