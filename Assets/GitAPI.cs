using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GitAPI
{
    private static int m_numOutputLines;
    private static StringBuilder m_output;

    public static string Run(string arguments)
    {
        var startInfo = new ProcessStartInfo()
        {
            UseShellExecute = false,
            RedirectStandardOutput = true,
            FileName = "git",
            Arguments = arguments,
            WorkingDirectory = Application.dataPath
        };
        
        var process = Process.Start(startInfo);
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        return output;
    }
    
    private static void OutputHandler(object sendingProcess,
        DataReceivedEventArgs outLine)
    {
        // Collect the sort command output.
        if (!string.IsNullOrEmpty(outLine.Data))
        {
            if (m_numOutputLines > 0)
                m_output.Append(Environment.NewLine);

            m_numOutputLines++;

            // Add the text to the collected output.
            m_output.Append($"{outLine.Data}");
        }
    }

    public static void CreateReleaseTag()
    {
        var count  = Run(@"rev-list --count HEAD");
        Run("tag -a v1.0.0.r"+count+" -m \"my version 1.4\"");
    }
    public static string GetIncrementalCommit()
    {
        var count = Run(@"rev-list --count HEAD");
        return count;
    }

    public static string GetCurrentCommitHash()
    {
        var count = Run(@"rev-parse --verify HEAD");
        return count;
    }    
    public static string GetDiff()
    {
        var count = Run(@"diff");
        return count;
    }    
    
    public static string GetStatus()
    {
        var count = Run(@"status -s -b");
        return count;
    }
}

