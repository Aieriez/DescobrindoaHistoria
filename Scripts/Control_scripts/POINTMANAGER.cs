using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class POINTMANAGER : MonoBehaviour
{
    public static POINTMANAGER instance;
    public int points;
	[SerializeField]
	private string filePath;
	[SerializeField]
	private int totalPoints;
	public static string playerName;

    void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad (this.gameObject);
		}
		else
		{
			Destroy (gameObject);
		}

		filePath = Application.persistentDataPath + "password.data";
	}

	public void SaveBestLevelPoints (string player, string level, int points)
	{
		if (!ZPlayerPrefs.HasKey(player+level))
		{
			ZPlayerPrefs.SetInt(player+level, points);
		}
		else
		{
			if (points > ZPlayerPrefs.GetInt(player+level, points))
			{
				ZPlayerPrefs.SetInt(player+level, points);
			}
		}
	}

	public int LoadBestLevelScore (string player, string level)
	{	
		if(ZPlayerPrefs.HasKey(player+level))
		{
			return ZPlayerPrefs.GetInt(player+level);
		}
		else
		{
			return 0;
		}	
	}
	public void SaveToData (string playerName)
	{
		for (int i = 1; i <= 5; i++)
		{
			totalPoints += ZPlayerPrefs.GetInt(playerName + "Level_"+i);
			ZPlayerPrefs.DeleteKey(playerName + "Level_"+i);
		}
		DataManager.data.InsertIntoRank(playerName, totalPoints);
	}

	[System.Serializable]
	public class Password
	{
		public string password;
	}

	private void SavePassword(string pass)
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream fs = File.Create(filePath);
			
		Password save = new Password();
		save.password = pass;
	
		bf.Serialize(fs, save);
		fs.Close();
		
	}
	public string LoadPassword()
	{
		string pass = "";
		if (File.Exists(filePath))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream fs = File.Open(filePath, FileMode.Open);

			Password save = bf.Deserialize(fs) as Password;
			fs.Close();

			pass = save.password;
		}
		else 
		{
			pass = alfanumericRandom(12);
			SavePassword(pass);
		}
		return pass;
	}

	//Gerador de Senhas
    public static string alfanumericRandom(int size)
    {
        var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var Charsarr = new char[size];
        string resultString;
        for (int i = 0; i < Charsarr.Length; i++)
        {
            Charsarr[i] = characters[Random.Range(0, characters.Length)];
        }

        resultString = string.Join("",Charsarr);
        return resultString;
    }
}
