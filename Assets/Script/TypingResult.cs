using SQLite4Unity3d;
using Unity.Mathematics;

public class TypingResult
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int Point { get; set; }
    public int TypingCount { get; set; }

    public float Accuracy {  get; set; }

    public int Speed { get; set; }


    public override string ToString()
    {
        return string.Format("[TypingResult: Id={0}, Point={1},  TypingCount={2}, Accuracy = {3}, Speed={4}]", Id, Point, TypingCount, Accuracy, Speed);
    }
}
