using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechRecognizer : MonoBehaviour {

    private SpeechRecognizerManager _speechManager = null;
    private string _message = " ";
    Queue myLogQueue = new Queue();
    private string[] text;

    public bool _isListening = false;
    public GameObject quote, starfield, starfieldred, christmas, lip, metoo, speaknow;
    //VolumeButton vb;

    const string AUDIO_SERVICE = "audio";
    const int STREAM_VOICE_CALL = 0;
    const int STREAM_SYSTEM = 1;
    const int STREAM_RING = 2;
    const int STREAM_MUSIC = 3;
    const int STREAM_ALARM = 4;
    const int STREAM_NOTIFICATION = 5;
    const int STREAM_DTMF = 8;

    #region MONOBEHAVIOUR

    // Use this for initialization
    void Start () {

        if (Application.platform != RuntimePlatform.Android)
        {
            Debug.Log("Speech recognition is only available on Android platform.");
            return;
        }

        if (!SpeechRecognizerManager.IsAvailable())
        {
            Debug.Log("Speech recognition is not available on this device.");
            return;
        }

        // We pass the game object's name that will receive the callback messages.
        _speechManager = new SpeechRecognizerManager(gameObject.name);

        quote.SetActive(false);
        starfield.SetActive(false);
        starfieldred.SetActive(false);
        metoo.SetActive(false);
        christmas.SetActive(false);
        lip.SetActive(false);
        speaknow.SetActive(false);

        //vb = gameObject.GetComponent<VolumeButton>();

        InvokeRepeating("RecogStart", 0f, 3f);

    }

    void OnDestroy()
    {
        if (_speechManager != null)
            _speechManager.Release();
    }

    #endregion

    #region SPEECH_CALLBACKS

    void OnSpeechResults(string results)
    {
        // Need to parse
        string[] texts = results.Split(new string[] { SpeechRecognizerManager.RESULT_SEPARATOR }, System.StringSplitOptions.None);
        text = texts;
        //DebugLog(" ");
        speaknow.SetActive(false);
        DebugLog("Speech results:\n   " + string.Join("\n   ", text));



        for (int i = 0; i < 5; i++)
        {
            // For remove effect
            if (text[i].IndexOf("remove the quote") >= 0)
            {
                quote.SetActive(false);
                break;
            }

            else if (text[i].IndexOf("remove the effect") >= 0)
            {
                starfield.SetActive(false);
                starfieldred.SetActive(false);
                break;
            }

            // For Quote
            else if (text[i].IndexOf("quote") >= 0)
            {
                quote.SetActive(true);
                break;
            }

            // For Scenario 1
            else if (text[i].IndexOf("Megan") >= 0)
            {
                starfield.SetActive(false);
                starfieldred.SetActive(true);
                SoundManager.instance.PlaySound(1, 0);
                Invoke("setListening", 16f);
                _isListening = true;
                break;
            }

            else if (text[i].IndexOf("song") >= 0)
            {
                SoundManager.instance.PlaySound(1, 1);
                Invoke("setListening", 15f);
                _isListening = true;
                break;
            }

            else if (text[i].IndexOf("video") >= 0)
            {
                metoo.SetActive(true);
                Invoke("MeToo", 16f);
                _isListening = true;
                break;
            }


            // For Scenario 2
            else if (text[i].IndexOf("mirror") >= 0)
            {
                starfieldred.SetActive(false);
                starfield.SetActive(true);
                SoundManager.instance.PlaySound(2, Random.Range(0, 3));
                Invoke("setListening", 2f);
                _isListening = true;
                break;
            }

            else if (text[i].IndexOf("look today") >= 0)
            {
                SoundManager.instance.PlaySound(2, Random.Range(3, 6));
                Invoke("setListening", 4f);
                _isListening = true;
                break;
            }

            else if (text[i].IndexOf("thank") >= 0)
            {
                SoundManager.instance.PlaySound(2, Random.Range(6, 9));
                Invoke("setListening", 3f);
                _isListening = true;
                break;
            }

            else if (text[i].IndexOf("recommend") >= 0)
            {
                SoundManager.instance.PlaySound(2, 9);
                Invoke("OnChristmas", 5f);
                Invoke("OffChristmas", 18.5f);
                _isListening = true;
                break;
            }

            else if (text[i].IndexOf("lip") >= 0)
            {
                SoundManager.instance.PlaySound(2, 10);
                Invoke("OnLip", 1f);
                Invoke("OffLip", 14f);
                _isListening = true;
                break;
            }
        }
    }

    void OnSpeechError(string error)
    {
        speaknow.SetActive(false);
        switch (int.Parse(error))
        {
            case SpeechRecognizerManager.ERROR_AUDIO:
                DebugLog("Error during recording the audio.");
                break;
            case SpeechRecognizerManager.ERROR_CLIENT:
                DebugLog("Error on the client side.");
                break;
            case SpeechRecognizerManager.ERROR_INSUFFICIENT_PERMISSIONS:
                DebugLog("Insufficient permissions. Do the RECORD_AUDIO and INTERNET permissions have been added to the manifest?");
                break;
            case SpeechRecognizerManager.ERROR_NETWORK:
                DebugLog("A network error occured. Make sure the device has internet access.");
                break;
            case SpeechRecognizerManager.ERROR_NETWORK_TIMEOUT:
                DebugLog("A network timeout occured. Make sure the device has internet access.");
                break;
            case SpeechRecognizerManager.ERROR_NO_MATCH:
                DebugLog("No recognition result matched.");
                break;
            case SpeechRecognizerManager.ERROR_NOT_INITIALIZED:
                DebugLog("Speech recognizer is not initialized.");
                break;
            case SpeechRecognizerManager.ERROR_RECOGNIZER_BUSY:
                //DebugLog("Speech recognizer service is busy.");
                break;
            case SpeechRecognizerManager.ERROR_SERVER:
                DebugLog("Server sends error status.");
                break;
            case SpeechRecognizerManager.ERROR_SPEECH_TIMEOUT:
                DebugLog("No speech input.");
                break;
            default:
                break;
        }

        _isListening = false;
    }

    #endregion

    #region VoiceRecog

    void RecogStart()
    {
        if (!_isListening)
        {
            speaknow.SetActive(true);
            DebugLog("Say Now");
            _speechManager.StartListening(5, "en-US");
        }
    }

    void MeToo()
    {
        metoo.SetActive(false);
        _isListening = false;
    }

    void OnChristmas()
    {
        christmas.SetActive(true);
    }

    void OffChristmas()
    {
        christmas.SetActive(false);
        _isListening = false;
    }

    void OnLip()
    {
        lip.SetActive(true);
    }

    void OffLip()
    {
        lip.SetActive(false);
        _isListening = false;
    }

    void setListening() {
        _isListening = false;
    }


    void OnGUI()
    {
        //GUILayout.Label(_message);
    }

    #endregion

    private void DebugLog(string message)
    {
        Debug.Log(message);
        _message = message;
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        _message = logString;
        string newString = "\n [" + type + "] : " + _message;
        myLogQueue.Enqueue(newString);
        if (type == LogType.Exception)
        {
            newString = "\n" + stackTrace;
            myLogQueue.Enqueue(newString);
        }
        _message = string.Empty;
        foreach (string mylog in myLogQueue)
        {
            _message += mylog;
        }
    }

}
