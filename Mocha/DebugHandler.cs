using UnityEngine;
using UnityEngine.UI;
using BepInEx;
using BepInEx.Logging;
using System.Text;

[BepInPlugin("org.thaterror404.gorillatag.debug", "Debug Display", "1.0.0")]
public class DebugDisplay : BaseUnityPlugin
{
    // Logger instance to allow logging to BepInEx console and log file
    private static ManualLogSource logger;

    private StringBuilder log = new StringBuilder();
    private Text debugText;

    void Awake()
    {
        logger = Logger;
        logger.LogInfo("Debug Display loaded");

        // Initialize the debug text UI element
        GameObject debugTextObject = new GameObject("DebugText");
        debugText = debugTextObject.AddComponent<Text>();

        // TODO: Set up the debug text object's position, size, and other properties
        // This will depend on the specifics of the game and its UI system

        // Register the log callback
        Application.logMessageReceived += HandleLog;
    }

    void OnDestroy()
    {
        // Unregister the log callback when the plugin is unloaded
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Append the log message to the log
        log.AppendLine(logString);

        // Update the debug text with the log
        debugText.text = log.ToString();
    }
}
