using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadScript : MonoBehaviour
{
    public static SaveData data = new SaveData();
    public string fileName;

    public Transform player;
    public UnityEngine.UI.Slider pitchSlider;

    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    public void Save()
    {
        data.pitchValue = pitchSlider.value;
        data.SetPlayerPos(player.position);

        SaveFile(SaveLoadScript.data);
    }

    public void Load()
    {
        if (LoadFile(out data))
        {
            pitchSlider.value = data.pitchValue;
            player.position = data.GetPlayerPos();
        }
    }

    public void SaveFile(SaveData data)
    {
        //Makes the savefile
        FileStream file = new FileStream(Application.persistentDataPath + "/" + fileName, FileMode.OpenOrCreate);

        //This puts the variables from data into the file
        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(file, data);

        //This closes the file so there's no resource conflicts
        file.Close();
    }

    public bool LoadFile(out SaveData data)
    {
        if (! System.IO.File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            Debug.Log("Load file was not found.");
            data = new SaveData();
            return false;
        }

        //Open the file
        FileStream file = new FileStream(Application.persistentDataPath + "/" + fileName, FileMode.Open);

        //
        BinaryFormatter converter = new BinaryFormatter();
        data = (SaveData)converter.Deserialize(file);

        file.Close();

        return true;
    }
}
