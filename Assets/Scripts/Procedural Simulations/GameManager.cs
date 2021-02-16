using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SimulationObjectType
{
    RectangularPrism=1,
    Sphere,
    TriangularPrism,
    RemoteMesh,
    CustomMesh

}

[Serializable]
public class SimulationObjectMaterial
{
    public Color Color;
    public float Metallicness;
    public float Smoothness;
}

[Serializable]
public class SimulationObjectPhysicMaterial
{
    public float bounciness;
    public float dynamicFriction;
    public float staticFriction;
    public PhysicMaterialCombine frictionCombine;
    public PhysicMaterialCombine bounceCombine;
}

[Serializable]
public class SimulationObject
{
    public SimulationObjectType Type;
    public SimulationObjectMaterial Material;
    public SimulationObjectPhysicMaterial PhysicMaterial;
    public Vector3 Position;
    public Vector3 EulerRotation;
    public Vector3 Scale;
}

[Serializable]
public class SimulationConfig
{
    public string Name;
    public string Version;
    public string ConfigVersion;
    public SimulationObject[] Objects;
}

public class GameManager : MonoBehaviour
{
    public GameObject simulationInitializatonPoint;
    
    [Header("Primative Prefabs")]
    [SerializeField]
    private GameObject rectangularPrismPrefab;

    void Start()
    {
        var config = new SimulationConfig
        {
            Name = "Test",
            Version = "0.0.1",
            ConfigVersion = "0.1.0",
            Objects = new SimulationObject[]{
                new SimulationObject
                {
                    Type = SimulationObjectType.RectangularPrism,
                    Material = new SimulationObjectMaterial {Color=new Color(1.0f, 0.0f, 0.0f), Metallicness=1.0f, Smoothness=1.0f},
                    PhysicMaterial = new SimulationObjectPhysicMaterial { staticFriction = 0.50f, dynamicFriction = 0.7f, bounciness = 0.3f },
                    Position = new Vector3(0, 0, 0.5f),
                    EulerRotation = new Vector3(0, 0, 0),
                    Scale = new Vector3(0.5f, 5f, 3f)
                }
            }
        };

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
            case 0:
                break;
        }
        gameObject.transform.localScale = simulationObject.Scale;

        Material gameObjectMat = gameObject.GetComponent<Renderer>().material;
        PhysicMaterial gameObjectPhysicMat = gameObject.GetComponent<Collider>().material;

        gameObjectMat.color = simulationObject.Material.Color;
        gameObjectMat.SetFloat("_Metallic", simulationObject.Material.Metallicness);
        gameObjectMat.SetFloat("_Smoothness", simulationObject.Material.Smoothness);

        gameObjectPhysicMat.bounciness = simulationObject.PhysicMaterial.bounciness;
        gameObjectPhysicMat.dynamicFriction = simulationObject.PhysicMaterial.dynamicFriction;
        gameObjectPhysicMat.staticFriction = simulationObject.PhysicMaterial.staticFriction;
        gameObjectPhysicMat.frictionCombine = simulationObject.PhysicMaterial.frictionCombine;
        gameObjectPhysicMat.bounceCombine = simulationObject.PhysicMaterial.bounceCombine;
    }

    void Update()
    {

    }
}
