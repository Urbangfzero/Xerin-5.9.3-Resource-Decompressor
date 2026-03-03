#region Logger

using System.Drawing;

public static class Logger
{
    private static int startColumn = 0;
    private static int prefixWidth = 10;

    private static void TypeWrite(string prefix, string message, Color color, int delay = 5)
    {
        Colorful.Console.CursorLeft = startColumn;

        prefix = prefix.PadRight(prefixWidth);

        Colorful.Console.Write(prefix, Color.WhiteSmoke);

        foreach (char c in message)
        {
            Colorful.Console.Write(c.ToString(), color);
            Thread.Sleep(delay);
        }
        Colorful.Console.WriteLine();
    }

    public static void Info(string message)
    {
        TypeWrite("[INFO]", message, Color.Cyan);
    }

    public static void Success(string message)
    {
        TypeWrite("[SUCCESS]", message, Color.LimeGreen);
    }

    public static void Warn(string message)
    {
        TypeWrite("[WARN]", message, Color.Maroon);
    }

    public static void Error(string message)
    {
        TypeWrite("[ERROR]", message, Color.Red);
    }

    public static void Custom(string message, Color color)
    {
        TypeWrite("", message, color);
    }

    public static void Inline(string message, Color color)
    {
        TypeWrite("", message, color);
    }
}

#endregion