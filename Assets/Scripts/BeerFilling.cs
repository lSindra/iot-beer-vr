using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerFilling : MonoBehaviour
{
    public List<GameObject> drinkLayers;
    public BeerBottle beerBottle;

    private List<float> layersLevels = new List<float>();

    void Start()
    {
        InitLayersLevels();
    }

    void Update()
    {
        UpdateShowLayerLevel();
    }

    void InitLayersLevels()
    {
        foreach (GameObject layer in drinkLayers)
        {
            float last = layersLevels.Count == 0 ? -10 : layersLevels[layersLevels.Count-1];
            layersLevels.Add(last + 100 / drinkLayers.Count);
        }
    }

    void UpdateShowLayerLevel()
    {
        int layer = GetLayerLevel();

        foreach (GameObject drinkLayer in drinkLayers)
        {
            layer--;

            if (layer < 0)
            {
                drinkLayer.SetActive(false);
            } else
            {
                drinkLayer.SetActive(true);
            }
        }
    }

    int GetLayerLevel()
    {
        float percentageBeer = beerBottle.PercentageFilled();

        int layer = 0;
        for (int i = 0; i < layersLevels.Count; i++)
        {
            if (percentageBeer >= layersLevels[i])
            {
                layer++;
            }
            else break;
        }
        return layer;
    }
}
