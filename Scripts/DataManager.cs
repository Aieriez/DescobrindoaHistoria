using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using Mono.Data;
using System.Data;

public class DataManager : MonoBehaviour
{
    public static DataManager data;
    //Quests
    public string text; 
    public string answer;
    public float difficulty;
    int count;
    [SerializeField]
    private string DataBaseName;
    //Rank
    [SerializeField]
    private long pos;
    [SerializeField]
    private string namerk;
    [SerializeField]
    private long score; 
                        

    void Awake()
    {
        if (data == null)
        {
            data = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    void Start ()
    {
        if(!CURRENTSCENE.instance.levelName.Equals("Menu_Iniciar") && !CURRENTSCENE.instance.levelName.Equals("Menu_Levels") && !CURRENTSCENE.instance.levelName.Equals("Ranking"))
        {
            DataSize();
            GAMEMANAGER.instance.dataSize = count;
        }
    }

    public void DataReader(int id)
    {    
        string conn = SetDataBaseClass.SetDataBase(DataBaseName +".db");
        IDbConnection dbconn;
        IDbCommand cmd;
        IDataReader reader;

        using (dbconn = new SqliteConnection(conn))
        {   
            dbconn.Open();
            using (cmd = dbconn.CreateCommand())
            {
                string sqlQuery = "SELECT Text, Answer, Difficulty FROM Quests WHERE Id_quest = '" + id + "'";
                cmd.CommandText = sqlQuery;
                reader = cmd.ExecuteReader();
                while (reader.Read()){
                    text = reader["Text"].ToString();
                    answer = reader["Answer"].ToString();
                    difficulty = (float)reader["Difficulty"];    
                }
                reader.Close();
                reader = null;
               
            } 
            cmd.Dispose();
            cmd = null;
        }
        dbconn.Close();
        dbconn = null;
    }

    public void DataSize()
    {
        string conn = SetDataBaseClass.SetDataBase(DataBaseName + ".db");
        IDbConnection dbconn;
        IDbCommand cmd;
        IDataReader reader;
        
        using (dbconn = new SqliteConnection(conn))
        {   
            dbconn.Open();
            using (cmd = dbconn.CreateCommand())
            {
                string sqlQuery = "SELECT id_quest FROM Quests;";
                cmd.CommandText = sqlQuery;
                reader = cmd.ExecuteReader();
                
                while (reader.Read()){
                
                    count++;
                    
                }
                
                reader.Close();
                reader = null;
               
            } 
            cmd.Dispose();
            cmd = null;
        }
        dbconn.Close();
        dbconn = null;
    }

    public void RankingReader()
    {    
        string conn = SetDataBaseClass.SetDataBase(DataBaseName +".db");
        IDbConnection dbconn;
        IDbCommand cmd;
        IDataReader reader;

        using (dbconn = new SqliteConnection(conn))
        {   
            dbconn.Open();
            using (cmd = dbconn.CreateCommand())
            {
                string sqlQuery = "SELECT name, Score FROM Players ORDER BY Score DESC";
                cmd.CommandText = sqlQuery;
                reader = cmd.ExecuteReader();
                int id = 0;
                while(reader.Read())
                {   
                    if (id < 10)
                    {
                        namerk = reader["Name"].ToString();
                        score = (long)reader["Score"];
                        RankManager.instance.AddToRankList(id, namerk, score);
                        id++; 
                    }
                    else
                    {
                        break;
                    }
                    
                }
                reader.Close();
                reader = null;
            } 
            cmd.Dispose();
            cmd = null;
        }
        dbconn.Close();
        dbconn = null;
    }

    public void InsertIntoRank(string player, int totalScore)
    {    
        string conn = SetDataBaseClass.SetDataBase(DataBaseName +".db");
        IDbConnection dbconn;
        IDbCommand cmd;
        
        using (dbconn = new SqliteConnection(conn))
        {   
            dbconn.Open();
            using (cmd = dbconn.CreateCommand())
            {
                string sql = "INSERT INTO Players (Name, Score) VALUES ('"+ player +"', " + totalScore +")";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            cmd.Dispose();
            cmd = null;
        }
        dbconn.Close();
        dbconn = null;
    }

}


