using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDrop : MonoBehaviour
{
    public GameObject objectToSpawn;
    private PlacementIndicatorScript placementIndicator;


    // Start is called before the first frame update
    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicatorScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            GameObject obj = Instantiate(objectToSpawn, placementIndicator.transform.position, placementIndicator.transform.rotation);
        }
    }
}
