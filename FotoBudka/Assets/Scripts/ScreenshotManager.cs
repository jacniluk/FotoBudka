using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class ScreenshotManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Canvas canvas;

    private const string ImageExtension = ".png";

    private string outputPath;

    private void Awake()
    {
        if (Application.dataPath.Contains(" ") == false)
        {
            outputPath = Application.dataPath + "/../Output/";
        }
        else
        {
            outputPath = Application.persistentDataPath + "/Output/";
        }
    }

    private void Start()
    {
        if (Directory.Exists(outputPath) == false)
        {
            Directory.CreateDirectory(outputPath);
        }
    }

    public void TakeScreenshot()
    {
        StartCoroutine(TakeScreenshotCoroutine());
    }

    private IEnumerator TakeScreenshotCoroutine()
    {
        canvas.enabled = false;

        string fileName = outputPath + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        if (File.Exists(fileName + ImageExtension))
        {
            int fileIndex = 2;
            while (File.Exists(fileName + "_" + fileIndex + ImageExtension))
            {
                fileIndex++;
            }
            fileName = fileName + "_" + fileIndex;
        }

        ScreenCapture.CaptureScreenshot(fileName + ImageExtension);

        yield return null;

        canvas.enabled = true;
    }
}
