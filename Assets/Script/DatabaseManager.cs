using UnityEngine;
using SQLite4Unity3d;

public class DatabaseManager : MonoBehaviour
{
    private SQLiteConnection dbConnection;

    void Awake()
    {
        InitializeDatabase();
        Debug.Log("Database connection status: " + (dbConnection != null ? "Connected" : "Not connected"));
    }

    // データベースの初期化
    void InitializeDatabase()
    {
        // データベースのパス
        string dbPath = Application.persistentDataPath + "/myDatabase.db";

        // データベース接続の確立
        dbConnection = new SQLiteConnection(dbPath);
        dbConnection.CreateTable<Person>(); // Personテーブルの作成
        Debug.Log("Database path: " + dbPath);

        // 他にも初期化処理があればここで実行
    }

    void OnDestroy()
    {
        // アプリケーション終了時にデータベースを閉じる
        if (dbConnection != null)
        {
            dbConnection.Close();
        }
    }

    public void ConnectToDatabase()
    {
        InitializeDatabase();
    }

    // データの追加
    public void AddPerson(string name, string surname, int age)
    {
        var person = new Person
        {
            Name = name,
            Surname = surname,
            Age = age
        };

        dbConnection.Insert(person);
    }

    // データの取得
    public Person GetPersonById(int id)
    {
        return dbConnection.Table<Person>().Where(p => p.Id == id).FirstOrDefault();
    }
}