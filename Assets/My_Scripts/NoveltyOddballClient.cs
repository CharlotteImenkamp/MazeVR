using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using System.IO;


public class NoveltyOddballClient : MonoBehaviour
{
// initialization
    internal Boolean ClientReady = false;
    TcpClient myClient;
    NetworkStream theStream;
    StreamWriter theWriter;
    StreamReader theReader;
    String Host = "127.0.0.1";  //vorher: "localhost"
    Int32 Port = 55001;
    string data; 

// Start
    void Start()
    {
        setupClient();
        Debug.Log("Client is set up");

        closeSocket();

        
    }

// Update
    void Update()
    {
    }

    public void setupClient()
    {
        try
        {
            myClient = new TcpClient(Host, Port);
            theStream = myClient.GetStream();
            theWriter = new StreamWriter(theStream);
            theReader = new StreamReader(theStream);
            ClientReady = true;
            data = readSocket();
            Debug.Log(data);
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e);
        }
    }

    public String readSocket()
    {
        if (!ClientReady)
        {
            return "client not ready";
        }
        else
        {
            return theReader.ReadLine();
        }
    }

    public void closeSocket()
    {
        if (!ClientReady)
            return;
        theWriter.Close();
        theReader.Close();
        myClient.Close();
        ClientReady = false;
    }




}


