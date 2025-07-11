using System.IO;
using UnityEngine;

public static class App
{
    public static readonly string SavePath = Path.Combine(Application.persistentDataPath, "save.json");
}