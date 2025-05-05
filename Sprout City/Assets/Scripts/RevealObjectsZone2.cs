using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SproutCity.Utilities;

public class RevealObjectsZone2 : MonoBehaviour {
    private bool triggeredOnce = false;

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player"))
            return;

        // Ensuring the trigger only fires once
        if (triggeredOnce) 
            return;

        triggeredOnce = true;

        Debug.Log("Player entered zone 2.");
        StartCoroutine(HandleZones());
    }

    private IEnumerator HandleZones() {
        // Destroy ParticleSystemZone1 at the end
        GameObject particleSystemZone1 = GameObject.Find("ParticleSystemZone2");
        if (particleSystemZone1 != null) {
            Destroy(particleSystemZone1);
            Debug.Log("ParticleSystemZone2 destroyed.");
        } else {
            Debug.LogWarning("ParticleSystemZone2 not found.");
        }

        // Hide trash_zone_2
        yield return this.AnimateZoneDrop(
            "trash_zone_2",
            makeVisible: false
        );

        // Then show healthy_zone_2
        yield return this.AnimateZoneDrop(
            "healthy_zone_2",
            makeVisible: true
        );

        PuzzlesGameState.Instance.isOrangeZoneComplete = true;
        PuzzlesGameState.Instance.UpdateUI();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
