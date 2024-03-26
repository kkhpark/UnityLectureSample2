using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultipleImageTracking : MonoBehaviour
{
    public GameObject[] ObjectsToSpawn; // objects array when image is recognized
    private Dictionary<string, GameObject> SpawnedObject = new Dictionary<string, GameObject>();
    private ARTrackedImageManager ARTrackedImageManager;

    // Start is called before the first frame update
    void Awake()
    {
        ARTrackedImageManager = GetComponent<ARTrackedImageManager>(); //Get component from connected GameObject
        foreach (GameObject obj in ObjectsToSpawn)
        {
            GameObject clone = Instantiate(obj);
            SpawnedObject.Add(obj.name, clone);
            clone.SetActive(false);
        }
        
    }


    private void OnEnable()
    {
        // Will add this callback method(OnTrackedImageChanged) when this object is enabled.
        ARTrackedImageManager.trackedImagesChanged += OnTrackedImageChanged; // �� �Լ��� onEnabled �ɶ� �߰� �ϰڴ�.
    }

    //AR Foundation - Respond to dected Images�� ���� API ������ �Ǿ� ����.
    void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // A��� �̹����� �ν� ��Ű�ٰ� B��� �̹����� �ν� ��ų�� �ٲ���� �ȹٲ���ĸ� �˷��ִ� �Լ��ΰ���.
        foreach(var trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }
        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach (var trackedImage in eventArgs.removed)
        {
            // disable object
            SpawnedObject[trackedImage.name].SetActive(false);
        }
    }

    void UpdateImage(ARTrackedImage trackedImage)
    {
        GameObject trackedObject = SpawnedObject[trackedImage.referenceImage.name]; // �Ʊ� Image01�� �������� �� �̸�
        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            trackedObject.transform.position = trackedImage.transform.position;
            trackedObject.transform.rotation = trackedImage.transform.rotation;
            trackedObject.SetActive(true);
        } else
        {
            trackedObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        ARTrackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
    }
}
