using System;
using UnityEngine;

public enum SimulationObjectType
{
    RectangularPrism = 1,
    TriangularPrism,
    Sphere,
    RemoteMesh,
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
    public float Mass;
    public bool Static;
}

[Serializable]
public class SimulationConfig
{
    public string Name;
    public string Version;
    public string ConfigVersion;
    public SimulationObjectPhysicMaterial PlanePhysicMaterial;
    public SimulationObject[] Objects;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public static SimulationConfig FromJson(string json)
    {
        return JsonUtility.FromJson<SimulationConfig>(json);
    }
}