using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SimulationObjectType
{
    RectangularPrism,
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
public class SimulationObject
{
    public SimulationObjectType Type;
    public SimulationObjectMaterial Material;
    public PhysicMaterial PhysicMaterial;
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
                    Material = null,
                    PhysicMaterial = new PhysicMaterial { staticFriction = 1.0f, dynamicFriction = 1.0f, bounciness = 0.3f },
                    Position = new Vector3(0, 0, 0.5f),
                    EulerRotation = new Vector3(0, 0, 0),
                    Scale = new Vector3(1f, 1f, 1f)
                }
            }
        };

        string configJson = JsonUtility.ToJson(config);
        Debug.Log(configJson);
    }

    void Update()
    {

    }
}
