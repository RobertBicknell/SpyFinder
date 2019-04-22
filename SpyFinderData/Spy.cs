using System;
using System.ComponentModel.DataAnnotations;

public class Spy
{
    private string _name;
    [Key]
    public string name
    {
        get { return _name; }
        set
        {
            if (string.IsNullOrEmpty(value)) //required as npgsql assigns the object's hash GUID to name when name is null instead of failing the NOT NULL constraint - bug - could also resolve by renaming 'name'
            {
                throw new Exception("name cannot be null or empty");
            }
            _name = value;
        }
    }

    private int[] _code;
    public int[] code
    {
        get { return _code; }
        set
        {
            if (value.Length == 0)
            {
                throw new Exception("code cannot be empty");
            }
            _code = value;
        }
    }
}