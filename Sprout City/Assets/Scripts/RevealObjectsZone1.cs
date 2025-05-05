using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SproutCity.Utilities;

public class RevealObjectsZone1 : MonoBehaviour {
    private bool triggeredOnce = false;

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player"))
            return;

        // Ensuring the trigger only fires once
        if (triggeredOnce) 
            return;

        triggeredOnce = true;

        Debug.Log("Player entered zone 1.");
        StartCoroutine(HandleZones());
    }

    private IEnumerator HandleZones() {
        // Destroy ParticleSystemZone1 at the end
        GameObject particleSystemZone1 = GameObject.Find("ParticleSystemZone1");
        if (particleSystemZone1 != null) {
            Destroy(particleSystemZone1);
            Debug.Log("ParticleSystemZone1 destroyed.");
        } else {
            Debug.LogWarning("ParticleSystemZone1 not found.");
        }

        // Hide trash_zone_1
        yield return this.AnimateZoneDrop(
            "trash_zone_1",
            makeVisible: false
        );

        // Then show healthy_zone_1
        yield return this.AnimateZoneDrop(
            "healthy_zone_1",
            makeVisible: true
        );

        PuzzlesGameState.Instance.isPurpleZoneComplete = true;
        PuzzlesGameState.Instance.UpdateUI();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
