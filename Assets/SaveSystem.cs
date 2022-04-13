
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    public static void SaveData(GameHandler data){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData playerData = new PlayerData(data);
        formatter.Serialize(stream, playerData);
        stream.Close();
    }
    public static void SaveWordsList(GameHandler data){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/words.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        WordsListData wData = new WordsListData(data);
        formatter.Serialize(stream, wData);
        stream.Close();
    }
    public static PlayerData LoadData(){
        string path = Application.persistentDataPath + "/player.save";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else{
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
     public static WordsListData LoadWordsList(){
        string path = Application.persistentDataPath + "/words.save";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            WordsListData data = formatter.Deserialize(stream) as WordsListData;
            stream.Close();
            return data;
        }
        else{
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
