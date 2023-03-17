using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private Button addPlanetButton;

    private PlanetManager planetManager;
    // Start is called before the first frame update
    void Start()
    {
        planetManager = FindObjectOfType<PlanetManager>();
        addPlanetButton.onClick.AddListener(AddPlanetHandler);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddPlanetHandler()
    {
        planetManager.CreatePlanet();
    }
}
