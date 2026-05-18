using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

[RequireComponent(typeof(ARTrackedImageManager))]
public class QRScanner : MonoBehaviour {
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    [SerializeField] private GameObject overlayPrefab;

    private Dictionary<string, GameObject> spawnedOverlays = new();

    void OnEnable() =>
        trackedImageManager.trackablesChanged.AddListener(OnTrackedImagesChanged);

    void OnDisable() =>
        trackedImageManager.trackablesChanged.RemoveListener(OnTrackedImagesChanged);

    void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args) {
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
        }
    }
}