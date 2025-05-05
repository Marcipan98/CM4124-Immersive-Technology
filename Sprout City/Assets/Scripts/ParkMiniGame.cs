using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkMiniGame : MonoBehaviour {
    private static int trashCollected = 0;
    private static int trashToPickUp = 4;

    [Header("UI References")]
    [SerializeField] private Text statusText;
    [SerializeField] private GameObject parkMiniGameCanvasUI;

    // Start is called before the first frame update
    void Start() {
        // Ensure UI is hidden at start
        if (parkMiniGameCanvasUI != null)
            parkMiniGameCanvasUI.SetActive(false);

        UpdateStatusUI();
    }

    // Update is called once per frame
    void Update() {
    }

    private void UpdateStatusUI() {
        if (statusText != null)
            statusText.text = $"Trash collected: {trashCollected} / {trashToPickUp}";
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player"))
            return;

        // Toggle mini game UI visibility
        if (parkMiniGameCanvasUI != null) {
            bool isCurrentlyVisible = parkMiniGameCanvasUI.activeSelf;
            parkMiniGameCanvasUI.SetActive(!isCurrentlyVisible);
            Debug.Log(isCurrentlyVisible
                ? "Hid ParkMiniGameCanvasUI."
                : "Showed ParkMiniGameCanvasUI.");
        }

        Debug.Log("Player entered mini game area.");
    }

    public void OnTrashPickedUp() {
        trashCollected++;
        UpdateStatusUI();

        if (trashCollected == trashToPickUp) {
            PuzzlesGameState.Instance.isParkMiniGameComplete = true;
            PuzzlesGameState.Instance.UpdateUI();
        }
    }
}
