using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    public Spawner buildingSpawner;
    private void OnEnable()
    {
        VisualElement ui = GetComponent<UIDocument>().rootVisualElement;

        Button buildingOne = ui.Q<Button>("Building1");
        Button buildingTwo = ui.Q<Button>("Building2");

        buildingOne.clicked += () => buildingSpawner.createBuilding(buildingOne.name);
        buildingTwo.clicked += () => buildingSpawner.createBuilding(buildingTwo.name);

    }
}
