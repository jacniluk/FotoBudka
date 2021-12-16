using Dummiesman;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ModelsManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform modelsContainer;

    private List<GameObject> models;
    private int currentModelIndex;

    private void Awake()
    {
        models = new List<GameObject>();
    }

    private void Start()
    {
        ImportModels();
    }

    private void ImportModels()
    {
        string[] objFilesPaths = Directory.GetFiles(Application.dataPath + "/../Input", "*.obj");
        for (int i = 0; i < objFilesPaths.Length; i++)
        {
            GameObject model = new OBJLoader().Load(objFilesPaths[i]);
            model.transform.localScale = Vector3.one;

            model.transform.SetParent(modelsContainer);
            model.SetActive(i == 0);
            models.Add(model);
        }

        if (models.Count != 0)
        {
            AspectManager.Instance.SetModelTransform(models[currentModelIndex].transform);
        }
    }

    public void ShowPreviousModel()
    {
        models[currentModelIndex].SetActive(false);

        if (currentModelIndex > 0)
        {
            currentModelIndex--;
        }
        else
        {
            currentModelIndex = models.Count - 1;
        }

        models[currentModelIndex].SetActive(true);

        AspectManager.Instance.SetModelTransform(models[currentModelIndex].transform);
    }

    public void ShowNextModel()
    {
        models[currentModelIndex].SetActive(false);

        if (currentModelIndex < models.Count - 1)
        {
            currentModelIndex++;
        }
        else
        {
            currentModelIndex = 0;
        }

        models[currentModelIndex].SetActive(true);

        AspectManager.Instance.SetModelTransform(models[currentModelIndex].transform);
    }
}
