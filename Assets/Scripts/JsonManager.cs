using System.IO;
using UnityEngine;

public static class JsonManager
{
    private static readonly string _jsonFileName = "SaveWithYears.json";

    public static void ExportJson(TransactionYear[] dataToSave)
    {
        string filePath = DataPath();
        CheckFileExistance(filePath);
        string newSave = Utilities.JsonHelper.ToJson(dataToSave, true);
        File.WriteAllText(filePath, newSave);
    }

    public static TransactionYear[] ImportJson()
    {
        string originalSave;
        /*#if UNITY_EDITOR
        originalSave = File.ReadAllText(_jsonSaveWithYearsPath);
        #elif UNITY_ANDROID
        WWW reader = new WWW(_jsonSaveWithYearsPath);
        while(!reader.isDone){}
        originalSave = reader.text;
        #endif*/

        string filePath = DataPath();
        CheckFileExistance(filePath);
        originalSave = File.ReadAllText(filePath);

        if (originalSave == "{}" || string.IsNullOrEmpty(originalSave)) return new TransactionYear[0];

        var transactionYears = Utilities.JsonHelper.FromJson<TransactionYear>(originalSave);
        return transactionYears;
    }

    static string DataPath()
    {
        if (Directory.Exists(Application.persistentDataPath))
        {
            Debug.Log(Path.Combine(Application.persistentDataPath, _jsonFileName));
            return Path.Combine(Application.persistentDataPath, _jsonFileName);
        }
        Debug.Log(Path.Combine(Application.streamingAssetsPath, _jsonFileName));
        return Path.Combine(Application.streamingAssetsPath, _jsonFileName);
    }

    static void CheckFileExistance(string filePath, bool isReading = false)
    {
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
            /*if (isReading)
            {
                SetStartingData();
                string dataString = JsonUtility.ToJson(savedData);
                File.WriteAllText(filePath, dataString);
            }*/
        }
    }
}
