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

    // �f�[�^�x�[�X�̏�����
    void InitializeDatabase()
    {
        // �f�[�^�x�[�X�̃p�X
        string dbPath = Application.persistentDataPath + "/myDatabase.db";

        // �f�[�^�x�[�X�ڑ��̊m��
        dbConnection = new SQLiteConnection(dbPath);
        dbConnection.CreateTable<Person>(); // Person�e�[�u���̍쐬
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

    // �f�[�^�̎擾
    public Person GetPersonById(int id)
    {
        return dbConnection.Table<Person>().Where(p => p.Id == id).FirstOrDefault();
    }
}