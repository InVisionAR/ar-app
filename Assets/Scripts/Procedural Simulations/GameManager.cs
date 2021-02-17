using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject simulationInitializatonPoint;
    
    [Header("Primative Prefabs")]
    [SerializeField] private GameObject rectangularPrismPrefab;
    [SerializeField] private GameObject triangularPrismPrefab;
    [SerializeField] private GameObject spherePrefab;

    [Header("Misc")]
    public GameObject basePlane;
    private PhysicMaterial basePlanePhysicMaterial;
    
    private List<GameObject> simulationGameObjects = new List<GameObject>();
    private List<Material> simulationMaterials = new List<Material>();
    private List<PhysicMaterial> simulationPhysicMaterials = new List<PhysicMaterial>();
    private List<Rigidbody> simulationRigidbodies = new List<Rigidbody>();


    void Start()
    {
        if(basePlane == null)
        {
            basePlane = GameObject.FindGameObjectsWithTag("Base Plane")[0];
        }
        
        var config = new SimulationConfig
        {
            Name = "Test",
            Version = "0.0.1",
            ConfigVersion = "0.3.0",
            BasePlanePhysicMaterial = new SimulationObjectPhysicMaterial { staticFriction = 0.0f, dynamicFriction = 0.0f, bounciness = 0.0f },
            Objects = new SimulationObject[]{
                new SimulationObject
                {
                    Type = SimulationObjectType.TriangularPrism,
                    Material = new SimulationObjectMaterial {Color=new Color(1.0f, 1.0f, 1.0f), Metallicness=0.0f, Smoothness=0.2f},
                    PhysicMaterial = new SimulationObjectPhysicMaterial { staticFriction = 0.0f, dynamicFriction = 0.0f, bounciness = 0.0f },
                    Position = new Vector3(0, 1f, 0),
                    EulerRotation = new Vector3(0, 0, 0),
                    Scale = new Vector3(3f, 2f, 1.5f),
                    Mass = 5.0f,
                    Static = true
                },
                new SimulationObject
                {
                    Type = SimulationObjectType.RectangularPrism,
                    Material = new SimulationObjectMaterial {Color=new Color(1.0f, 0.0f, 0.0f), Metallicness=0.0f, Smoothness=0.2f},
                    PhysicMaterial = new SimulationObjectPhysicMaterial { staticFriction = 0.0f, dynamicFriction = 0.0f, bounciness = 0.0f },
                    Position = new Vector3(-1.25f, 2.5f, 0),
                    EulerRotation = new Vector3(0, 0, -30),
                    Scale = new Vector3(0.9f, 0.9f, 0.9f),
                    Mass = 1.0f,
                    Static = false
                }
            }
        };

        basePlanePhysicMaterial = basePlane.GetComponent<Collider>().material;
        SetSimulationObjectPhysicMaterialProperties(basePlanePhysicMaterial, config.BasePlanePhysicMaterial);

        foreach(SimulationObject simulationObject in config.Objects){
            BuildSimulationObject(simulationObject);
        }

        // string configJson = JsonUtility.ToJson(config);
        // Debug.Log(configJson); <-- works as expected
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
        PhysicMaterial gameObjectPhysicMat = gameObject.GetComponent<Collider>().material;
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
        gameObjectPhysicMat.bounciness = simulationObjectPhysicMaterial.bounciness;
        gameObjectPhysicMat.dynamicFriction = simulationObjectPhysicMaterial.dynamicFriction;
        gameObjectPhysicMat.staticFriction = simulationObjectPhysicMaterial.staticFriction;
        gameObjectPhysicMat.frictionCombine = simulationObjectPhysicMaterial.frictionCombine;
        gameObjectPhysicMat.bounceCombine = simulationObjectPhysicMaterial.bounceCombine;
    }

    void Update()
    {

    }
}
