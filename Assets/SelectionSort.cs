using System.Collections;
using UnityEngine;

public class SelectionSort : MonoBehaviour
{
    public int numberOfCubes = 10;
    public int cubeHeightMax = 10;
    public float speed = 1f;
    public GameObject cubePrefab = null;
    public GameObject[] cubes = null;
    public Canvas canvasPrefab = null;
    [Header("Material Lookup: \n0: Unsorted \n1: Sorted \n2: Current Place \n3: Best \n4: Looking At")]
    public Material[] materials = new Material[5];

    public void StartSort()
    {
        IntializeRandomCubes();
        StartCoroutine(SelectionSortAlgorithm(cubes));
    }

    IEnumerator SelectionSortAlgorithm(GameObject[] unsortedList)
    {
        for(int i = 0; i < unsortedList.Length; i++)
        {
            int best = i;

            unsortedList[i].GetComponent<Renderer>().material = materials[2]; // Current Place
            yield return new WaitForSeconds(speed);

            for(int j = i + 1; j < unsortedList.Length; j++)
            {
                unsortedList[j].GetComponent<Renderer>().material = materials[4]; // Looking At      
                yield return new WaitForSeconds(speed);

                if(unsortedList[j].transform.localScale.y < unsortedList[best].transform.localScale.y)
                {
                    if(best == i){
                        unsortedList[i].GetComponent<Renderer>().material = materials[2]; // Current Place
                    }
                    else
                    {
                        unsortedList[best].GetComponent<Renderer>().material = materials[0]; // Unsorted
                    }
                    best = j;
                    unsortedList[best].GetComponent<Renderer>().material = materials[3]; // Best
                }
                else
                {
                    unsortedList[j].GetComponent<Renderer>().material = materials[0]; // Unsorted
                }
            }

            if(best != i)
            {                
                Swap(unsortedList, i, best);
                yield return new WaitForSeconds(speed);
            }
            unsortedList[best].GetComponent<Renderer>().material = materials[0]; // Unsorted
            unsortedList[i].GetComponent<Renderer>().material = materials[1]; // Sorted
        }
    }

    void Swap(GameObject[] list, int i, int j)
    {
        GameObject temp = list[i];
        list[i] = list[j];
        list[j] = temp;

        Vector3 tempPosition = list[i].transform.localPosition;

        LeanTween.moveLocalX(list[i], list[j].transform.localPosition.x, speed);
        LeanTween.moveLocalZ(list[i], -3f, speed/2).setLoopPingPong(1);
        
        LeanTween.moveLocalX(list[j], tempPosition.x, speed);
        LeanTween.moveLocalZ(list[j], 3f, speed/2).setLoopPingPong(1);
    }

    void IntializeRandomCubes()
    {
        cubes = new GameObject[numberOfCubes];

        for(int i = 0; i < numberOfCubes; i++)
        {
            int randomNumber = Random.Range(1, cubeHeightMax + 1);

            Vector3 cubePosition = new Vector3(i, randomNumber / 2f, 0f);

            GameObject cube = Instantiate(cubePrefab, cubePosition, Quaternion.identity);
            cube.transform.localScale = new Vector3(0.7f, randomNumber, 1f);

            Canvas canvas = Instantiate(canvasPrefab, new Vector3(i, -1f, -0.501f), Quaternion.identity);
            canvas.transform.SetParent(cube.transform);
            cube.GetComponent<ValueText>().Setup();

            cube.transform.SetParent(this.transform);

            cubes[i] = cube;
        }

        this.transform.position = new Vector3(-numberOfCubes / 2f + 0.5f, 0f, 0f);
    }
}
