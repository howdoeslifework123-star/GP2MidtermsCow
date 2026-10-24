using UnityEngine;

public enum GameColor { Yellow, Green, Gray }

public static class GameColorExtensions
{
    public static Color ToColor(this GameColor c) => c switch
    {
        GameColor.Yellow => Color.yellow,
        GameColor.Green => Color.green,
        _ => Color.gray
    };

    
    public static void Tint(this GameObject go, Color color)
    {
        foreach (var r in go.GetComponentsInChildren<Renderer>())
            r.Tint(color);
    }

    
    public static void Tint(this Renderer r, Color color)
    {
        foreach (var m in r.materials)
            m.color = color;
    }
}