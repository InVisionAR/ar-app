using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject simulationInitializatonPoint;
    
    [Header("Primative Prefabs")]
    [SerializeField] private GameObject rectangularPrismPrefab;
    [SerializeField] private GameObject triangularPrismPrefab;
    [SerializeField] private GameObject spherePrefab;

    [Header("UI")]
    private bool paused = false;
    private float timeScale = 1.0f;
    [SerializeField] private GameObject playText;
    [SerializeField] private GameObject pauseText;

    [Header("Misc")]
    public GameObject basePlane;
    private PhysicMaterial basePlanePhysicMaterial;
    
    private List<GameObject> simulationGameObjects = new List<GameObject>();
    private List<Material> simulationMaterials = new List<Material>();
    private List<PhysicMaterial> simulationPhysicMaterials = new List<PhysicMaterial>();
    private List<Rigidbody> simulationRigidbodies = new List<Rigidbody>();


    void Start()
    {
        TogglePause();
        if(basePlane == null)
        {
            basePlane = GameObject.FindGameObjectsWithTag("Base Plane")[0];
        }

        SimulationConfig config;
        
        using(StreamReader r = new StreamReader("Assets/Scripts/Procedural Simulations/TestConfig.json")){
            string configJson = r.ReadToEnd();
            config = SimulationConfig.FromJson(configJson);
        }

        basePlanePhysicMaterial = basePlane.GetComponent<Collider>().material;
        SetSimulationObjectPhysicMaterialProperties(basePlanePhysicMaterial, config.BasePlanePhysicMaterial);

        foreach(SimulationObject simulationObject in config.Objects){
            BuildSimulationObject(simulationObject);
        }
    }

    void BuildSimulationObject(SimulationObject simulationObject){
        GameObject gameObject = null;
        switch(simulationObject.Type){
            case SimulationObjectType.RectangularPrism:
                gameObject = (GameObject)Instantiate(rectangularPrismPrefab, simulationObject.Position, Quaternion.Euler(simulationObject.EulerRotation), simulationInitializatonPoint.transform);
                break;
            case SimulationObjectType.TriangularPrism:
                gameObject = (GameObject)Instantiate(triangularPrismPrefab, simulationObject.Position, Quaternion.Euler(simulationObject.EulerRotation), simulationInitializatonPoint.transform);
                break;
            case SimulationObjectType.Sphere:
                gameObject = (GameObject)Instantiate(spherePrefab, simulationObject.Position, Quaternion.Euler(simulationObject.EulerRotation), simulationInitializatonPoint.transform);
                break;
            case 0:
                break;
        }
        gameObject.transform.localScale = simulationObject.Scale;

        Material gameObjectMat = gameObject.GetComponent<Renderer>().material;
        PhysicMaterial gameObjectPhysicMaterial = gameObject.GetComponent<Collider>().material;
        Rigidbody gameObjectRigidbody = gameObject.GetComponent<Rigidbody>();

        simulationGameObjects.Add(gameObject);
        simulationMaterials.Add(gameObjectMat);
        simulationPhysicMaterials.Add(gameObjectPhysicMaterial);
        simulationRigidbodies.Add(gameObjectRigidbody);

        gameObjectMat.color = simulationObject.Material.Color;
        gameObjectMat.SetFloat("_Metallic", simulationObject.Material.Metallicness);
        gameObjectMat.SetFloat("_Smoothness", simulationObject.Material.Smoothness);

        SetSimulationObjectPhysicMaterialProperties(gameObjectPhysicMaterial, simulationObject.PhysicMaterial);

        gameObjectRigidbody.mass = simulationObject.Mass;
        gameObjectRigidbody.isKinematic = simulationObject.Static;
    }

    void SetSimulationObjectPhysicMaterialProperties(PhysicMaterial gameObjectPhysicMaterial, SimulationObjectPhysicMaterial simulationObjectPhysicMaterial){
        gameObjectPhysicMaterial.bounciness = simulationObjectPhysicMaterial.bounciness;
        gameObjectPhysicMaterial.dynamicFriction = simulationObjectPhysicMaterial.dynamicFriction;
        gameObjectPhysicMaterial.staticFriction = simulationObjectPhysicMaterial.staticFriction;
        gameObjectPhysicMaterial.frictionCombine = simulationObjectPhysicMaterial.frictionCombine;
        gameObjectPhysicMaterial.bounceCombine = simulationObjectPhysicMaterial.bounceCombine;
    }

    void Update()
    {
        for(int i = 0; i < simulationGameObjects.Count; i++){
            if(simulationGameObjects[i].transform.position.y < 0){
                Destroy(simulationGameObjects[i]);
                simulationGameObjects.RemoveAt(i);
                simulationMaterials.RemoveAt(i);
                simulationPhysicMaterials.RemoveAt(i);
                simulationRigidbodies.RemoveAt(i);
                i--;
            }
        }
    }

    public void TogglePause(){
        if(paused){
            playText.SetActive(false);
            pauseText.SetActive(true);
            Time.timeScale = timeScale;
        }
        else{
            playText.SetActive(true);
            pauseText.SetActive(false);
            Time.timeScale = 0;
        }
        // Time.fixedDeltaTime = 0.02f * timeScale;
        paused = !paused;
    }

    public void TimeScaleChanged(Slider slider){
        timeScale = slider.value;
        if(!paused){
            Time.timeScale = timeScale;
            // Time.fixedDeltaTime = 0.02f * timeScale;
        }
    }
}
