using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using System.Collections;
using UnityOSC;

public class OSCConnection2 : MonoBehaviour
{
    public const string ServerName = "PD-in";
	public const string ClientName = "PD-out";

    public int serverIP = 5011;
    public Transform sphere;
    public Vector3 offset;
    public float multiplier = 1;

	public int upValue = 200;
	public int downValue = -200;
	public int rightValue = 200;
	public int leftValue = -200;

    private OSCServer server;
    private Vector3 acc;
	public float mellowScore;
	public int blink;
	public string isMuseOn;

	//Gameplay elements
	public int upDownDirection = 0; //0 = neutral, 1 = up (-350), 2 = down (250)
	public int leftRightDirection = 0; //0 = neutral, 1 = left (), 2 = right ()

    private void Awake()
    {
        OSCHandler.Instance.CreateServer(ServerName, serverIP);
		OSCHandler.Instance.CreateClient(ClientName, IPAddress.Parse("169.254.50.81"), 6000);

        var pd = OSCHandler.Instance.Servers[ServerName];
        server = pd.server;
    }

    private void OnEnable()
    {
        server.PacketReceivedEvent += OnPacketReceivedEvent;
    }

    private void OnDisable()
    {
        server.PacketReceivedEvent -= OnPacketReceivedEvent;
    }

    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			//OSCHandler.Instance.SendMessageToClient(ClientName, "/creature/mellow", new List<int> { UnityEngine.Random.Range(0, 10) });
			//OSCHandler.Instance.SendMessageToClient(ClientName, "/creature/turn", new List<int> { UnityEngine.Random.Range(0, 10) });
			//OSCHandler.Instance.SendMessageToClient(ClientName, "/creature/players", new List<int> { UnityEngine.Random.Range(0, 10) });
		}
		lock (this) {
			//sphere.localPosition = (acc + offset) * multiplier;
			//sphere.localRotation = Quaternion.Euler (0, 0, -acc.z * multiplier);
			//sphere.localRotation = Quaternion.Euler (-acc.x * multiplier, 0, 0);

			//DIRECTION - Up and Down 
			if (acc.x > upValue) {		// Going down
				sphere.localRotation = Quaternion.Euler(-50, 0, 0);
				upDownDirection = 2;
			}
			else if (acc.x < downValue) { // Going up
				sphere.localRotation = Quaternion.Euler(50, 0, 0);
				upDownDirection = 1;
			}
			//DIRECTION - Left and Right
			else if (acc.z > rightValue) {
				sphere.localRotation = Quaternion.Euler(0, 0, -45);
				leftRightDirection = 2;
			}
			else if (acc.z < leftValue) {
				sphere.localRotation = Quaternion.Euler(0, 0, 45);
				leftRightDirection = 1;
			}
			// DIRECTION - No direction
			else {
				sphere.localRotation = Quaternion.Euler(0, 0, 0);
				upDownDirection = 0;
				leftRightDirection = 0;
			}
		}
    }

    private void OnPacketReceivedEvent(OSCServer sender, OSCPacket packet)
    {
		lock (this) {
			// Send something from PureData and it shows up in the Unity console
			if (packet.Address.Equals ("/muse/acc")) {
				acc = new Vector3 ((float)packet.Data [0], (float)packet.Data [1], (float)packet.Data [2]);

				//Debug.Log (acc);

				//Debug.Log(packet.Address + ": " + DataToString(packet.Data));
			}
		}

		lock (this) {
			// Send something from PureData and it shows up in the Unity console
			if (packet.Address.Equals ("/muse/elements/blink")) {
				blink = ((int)packet.Data [0]);
				//Debug.Log (blink);
			}
		}

		lock (this) {
			// Send something from PureData and it shows up in the Unity console
			if (packet.Address.Equals ("/muse/elements/horseshoe")) {
				isMuseOn = DataToString(packet.Data);
			}
		}

		lock (this) {
			// Send something from PureData and it shows up in the Unity console
			if (packet.Address.Equals ("/muse/elements/experimental/mellow")) {
				mellowScore = ((float)packet.Data [0]);
				//Debug.Log (mellowScore);
			}
		}

			// Send something from PureData and it shows up in the Unity console
			//if (packet.Address.Equals ("/muse/elements/experimental/concentration")) {
			//	concentration = new Vector3 ((float)packet.Data [0], (float)packet.Data [1], (float)packet.Data [2]);
				
			//	Debug.Log (concentration);
				
				//Debug.Log(packet.Address + ": " + DataToString(packet.Data));
			//}
    }

    public static string DataToString(List<object> data)
	{
		var buffer = "";
		
		for (int i = 0; i < data.Count; i++)
		{
			buffer += data[i] + " ";
		}
		
		return buffer;
	}
}
