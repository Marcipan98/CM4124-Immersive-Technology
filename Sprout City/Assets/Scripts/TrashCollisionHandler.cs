using UnityEngine;

public class TrashCollisionHandler : MonoBehaviour {
    private Transform healthyZoneParent;
    private ParkMiniGame miniGame;

    void Start() {
        // cache the ParkMiniGame instance
        miniGame = FindObjectOfType<ParkMiniGame>();
        if (miniGame == null)
            Debug.LogError("No ParkMiniGame found in scene!");

        // cache the healthy zone parent for quick lookups
        GameObject healthy_zone = GameObject.Find("park_healthy_zone");
        if (healthy_zone != null)
            healthyZoneParent = healthy_zone.transform;
        else
            Debug.LogWarning("park_healthy_zone not found!");
    }

    private void OnTriggerEnter(Collider other) {
        // only care if the thing we hit is a child of park_healthy_zone
        if (healthyZoneParent != null 
            && other.transform.IsChildOf(healthyZoneParent)) {
            // Tell the mini game we picked up one
            miniGame.OnTrashPickedUp();

            Debug.Log($"Trash item '{gameObject.name}' was destroyed.");

            // Remove this trash item
            Destroy(gameObject);
        }
    }
}