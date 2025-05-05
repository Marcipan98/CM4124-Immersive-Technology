using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SproutCity.Utilities {

    public static class ZoneDropUtility {
        private static float dropHeight = 2.5f;
        private static float dropDuration = 0.25f;
        private static float staggerDelay = 0.1f;

        // Starts a drop in or drop out animation for all children of a named GameObject.
        public static IEnumerator AnimateZoneDrop(
            this MonoBehaviour mb,
            string folderName,
            bool makeVisible
        ) {
            GameObject zone = GameObject.Find(folderName);
            if (zone == null) {
                Debug.LogWarning($"{folderName} folder not found.");
                yield break;
            }

            // Collect all child transforms except the parent
            List<Transform> children = new List<Transform>();
            foreach (var t in zone.GetComponentsInChildren<Transform>(includeInactive: true))
                if (t != zone.transform)
                    children.Add(t);

            int total = children.Count;
            int done = 0;

            // Kick off all drops in parallel, staggered
            for (int i = 0; i < total; i++) {
                yield return new WaitForSeconds(staggerDelay);
                mb.StartCoroutine(
                    DropCoroutine(
                        children[i].gameObject,
                        makeVisible,
                        dropHeight,
                        dropDuration,
                        () => done++
                    )
                );
            }

            // Wait until all have completed
            while (done < total)
                yield return null;
        }

        private static IEnumerator DropCoroutine(
            GameObject obj,
            bool makeVisible,
            float dropHeight,
            float dropDuration,
            Action onComplete
        ) {
            Vector3 original = obj.transform.position;
            Vector3 startPos, endPos;

            if (makeVisible) {
                startPos = original + Vector3.up * dropHeight;
                endPos = original;
                obj.transform.position = startPos;
                obj.SetActive(true);
            } else {
                startPos = original;
                endPos = original - Vector3.up * dropHeight;
            }

            float elapsed = 0f;
            while (elapsed < dropDuration) {
                float t = elapsed / dropDuration;
                obj.transform.position = Vector3.Lerp(startPos, endPos, t);
                elapsed += Time.deltaTime;
                yield return null;
            }

            obj.transform.position = endPos;
            if (!makeVisible)
                obj.SetActive(false);

            onComplete?.Invoke();
        }
    }
}