using UnityEngine;
using SQLite4Unity3d;
using System;
using System.Collections.Generic;
using System.Linq;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }
    public SQLiteConnection dbConnection;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // このオブジェクトが破棄されないようにする
            InitializeDatabase();
        }
        else
        {
            Destroy(gameObject); // 既にインスタンスが存在する場合、新しいものを破棄する
        }

        Debug.Log("Database connection status: " + (dbConnection != null ? "Connected" : "Not connected"));
    }

    // データベースの初期化
    void InitializeDatabase()
    {
        // データベースのパス
        string dbPath = Application.persistentDataPath + "/myDatabase.db";

        // データベース接続の確立
        dbConnection = new SQLiteConnection(dbPath);

        dbConnection.CreateTable<TypingResult>();
        Debug.Log("Database path: " + dbPath);

        // 他にも初期化処理があればここで実行
        if (GetRecordCount() < 5)
        {
            InsertInitialData();
        }
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

    int GetRecordCount()
    {
        try
        {
            // TypingResultテーブルのレコード数を取得
            int count = dbConnection.Table<TypingResult>().Count();

            return count;
        }
        catch (Exception e)
        {
            Debug.LogError("Error getting record count: " + e.Message);
            return -1; // エラーが発生した場合は適切に処理するか、エラーコードを返すなど
        }
    }
    void InsertInitialData()
    {
        // 初期データを5つ挿入
        for (int i = 0; i < 5; i++)
        {
            AddResult(0, 0, 0f, 0f);
        }

        Debug.Log("Initial data added successfully.");
    }

    // データの追加
    public void AddResult(int point, int typingCount,float accuracy, float speed)
    {
        var typingResult = new TypingResult
        {
            Point = point,
            TypingCount = typingCount,
            Accuracy = accuracy,
            Speed = speed
            
        };

        try
        {
            int result = dbConnection.Insert(typingResult);

            if (result > 0)
            {
                Debug.Log("TypingResult added successfully.");
            }
            else
            {
                Debug.LogError("Failed to add TypingResult.");
            }

            
        }
        catch (Exception e)
        {
            Debug.LogError("Error adding TypingResult: " + e.Message);
        }
    }


    // データの取得
    public List<TypingResult> GetTypingResultsOrderedByPoint()
    {
        // OrderByDescendingメソッドを使用してPointの降順でソート
        return dbConnection.Table<TypingResult>().OrderByDescending(p => p.Point).ToList();
    }
}