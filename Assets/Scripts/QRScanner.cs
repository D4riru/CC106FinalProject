using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class QRScanner : MonoBehaviour {
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    [SerializeField] private GameObject beforePrefab;
    [SerializeField] private GameObject duringPrefab;
    [SerializeField] private GameObject afterPrefab;

    private Camera arCamera;

    void Start() {
        arCamera = Camera.main;
    }

    void OnEnable() =>
        trackedImageManager.trackablesChanged.AddListener(OnTrackedImagesChanged);

    void OnDisable() =>
        trackedImageManager.trackablesChanged.RemoveListener(OnTrackedImagesChanged);

    void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args) {
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
        }
    }
}