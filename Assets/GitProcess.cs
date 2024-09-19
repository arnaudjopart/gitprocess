using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GitProcess
{
    private static int m_numOutputLines;
    private static StringBuilder m_output;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        
        var process = System.Diagnostics.Process.Start(startInfo);
        string output = process.StandardOutput.ReadToEnd();
        UnityEngine.Debug.Log(output);
        process.WaitForExit();
        return output;
        /*
        Process gitProcess = new Process();
        gitProcess.StartInfo.FileName = "git";
        gitProcess.StartInfo.Arguments = arguments;
        gitProcess.StartInfo.WorkingDirectory = Application.dataPath;

        // Set UseShellExecute to false for redirection.
        gitProcess.StartInfo.UseShellExecute = false;

        // Redirect the standard output of the sort command.  
        // This stream is read asynchronously using an event handler.
        gitProcess.StartInfo.RedirectStandardOutput = true;
        m_output = new StringBuilder();
        m_numOutputLines = 0;
        
        gitProcess.OutputDataReceived += OutputHandler;

        // Redirect standard input as well.  This stream
        // is used synchronously.
        gitProcess.StartInfo.RedirectStandardInput = true;

        // Start the process.
        gitProcess.Start();

        // Start the asynchronous read of the sort output stream.
        gitProcess.BeginOutputReadLine();

        // Wait for the sort process to write the sorted text lines.
        gitProcess.WaitForExit();

        gitProcess.Close();

        Debug.Log(m_output.ToString());
        //return m_output.ToString();*/
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
}
