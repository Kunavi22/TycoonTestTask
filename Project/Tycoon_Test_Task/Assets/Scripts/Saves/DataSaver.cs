using System.IO;
using UnityEngine;


public static class DataSaver
{
    const string SaveDataFolder = "Saves";
    const string SaveDataExtention = ".dat";
    const bool EncryptSaves = true;


    public static void SaveData(object data) => SaveData(data, EncryptSaves);
    private static void SaveData(object data, bool encrypt)
    {
        string saveName = data.GetType().ToString();
        string pathToFolder = Application.persistentDataPath + '/' + SaveDataFolder;

        string fullPath = pathToFolder + '/' + saveName + SaveDataExtention;

        if (!Directory.Exists(pathToFolder))
            Directory.CreateDirectory(pathToFolder);

        string saveString = JsonUtility.ToJson(data);
        //if (encrypt)
        //saveString = Encryptor.Encrypt(saveString);

        using (BinaryWriter writer = new BinaryWriter(File.Open(fullPath, FileMode.Create)))
        {
            writer.Write(saveString);
            //Debug.Log($"Data [{saveName}] has been saved!");
        }
    }

    public static bool LoadData<T>(out T data) => LoadData<T>(out data, EncryptSaves);
    private static bool LoadData<T>(out T data, bool encrypt)
    {
        string saveName = typeof(T).ToString();
        string fullPath = Application.persistentDataPath + '/' + SaveDataFolder + '/' + saveName + SaveDataExtention;

        if (!File.Exists(fullPath))
        {
            //Debug.Log($"Save data not found! [{fullPath}]");
            data = default;
            return false;
        }

        try
        {
            using (BinaryReader reader = new BinaryReader(File.Open(fullPath, FileMode.Open)))
            {
                string saveString = reader.ReadString();

                //if (encrypt)
                //saveString = Encryptor.Decrypt(saveString);

                data = JsonUtility.FromJson<T>(saveString);
                return true;
            }
        }
        catch
        {
            data = default;
            return false;
        }
    }

    public static void ClearAllData()
    {
        string pathToFolder = Application.persistentDataPath + '/' + SaveDataFolder;

        if (!Directory.Exists(pathToFolder))
            return;

        DirectoryInfo dirInfo = new DirectoryInfo(pathToFolder);

        foreach (FileInfo file in dirInfo.GetFiles())
        {
            file.Delete();
        }
    }
}


public interface IDataSaveble
{
    public void Save();

    public virtual bool Load<T>(out T data)
    {
        return DataSaver.LoadData(out data);
    }
}