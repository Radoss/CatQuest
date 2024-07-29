using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using System.Runtime.InteropServices.WindowsRuntime;

public class FileDataHandler
{
    private string _dataDirPath = "";
    private string _dataFileName = "";
    private string _fullPath = "";
    private bool _useEncryption = false;
    private readonly string _encryptionCodeWord = "abracadabra";


    public  FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        _dataDirPath = dataDirPath;
        _dataFileName = dataFileName;
        _fullPath = Path.Combine(dataDirPath, dataFileName);
        _useEncryption = useEncryption;
    }

    public GameData Load()
    {
        if (!File.Exists(_fullPath))
        {
            return null;
        }
        GameData gameData = null;
        try
        {
            string DataTxt = "";
            using (FileStream stream = new FileStream(_fullPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    DataTxt = reader.ReadToEnd();
                }
            }
            if (_useEncryption)
            {
                DataTxt = EncryptDecrypt(DataTxt);
            }
            gameData = Newtonsoft.Json.JsonConvert.DeserializeObject<GameData>(DataTxt);
        }
        catch (Exception e) 
        {
            Debug.LogException(e);
        }
        return gameData;
    }

    public void Save(GameData gameData) { 
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_fullPath));

            //string dataToStore = JsonUtility.ToJson(gameData, true);
            
            string dataToStore = Newtonsoft.Json.JsonConvert.SerializeObject(gameData);
            if(_useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (FileStream stream = new FileStream(_fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void DeleteSaveFile()
    {
        try
        {
            string path = Path.GetDirectoryName(_fullPath);
            File.Delete(_fullPath);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    private string EncryptDecrypt(string data)
    {
        string result = "";
        for(int i = 0; i< data.Length; i++)
        {
            result += (char) (data[i] ^ _encryptionCodeWord[i % _encryptionCodeWord.Length]);
        }
        return result; 
    }
}
