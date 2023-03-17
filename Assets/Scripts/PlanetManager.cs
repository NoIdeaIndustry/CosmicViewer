using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public static float G = 6.67430e-11f;
    
    [SerializeField] private GameObject[] planetPrefabs;
    
    [SerializeField] private int fieldSize = 100;
    [SerializeField] private List<PlanetObject> planetObjects;
    void Start()
    {
        planetPrefabs = Resources.LoadAll<GameObject>("Planet");
        planetObjects = FindObjectsOfType<PlanetObject>().ToList();
        
    }

    public void CreatePlanet()
    {
        GameObject planetGameObject = Instantiate(planetPrefabs[Random.Range(0, planetPrefabs.Length - 1)]);
        PlanetObject planetObject = planetGameObject.AddComponent<PlanetObject>();
        planetObjects.Add(planetObject);
    }
    
    private void OnDrawGizmos()
    {
        if (planetObjects.Count == 0) return;
        
        foreach (var planet in planetObjects) {
            for (float x = 0; x < fieldSize; x++)
            {
                for (float y = 0; y < fieldSize; y++)
                {
                    for (float z = 0; z < fieldSize; z++)
                    {
                        Vector3 origin = planet.transform.position + new Vector3(x, y, z) -
                                         new Vector3(fieldSize / 2, fieldSize / 2, fieldSize / 2);
                        
                        var effects = ComputeForcesAtPoint(origin);
                        DrawForceAtPoint(origin, effects);
                    }
                }
            }
        }
    }

    PlanetEffects ComputeForcesAtPoint(Vector3 point)
    {
        PlanetEffects effects = new PlanetEffects(Vector3.zero, Vector3.zero);
        
        foreach (var planet in planetObjects)
        {
            PlanetEffects effect = planet.ComputeEffectAtPoint(point);
            effects.force += effect.force;
            effects.rotation += effect.rotation;
        }

        return effects;
    }

    void DrawForceAtPoint(Vector3 origin, PlanetEffects effect)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(origin, effect.force);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(origin, effect.rotation*1e9f);
    }
}
