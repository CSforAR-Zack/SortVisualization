using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSortMenu : MonoBehaviour
{
    public SelectionSort selectionSort = null;
    public SelectionSort activeSort = null;
    public InputField speed = null;
    public InputField numberOfCubes = null;

    public void StartSort()
    {
        if(activeSort != null)
        {
            ResetSort();
        }

        activeSort = Instantiate(selectionSort);

        try
        {
            activeSort.speed = float.Parse(speed.text);
            activeSort.numberOfCubes = int.Parse(numberOfCubes.text);
        }
        catch(Exception)
        {
            activeSort.speed = 1f;
            activeSort.numberOfCubes = 5;
        }
        activeSort.StartSort();
    }

    public void ResetSort()
    {
        Destroy(activeSort.gameObject);
    }
}
