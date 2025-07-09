using Photon.Voice.Unity;
using UnityEngine;

public class PhotonVoiceNetwork : VoiceConnection
{
    private static PhotonVoiceNetwork instance;

    public static PhotonVoiceNetwork Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PhotonVoiceNetwork>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("PhotonVoiceNetwork");
                    instance = obj.AddComponent<PhotonVoiceNetwork>();
                }
            }
            return instance;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
