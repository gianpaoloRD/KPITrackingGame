

using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class TargetThread : BaseThread
{
    ArrayList target = new ArrayList();
    ArrayList cam = new ArrayList();
    ArrayList degreeError = new ArrayList();
    string saveFilePath = null;


    public TargetThread(ArrayList target, ArrayList cam, string saveFilePath, ArrayList degreeError) : base()
    {
        
        this.target = target;
        this.cam = cam;
        this.degreeError = degreeError;
        this.saveFilePath = saveFilePath;
    }
    public override void RunThread()
    {
        WriteCsv();
    }

    public void WriteCsv()

    {

        //Debug.Log(Application.dataPath + "/GetData/"+ PlayerPrefs.GetString("name") + "_SummaryClickResult.csv");
        String dateCurrent = DateTime.Now.ToLongDateString().ToString().Replace("/", "_");
        String saveHere = saveFilePath + "testplayer" + "_" + dateCurrent.Replace(" ", "_") + "_SummaryTrackResult.csv"; //String saveHere = saveFilePath + PlayerPrefs.GetString("name") + "_" + dateCurrent.Replace(" ", "_") + "_SummaryTrackResult.csv";
        Debug.Log(saveHere);
        try
        {
            if (!Directory.Exists(saveFilePath))
            {
                Directory.CreateDirectory(saveFilePath);

            }

        }
        catch (IOException ex)
        {
            Console.WriteLine(ex.Message);
        }


        TextWriter tw = new StreamWriter(saveHere, false);
        tw.WriteLine("Date,Target_x,TArget_y,Target_z,camRotation_x,camRotation_y,camRotation_z,DegreeError");

        for (int i = 0; i < target.Count; ++i)
        {
            if (i < target.Count) tw.Write(i);
            tw.Write(",");
            if (i < target.Count) tw.Write(target[i].ToString().Replace(")", "").Replace("(", ""));
            tw.Write(",");
            if (i < cam.Count) tw.Write(cam[i].ToString().Replace(")", "").Replace("(", ""));
            tw.Write(",");
            if (i < degreeError.Count) tw.Write(degreeError[i]);
            tw.Write(",");
            tw.Write(System.Environment.NewLine);
        }

        tw.Flush();
        tw.Close();

    }



}