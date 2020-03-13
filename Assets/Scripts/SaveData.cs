using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveData : MonoBehaviour
{
	public static SaveData Instance;
	public GameObject field;
	public GameObject button;

	// Use this for initialization
	void Start()
	{
		LoadGame();
	}

	public void NewField ()
	{
		Instantiate(field, button.transform.position, Quaternion.identity, transform);
		button.transform.position -= new Vector3(0, 30, 0);
	}

	Data newSave()
	{
		
		Data data = new Data();
		for (int i = 0; i < transform.GetChild(0).childCount; i++)
		{
			data._dadosPessoais[i] = transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text;
		}
		for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
			data._habilidades[i] = int.Parse(transform.GetChild(1).GetChild(i).GetChild(2).GetComponent<Text>().text);
		}

		return data;
	}

	public void SaveGame()
	{
		// 1
		Data data = newSave();

		// 2
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/score.data");
		bf.Serialize(file, data);
		file.Close();

		// 3

		Debug.Log("Game Saved");
	}

	public void LoadGame()
	{
		// 1
		if (File.Exists(Application.persistentDataPath + "/score.data"))
		{

			// 2
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/score.data", FileMode.Open);
			Data data = (Data)bf.Deserialize(file);
			file.Close();

			// 4
			for (int i = 0; i < transform.GetChild(0).childCount; i++)
			{
				transform.GetChild(0).GetChild(i).GetComponent<InputField>().text = data._dadosPessoais[i];
			}
			for (int i = 0; i < transform.GetChild(1).childCount; i++)
			{
				transform.GetChild(1).GetChild(i).GetComponent<InputField>().text = data._habilidades[i].ToString();
			}

			Debug.Log("Game Loaded");

		}
		else
		{
			Debug.Log("No game saved!");
		}
	}

}
