using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
<<<<<<< HEAD
=======
using System.Collections.Generic;
>>>>>>> 08f5e0ee514b1f25a8ea36722e2420ca541ab8be

[RequireComponent(typeof(ARTrackedImageManager))]
public class QRScanner : MonoBehaviour {
    [SerializeField] private ARTrackedImageManager trackedImageManager;
<<<<<<< HEAD
    [SerializeField] private GameObject beforePrefab;
    [SerializeField] private GameObject duringPrefab;
    [SerializeField] private GameObject afterPrefab;

    private Camera arCamera;

    void Start() {
        arCamera = Camera.main;
    }
=======
    [SerializeField] private GameObject overlayPrefab;

    private Dictionary<string, GameObject> spawnedOverlays = new();
>>>>>>> 08f5e0ee514b1f25a8ea36722e2420ca541ab8be

    void OnEnable() =>
        trackedImageManager.trackablesChanged.AddListener(OnTrackedImagesChanged);

    void OnDisable() =>
        trackedImageManager.trackablesChanged.RemoveListener(OnTrackedImagesChanged);

    void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args) {
<<<<<<< HEAD
        foreach (var image in args.added) SpawnPoster(image);
    }

    void SpawnPoster(ARTrackedImage image) {
        Vector3 forward = arCamera.transform.forward;
        Vector3 spawnPos = arCamera.transform.position + forward * 1.5f;
        Quaternion facing = Quaternion.LookRotation(forward);

        GameObject prefabToSpawn = null;

        if (image.referenceImage.name == "TYPHOON:BEFORE")
            prefabToSpawn = beforePrefab;
        else if (image.referenceImage.name == "TYPHOON:DURING")
            prefabToSpawn = duringPrefab;
        else if (image.referenceImage.name == "TYPHOON:AFTER")
            prefabToSpawn = afterPrefab;

        if (prefabToSpawn != null) {
            var poster = Instantiate(prefabToSpawn, spawnPos, facing);
            poster.transform.localScale = new Vector3(1.0f, 1.5f, 1f);
            Debug.Log("QR Detected: " + image.referenceImage.name);
=======
        foreach (var image in args.added)          SpawnOverlay(image);
        foreach (var image in args.updated)        UpdateOverlay(image);
        foreach (var kvp in args.removed)          RemoveOverlay(kvp.Value);
    }

    void SpawnOverlay(ARTrackedImage image) {
        if (image.referenceImage.name == "TyphoonQR") {
            var overlay = Instantiate(overlayPrefab, image.transform);
            overlay.transform.localPosition = Vector3.zero;
            spawnedOverlays[image.referenceImage.name] = overlay;
            Debug.Log("QR Detected - Overlay Spawned");
        }
    }

    void UpdateOverlay(ARTrackedImage image) {
        if (spawnedOverlays.TryGetValue(image.referenceImage.name, out var overlay))
            overlay.SetActive(image.trackingState == TrackingState.Tracking);
    }

    void RemoveOverlay(ARTrackedImage image) {
        if (spawnedOverlays.TryGetValue(image.referenceImage.name, out var overlay)) {
            Destroy(overlay);
            spawnedOverlays.Remove(image.referenceImage.name);
            Debug.Log("QR Lost - Overlay Removed");
>>>>>>> 08f5e0ee514b1f25a8ea36722e2420ca541ab8be
        }
    }
}