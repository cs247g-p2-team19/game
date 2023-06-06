using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Debug = UnityEngine.Debug;

/// <summary>
/// Provides methods to interact with the user (and their computer)
/// in strictly non-destructive but unexpected ways, 
/// "reaching out" of the game.
/// </summary>
public class Meta
{
    /// <summary>
    /// Attempts to get the player's real name from their computer. 
    /// Throws a NotImplementedException if the platform isn't supported.
    /// </summary>
    /// <returns>The user's real name</returns>
    public static string GetRealPlayerName() {
        Process nameFinder = new Process();
        string foundName;
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            nameFinder.StartInfo.FileName = "net.exe";
            nameFinder.StartInfo.Arguments = "user \"" + Environment.UserName + "\"";
            nameFinder.StartInfo.UseShellExecute = false;
            nameFinder.StartInfo.RedirectStandardOutput = true;
            nameFinder.StartInfo.CreateNoWindow = true;
            nameFinder.Start();
            string winUserData = nameFinder.StandardOutput.ReadToEnd();
            Regex rgx = new Regex("^Full Name\\s+([^\n]+)$", RegexOptions.Multiline);
            MatchCollection matches = rgx.Matches(winUserData);
            Debug.Log(matches[0].Groups[1].Value);
            Debug.Log(winUserData);
            foundName = matches[0].Groups[1].Value.Trim();
#elif UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
            nameFinder.StartInfo.FileName = "dscl";
            nameFinder.StartInfo.Arguments = ". read /Users/" + Environment.UserName + " RealName";
            nameFinder.StartInfo.UseShellExecute = false;
            nameFinder.StartInfo.RedirectStandardOutput = true;
            nameFinder.StartInfo.CreateNoWindow = true;
            nameFinder.Start();
            Regex rgx = new Regex("^RealName:\\s+([^\n]+)$");
            MatchCollection matches = rgx.Matches(nameFinder.StandardOutput.ReadToEnd());
            foundName = matches[0].Groups[1].Value.Trim();
#elif UNITY_STANDALONE_LINUX
            nameFinder.StartInfo.FileName = "getent";
            nameFinder.StartInfo.Arguments = "passwd " + Environment.UserName;
            nameFinder.StartInfo.UseShellExecute = false;
            nameFinder.StartInfo.RedirectStandardOutput = true;
            nameFinder.StartInfo.CreateNoWindow = true;
            nameFinder.Start();
            string lnxUserData = nameFinder.StandardOutput.ReadToEnd();
            Regex rgx = new Regex("([-A-Za-z0-9_/ ]+)");
            foundName = rgx.Matches(lnxUserData)[5].Value;
#else
        throw new NotImplementedException("This system doesn't support this operation.");
#endif
        return foundName;
    }

    public static string GetRealPlayerNameOrElse(string fallback) {
        try {
            return GetRealPlayerName();
        }
        catch (NotImplementedException) {
            return fallback;
        }
    }
}