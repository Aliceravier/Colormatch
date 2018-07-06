using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO:rename
public class cameraReference : MonoBehaviour {

    public GameObject playerCamera;
    public GameObject minimapCamera;

    private Camera _minimapCamera;
    private Camera _playerCamera;
    int cullingMask;

    [SerializeField]
    private LayerMask visibleLayers;

    void Awake()
    {
        _minimapCamera = minimapCamera.GetComponent<Camera>();
        cullingMask = _minimapCamera.cullingMask;
    }

    public void viewAllMinimaps(GameObject[] players)
    {
        foreach (GameObject player in players)
        {
            if (player != gameObject) {
                cameraReference otherRef = player.GetComponent<cameraReference>();
                _minimapCamera.cullingMask |= (1 << otherRef.getVisibleLayers());
            }
            
        }
    }

    public void resetMask()
    {
        _minimapCamera.cullingMask = cullingMask;
    }

    public int getVisibleLayers()
    {
        return (int)Mathf.Log(visibleLayers.value, 2);
    }

}
