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
            DontDestroyOnLoad(gameObject); // ���̃I�u�W�F�N�g���j������Ȃ��悤�ɂ���
            InitializeDatabase();
        }
        else
        {
            Destroy(gameObject); // ���ɃC���X�^���X�����݂���ꍇ�A�V�������̂�j������
        }

        Debug.Log("Database connection status: " + (dbConnection != null ? "Connected" : "Not connected"));
    }

    // �f�[�^�x�[�X�̏�����
    void InitializeDatabase()
    {
        // �f�[�^�x�[�X�̃p�X
        string dbPath = Application.persistentDataPath + "/myDatabase.db";

        // �f�[�^�x�[�X�ڑ��̊m��
        dbConnection = new SQLiteConnection(dbPath);
        dbConnection.CreateTable<TypingResult>();
        Debug.Log("Database path: " + dbPath);

        // ���ɂ�����������������΂����Ŏ��s
    }

    void OnDestroy()
    {
        // �A�v���P�[�V�����I�����Ƀf�[�^�x�[�X�����
        if (dbConnection != null)
        {
            dbConnection.Close();
        }
    }

    public void ConnectToDatabase()
    {
        InitializeDatabase();
    }

    // �f�[�^�̒ǉ�
    public void AddResult(int point, int typingCount,float accuracy, int speed)
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


    // �f�[�^�̎擾
    public List<TypingResult> GetTypingResultsOrderedByPoint()
    {
        // OrderByDescending���\�b�h���g�p����Point�̍~���Ń\�[�g
        return dbConnection.Table<TypingResult>().OrderByDescending(p => p.Point).ToList();
    }
}