using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class ArduinoInput : MonoBehaviour
{
	[SerializeField] private string comNumber = "/dev/cu.usbserial-A603H3P3";
	[SerializeField] private int baudRate = 9600;
	
	private SerialPort stream;
	private bool active;
	
	private void Awake()
	{
		foreach (var portName in SerialPort.GetPortNames())
			Debug.Log (portName);
		stream = new SerialPort(comNumber, baudRate);
	}
	
	private void OnEnable()
	{
		// Object starts? Open stream, start thread
		stream.Open();
	}
	
	private void OnDisable()
	{
		// Object is deactivated/destroyed? Close stream, abort thread
		stream.Close();
	}

	private void Update()
	{
		if (Input.anyKeyDown) {
			active = !active;
			UpdateValue();
		}
	}

	private void UpdateValue()
	{
		if (active) {
			stream.Write (new byte[] { 1 }, 0, 1);
		} else {
			stream.Write (new byte[] { 0 }, 0, 1);
		}
	}
}