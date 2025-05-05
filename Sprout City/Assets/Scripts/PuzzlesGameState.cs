using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SproutCity.Utilities;

public class PuzzlesGameState : MonoBehaviour {
    public static PuzzlesGameState Instance { get; private set; }

    public bool isParkMiniGameComplete = false;
    public bool isPurpleZoneComplete = false;
    public bool isOrangeZoneComplete = false;
    private bool triggeredOnce = false;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
            UpdateUI();
        }
    }

    public void UpdateUI() {
        GameObject gameUICanvas = GameObject.Find("GameUI");
        if (gameUICanvas != null) {
            Text taskListText = gameUICanvas.transform.Find("TaskListText")?.GetComponent<Text>();
            if (taskListText != null) {
                taskListText.text = $"Your eco journey:\n" +
                    $"- Enter the purple mist:\t{(isPurpleZoneComplete ? "Complete" : "Incomplete")}\n" +
                    $"- Enter the orange mist:\t{(isOrangeZoneComplete ? "Complete" : "Incomplete")}\n" +
                    $"- Clean up the park:\t\t{(isParkMiniGameComplete ? "Complete" : "Incomplete")}";
            } else {
                Debug.LogWarning("TaskListText component not found in GameUI canvas.");
            }
        } else {
            Debug.LogWarning("GameUI canvas not found in the scene.");
        }
    }
}
