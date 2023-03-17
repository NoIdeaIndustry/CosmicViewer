using UnityEngine;

public struct PlanetEffects
{
    public Vector3 force;
    public Vector3 rotation;

    public PlanetEffects(Vector3 f, Vector3 r)
    {
        force = f;
        rotation = r;
    }
}

public class PlanetObject : MonoBehaviour
{
    [SerializeField] public float radius;
    [SerializeField] public float mass;

    public PlanetEffects ComputeEffectOnPlanet(PlanetObject planetObject)
    {
        // Calcul de la distance et de la direction entre position et la position de la masse i
        Vector3 direction = transform.position - planetObject.transform.position;
        float distance = direction.magnitude;

        // Calcul du champ vectoriel gravitationnel de la masse i en position
        float gravitationalField = PlanetManager.G * mass * planetObject.mass / (distance * distance);
        Vector3 force = gravitationalField * direction.normalized;
        Vector3 rotation = Vector3.Cross(direction, force) / (distance * distance);
        return new PlanetEffects(force, rotation);
    }
    
    public PlanetEffects ComputeEffectAtPoint(Vector3 otherPosition)
    {
        // Calcul de la distance et de la direction entre position et la position de la masse i
        Vector3 direction = transform.position - otherPosition;
        float distance = direction.magnitude;

        // Calcul du champ vectoriel gravitationnel de la masse i en position
        float gravitationalField = PlanetManager.G * mass / (distance * distance);
        Vector3 force = gravitationalField * direction.normalized;
        Vector3 rotation = Vector3.Cross(direction, force) / (distance * distance);
        return new PlanetEffects(force, rotation);
    }
}
