using UnityEngine;
using Photon.Voice.Unity;
using Photon.Voice.PUN;

public class VoiceSetup : MonoBehaviour
{
    public PhotonVoiceView voiceView;
    public Recorder recorder;
    public Speaker speaker;

    void Start()
    {
        voiceView.RecorderInUse = recorder;
        voiceView.SpeakerInUse = speaker;
    }
}
