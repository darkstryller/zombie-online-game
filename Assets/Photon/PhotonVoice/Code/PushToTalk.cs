using UnityEngine;
using Photon.Voice.Unity;

public class PushToTalk : MonoBehaviour
{
    public Recorder recorder;

    void Update()
    {
        recorder.TransmitEnabled = Input.GetKey(KeyCode.E);
    }
}