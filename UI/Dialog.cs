using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RemptyTool.ES_MessageSystem;

public class Dialog : MonoBehaviour
{
    public static bool isDiaPausing, isDiaWaiting;
    public static bool isGD, is003, isEND;

    public GameObject ChenImage, ToneImage, WangImage, GuoDongImage, OO3Image, dialogPan;
    public Sprite ChenSprite, ToneSprite, ChenBlinkSprite, ChenCrySprite, ToneAkwardSprite, GuoDongSprite, GDCrySprite, GDAngrySprite, OO3Sprite;
    private List<string> textList = new List<string>();
    public int textIndex = 0;
    private int ChenIndex;
    private bool chk;

    [Header("Asset")]
    public bool skip;
    private ES_MessageSystem msgSys;
    public UnityEngine.UI.Text uiText;
    
    public TextAsset emptyText, plotTutorial, plotGD, plot003, plotEND, plotEND100;
    static public TextAsset textAsset;
    public TextAsset[] chen;
    public GameObject[] chenDieAudio;

    public AudioClip[] plot_audio;
    AudioSource AS;

    private void OnEnable()
    {
        ReadTextDataFromAsset(textAsset);
        textIndex = 0;
    }
    private void Awake()
    {
        textAsset = plotTutorial;
    }
    void Start()
    {
        AS = GetComponent<AudioSource>();
        //textAsset = emptyText;
        if (skip)
        {
            textAsset = emptyText;
            PlayerController.toneScore = 1;
        }

        

        isDiaPausing = false;
        msgSys = this.GetComponent<ES_MessageSystem>();
        if (uiText == null)
        {
            Debug.LogError("UIText Component not assign.");
        }
        else ReadTextDataFromAsset(textAsset);

        

        msgSys.AddSpecialCharToFuncMap("START", DiaStart);
        msgSys.AddSpecialCharToFuncMap("WAIT", DiaWait);
        msgSys.AddSpecialCharToFuncMap("CLEAR", Clear);
        msgSys.AddSpecialCharToFuncMap("END", DiaEnd);
        msgSys.AddSpecialCharToFuncMap("RESPAWN", Respawn);
        msgSys.AddSpecialCharToFuncMap("THEEND", TELEPORT);
        msgSys.AddSpecialCharToFuncMap("Wang", WangSpeaking);
        msgSys.AddSpecialCharToFuncMap("Chen", ChenSpeaking);
        msgSys.AddSpecialCharToFuncMap("ChenBlink", ChenBlink);
        msgSys.AddSpecialCharToFuncMap("ChenCry", ChenCry);
        msgSys.AddSpecialCharToFuncMap("Tone", ToneSpeaking);
        msgSys.AddSpecialCharToFuncMap("ToneAkward", ToneAkward);
        msgSys.AddSpecialCharToFuncMap("GD", GuoDongSpeaking);
        msgSys.AddSpecialCharToFuncMap("GDcry", GDcry);
        msgSys.AddSpecialCharToFuncMap("GDangry", GDangry);
        msgSys.AddSpecialCharToFuncMap("003", OO3);

        msgSys.AddSpecialCharToFuncMap("AU0", AU0);
        msgSys.AddSpecialCharToFuncMap("AU1", AU1);
        msgSys.AddSpecialCharToFuncMap("AU2", AU2);
        msgSys.AddSpecialCharToFuncMap("AU3", AU3);
        msgSys.AddSpecialCharToFuncMap("AU4", AU4);
        msgSys.AddSpecialCharToFuncMap("AU5", AU5);
        msgSys.AddSpecialCharToFuncMap("AU6", AU6);
        msgSys.AddSpecialCharToFuncMap("AU7", AU7);
        msgSys.AddSpecialCharToFuncMap("AU8", AU8);
        msgSys.AddSpecialCharToFuncMap("AU9", AU9);
        msgSys.AddSpecialCharToFuncMap("AU10", AU10);
        msgSys.AddSpecialCharToFuncMap("AU11", AU11);

        msgSys.AddSpecialCharToFuncMap("DEBUG", DEBUG);

    }


    public void Respawn()
    {
        isDiaPausing = false;
        PlayerController.playerDead = false;
        PlayerController.deathCount++;
        SceneManager.LoadScene("World");
    }
    private void DiaStart()
    {
        isDiaWaiting = false;
        dialogPan.SetActive(true);
        isDiaPausing = true;
        
        this.gameObject.SetActive(true);
    }
    private void DiaEnd()
    {
        isDiaPausing = false;
        dialogPan.SetActive(false);
        this.gameObject.SetActive(false);
        chk = false;
    }
    private void DiaWait()
    {
        isDiaPausing = false;
        dialogPan.SetActive(false);
        isDiaWaiting = true;
    }
    private void DEBUG()
    {
        ReadTextDataFromAsset(textAsset);
    }
    private void TELEPORT()
    {
        SceneManager.LoadScene("Ending");
    }

    //Speaking UI
    private void Clear()
    {
        ChenImage.SetActive(false);
        ToneImage.SetActive(false);
        WangImage.SetActive(false);
        GuoDongImage.SetActive(false);
        OO3Image.SetActive(false);
    }
    private void WangSpeaking()
    {
        Clear();
        WangImage.SetActive(true);
    }
    private void ChenSpeaking()
    {
        Clear();
        ChenImage.GetComponent<Image>().sprite = ChenSprite;
        ChenImage.SetActive(true);
    }
    private void ChenBlink()
    {
        Clear();
        ChenImage.GetComponent<Image>().sprite = ChenBlinkSprite;
        ChenImage.SetActive(true);
    }
    private void ChenCry()
    {
        Clear();
        ChenImage.GetComponent<Image>().sprite = ChenCrySprite;
        ChenImage.SetActive(true);
    }
    private void ToneSpeaking()
    {
        Clear();
        ToneImage.GetComponent<Image>().sprite = ToneSprite;
        ToneImage.SetActive(true);
    }
    private void ToneAkward()
    {
        Clear();
        ToneImage.GetComponent<Image>().sprite = ToneAkwardSprite;
        ToneImage.SetActive(true);
    }
    private void GuoDongSpeaking()
    {
        Clear();
        GuoDongImage.GetComponent<Image>().sprite = GuoDongSprite;
        GuoDongImage.SetActive(true);
    }
    private void GDcry()
    {
        Clear();
        GuoDongImage.GetComponent<Image>().sprite = GDCrySprite;
        GuoDongImage.SetActive(true);
    }
    private void GDangry()
    {
        Clear();
        GuoDongImage.GetComponent<Image>().sprite = GDAngrySprite;
        GuoDongImage.SetActive(true);
    }
    private void OO3()
    {
        Clear();
        OO3Image.GetComponent<Image>().sprite = OO3Sprite;
        OO3Image.SetActive(true);
    }
    private void AU0()
    {
        AS.clip = plot_audio[0];
        AS.Play();
    }
    private void AU1()
    {
        AS.clip = plot_audio[1];
        AS.Play();
    }
    private void AU2()
    {
        AS.clip = plot_audio[2];
        AS.Play();
    }
    private void AU3()
    {
        AS.clip = plot_audio[3];
        AS.Play();
    }
    private void AU4()
    {
        AS.clip = plot_audio[4];
        AS.Play();
    }
    private void AU5()
    {
        AS.clip = plot_audio[5];
        AS.Play();
    }
    private void AU6()
    {
        AS.clip = plot_audio[6];
        AS.Play();
    }
    private void AU7()
    {
        AS.clip = plot_audio[7];
        AS.Play();
    }
    private void AU8()
    {
        AS.clip = plot_audio[8];
        AS.Play();
    }
    private void AU9()
    {
        AS.clip = plot_audio[9];
        AS.Play();
    }
    private void AU10()
    {
        AS.clip = plot_audio[10];
        AS.Play();
    }
    private void AU11()
    {
        AS.clip = plot_audio[11];
        AS.Play();
    }

    private void ReadTextDataFromAsset(TextAsset _textAsset)
    {
        textList.Clear();
        textList = new List<string>();
        textIndex = 0;
        var lineTextData = _textAsset.text.Split('\n');
        foreach (string line in lineTextData)
        {
            textList.Add(line);
        }
    }

    void Update()
    {

        if (PlayerController.playerDead)
        {
            if (!chk)
            {
                isDiaWaiting = false;
                textIndex = 0;
                ChenIndex = Random.Range(0, chen.Length);
                textAsset = chen[ChenIndex];
                chenDieAudio[ChenIndex].GetComponent<AudioSource>().Play();
                ReadTextDataFromAsset(textAsset);
                chk = true;
            }
        }

        if (isGD)
        {
            isDiaWaiting = false;
            textIndex = 0;
            textAsset = plotGD;
            ReadTextDataFromAsset(textAsset);
            isGD = false;
        }

        if (is003)
        {
            isDiaWaiting = false;
            textIndex = 0;
            textAsset = plot003;
            ReadTextDataFromAsset(textAsset);
            is003 = false;
        }

        if (isEND)
        {
            isDiaWaiting = false;
            textIndex = 0;

            textAsset = (counter.subscriber > 1000000) ? plotEND100 : plotEND;
            ReadTextDataFromAsset(textAsset);
            isEND = false;
        }

        if (isDiaWaiting) return;
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !PlayerController.isPausing)
        {
            //Continue the messages, stoping by [w] or [lr] keywords.
            msgSys.Next();
        }

        //If the message is complete, stop updating text.
        if (msgSys.IsCompleted == false)
        {
            uiText.text = msgSys.text;
        }

        //Auto update from textList.
        if (msgSys.IsCompleted == true && textIndex < textList.Count)
        {
            msgSys.SetText(textList[textIndex]);
            textIndex++;
        }
    }
}
