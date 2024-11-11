using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Chat : MonoBehaviour
{
    //---------------------------------------------------- Windows --------------------------------------------------
    public int WindowsmaxMessages = 25;
    public InputField WindowschatBox;
    public GameObject WindowsPanel;
    public GameObject WindowsChatButton;
    public GameObject windowsFiloChat, windowsGlobalChat;

    public ScrollRect WindowschatView, WindowsFilochatview;
    private TextGenerator WindowscontentTextGenerator, WindowsFilocontentTextGenerator;
    private TextGenerationSettings contentTextGeneratorSettings, contentTextGeneratorSettingsFilo;

    private float oneLineHeigth, filoOneLineHeigth;  // the hight of one line in the conent text, we need it for initial spacings
    private TextMeshProUGUI contentText, FilocontentText;
    public RectTransform WindowscontentTransform, WindowsFilocontentTransform;
    private float WindowscontentWidth, WindowsFilocontentWidth;

    private float emptyContentSize, filoemptyContentSize;
    private List<string> chatItems;
    private List<string> chatItems2;
    private List<string> chatItems3;
    public int maxMessagesCount;  // the max number of messages on the chat window
    public InputField WindowschatInputField;

    public GameObject FiloChatKapaliButton, GlobalChatKapaliButton, FiloChatAcikButton, GlobalChatAcikButton;
    //---------------------------------------------------- Android --------------------------------------------------
    public InputField chatBox;
    public GameObject Panel;
    public GameObject ChatButton;
    public GameObject LocalButonAcik, ServerButtonAcik;
    public GameObject LocalButonKpali, ServerButonKapali;
    public ScrollRect mobilchatView, mobilFilochatview;
    private TextGenerator mobilcontentTextGenerator, mobilFilocontentTextGenerator;

    private TextMeshProUGUI mobilcontentText, mobilFilocontentText;
    public RectTransform contentTransform, FilocontentTransform, mobilcontentTransform, mobilFilocontentTransform;
    private float mobilcontentWidth,mobilFiloContentWidth;

    public TextMeshProUGUI AnaYüzChat,AnaYüzKillTable;
    public int AnaYüzChatMaxMessageCount;
    public int AnaYüzKillTableChatMaxMessageCount;
    public GameObject AnayüzChatMenu;
    public GameObject LocalText, ServerText;
    public GameObject OyuniciUI;
    public InputField chatInputField;
    private List<string> filoChatItems;



    private void Start()
    {
#if UNITY_STANDALONE_WIN 
        contentText = WindowscontentTransform.GetComponent<TextMeshProUGUI>();
        FilocontentText = WindowsFilocontentTransform.GetComponent<TextMeshProUGUI>();
        WindowscontentTextGenerator = new TextGenerator();
        WindowsFilocontentTextGenerator = new TextGenerator();
        oneLineHeigth = WindowscontentTextGenerator.GetPreferredHeight(" ", contentTextGeneratorSettings);
        filoOneLineHeigth = WindowsFilocontentTextGenerator.GetPreferredHeight(" ", contentTextGeneratorSettingsFilo);
        WindowschatView.verticalNormalizedPosition = 0.0f;
        WindowsFilochatview.verticalNormalizedPosition = 0.0f;
        WindowscontentWidth = WindowscontentTransform.sizeDelta.x;
        WindowsFilocontentWidth = WindowsFilocontentTransform.sizeDelta.x;
        WindowscontentTransform.sizeDelta = new Vector2(WindowscontentWidth, GetComponent<RectTransform>().sizeDelta.y - WindowschatBox.GetComponent<RectTransform>().sizeDelta.y);
        WindowsFilocontentTransform.sizeDelta = new Vector2(WindowsFilocontentWidth, GetComponent<RectTransform>().sizeDelta.y - WindowschatBox.GetComponent<RectTransform>().sizeDelta.y);
        emptyContentSize = WindowscontentTransform.sizeDelta.y;
        filoemptyContentSize = WindowsFilocontentTransform.sizeDelta.y;
        chatItems = new List<string>();
        chatItems2 = new List<string>();
        chatItems3 = new List<string>();
        filoChatItems = new List<string>();

#endif
#if UNITY_ANDROID
        LocalButtonActive();
        mobilcontentText = mobilcontentTransform.GetComponent<TextMeshProUGUI>();
        mobilFilocontentText = mobilFilocontentTransform.GetComponent<TextMeshProUGUI>();
        mobilcontentTextGenerator = new TextGenerator();
        oneLineHeigth = mobilcontentTextGenerator.GetPreferredHeight(" ", contentTextGeneratorSettings);
        mobilFilocontentTextGenerator = new TextGenerator();
        filoOneLineHeigth = mobilFilocontentTextGenerator.GetPreferredHeight(" ", contentTextGeneratorSettingsFilo);
        mobilchatView.verticalNormalizedPosition = 0.0f;
        mobilFilochatview.verticalNormalizedPosition = 0.0f;
        mobilcontentWidth = mobilcontentTransform.sizeDelta.x;
        mobilFiloContentWidth = mobilFilocontentTransform.sizeDelta.x;
        mobilcontentTransform.sizeDelta = new Vector2(mobilcontentWidth, GetComponent<RectTransform>().sizeDelta.y - chatBox.GetComponent<RectTransform>().sizeDelta.y);
        mobilFilocontentTransform.sizeDelta = new Vector2(mobilFiloContentWidth, GetComponent<RectTransform>().sizeDelta.y - chatBox.GetComponent<RectTransform>().sizeDelta.y);
        emptyContentSize = mobilcontentTransform.sizeDelta.y;
        filoemptyContentSize = mobilFilocontentTransform.sizeDelta.y;
        chatItems = new List<string>();
        chatItems2 = new List<string>();
        chatItems3 = new List<string>();
        filoChatItems = new List<string>();
#endif
    }
    public void LocalButtonActive()
    {
        LocalButonAcik.SetActive(true);
        LocalButonKpali.SetActive(false);
        ServerButonKapali.SetActive(true);
        ServerButtonAcik.SetActive(false);
        LocalText.SetActive(true);
        ServerText.SetActive(false);
    }
    public void ServerButtonActive()
    {
        LocalButonAcik.SetActive(false);
        LocalButonKpali.SetActive(true);
        ServerButonKapali.SetActive(false);
        ServerButtonAcik.SetActive(true);
        LocalText.SetActive(false);
        ServerText.SetActive(true);
    }

    public void GuildButtonActive()
    {
        LocalButonAcik.SetActive(false);
        LocalButonKpali.SetActive(true);
        ServerButonKapali.SetActive(true);
        ServerButtonAcik.SetActive(false);
        LocalText.SetActive(false);
        ServerText.SetActive(false);
    }

    public void chatAc()
    {
        ChatButton.SetActive(false);
        Panel.SetActive(true);
        AnayüzChatMenu.SetActive(false);
    }
    public void chatKapa()
    {
        ChatButton.SetActive(true);
        Panel.SetActive(false);
        AnayüzChatMenu.SetActive(true);
        OyuniciUI.SetActive(true);

    }
    public void windowsGlobalMesajAc()
    {
        windowsFiloChat.SetActive(false);
        windowsGlobalChat.SetActive(true);

        FiloChatAcikButton.SetActive(false);
        FiloChatKapaliButton.SetActive(true);
        GlobalChatAcikButton.SetActive(true);
        GlobalChatKapaliButton.SetActive(false);
    }
    public void windowsFiloMesajAc()
    {
        windowsFiloChat.SetActive(true);
        windowsGlobalChat.SetActive(false);

        FiloChatAcikButton.SetActive(true);
        FiloChatKapaliButton.SetActive(false);
        GlobalChatAcikButton.SetActive(false);
        GlobalChatKapaliButton.SetActive(true);

    }
    public void GlobalMesaJyolla()
    {

# if UNITY_STANDALONE_WIN
        if (WindowschatBox.text.Length > 0)
        {
            if (windowsGlobalChat.activeSelf)
            {
                GameManager.gm.BenimGemim.GetComponent<Player>().SendMessageGlobal(WindowschatBox.text);
                WindowschatBox.text = "";
                WindowschatBox.DeactivateInputField();
            }
            else if (windowsFiloChat.activeSelf)
            {
                GameManager.gm.BenimGemim.GetComponent<Player>().SendMessageFilo(WindowschatBox.text);
                WindowschatBox.text = "";
                WindowschatBox.DeactivateInputField();
            }
        }
#endif
#if UNITY_ANDROID
        if (chatBox.text.Length > 0)
        {
            if (LocalButonAcik.activeSelf)
            {
                GameManager.gm.BenimGemim.GetComponent<Player>().SendMessageGlobal(chatBox.text);
                chatBox.text = "";
                chatBox.DeactivateInputField();
            }
            else if (ServerButtonAcik.activeSelf)
            {
                GameManager.gm.BenimGemim.GetComponent<Player>().SendMessageFilo(chatBox.text);
                chatBox.text = "";
                chatBox.DeactivateInputField();
            }
        }
#endif

    }

    public void AddMessageToChat(string message)
    {
#if UNITY_STANDALONE_WIN
        float scrollPosition = WindowschatView.verticalNormalizedPosition;
        float height = WindowscontentTextGenerator.GetPreferredHeight(message, contentTextGeneratorSettings);
        AddNewMessage(message, (int)(height / oneLineHeigth));
        contentText.text = BuildContentString();
        WindowscontentTransform.sizeDelta = new Vector2(WindowscontentWidth, Mathf.Max(contentText.preferredHeight, emptyContentSize));
        WindowschatView.verticalNormalizedPosition = scrollPosition;
        chatItems2.Add(message);
#endif

#if UNITY_ANDROID
        float scrollPosition = mobilchatView.verticalNormalizedPosition;
        float height = mobilcontentTextGenerator.GetPreferredHeight(message, contentTextGeneratorSettings);
        AddNewMessage(message, (int)(height / oneLineHeigth));
        mobilcontentText.text = BuildContentString();
        mobilcontentTransform.sizeDelta = new Vector2(mobilcontentWidth, Mathf.Max(mobilcontentText.preferredHeight, emptyContentSize));
        mobilchatView.verticalNormalizedPosition = scrollPosition;
        chatItems2.Add(message);
        AnaYüzChat.text = BuildContentString2();
        if (chatItems2.Count > AnaYüzChatMaxMessageCount)
        {
            chatItems2.RemoveAt(0);
        }
        Invoke("sohbetMobilMesajSilme", 7f);
#endif
    }

    public void AddMessageToKillTable(string message)
    {
        chatItems3.Add(message);
        AnaYüzKillTable.text = BuildContentStringKillTable();
        if (chatItems3.Count > AnaYüzKillTableChatMaxMessageCount)
        {
            chatItems3.RemoveAt(0);
        }
    }

    public void AddMessageToFiloChat(string message)
    {
#if UNITY_STANDALONE_WIN
         float scrollPosition = WindowsFilochatview.verticalNormalizedPosition;
        float height = WindowsFilocontentTextGenerator.GetPreferredHeight(message, contentTextGeneratorSettingsFilo);
        AddNewMessageFilo(message);
        FilocontentText.text = BuildContentFiloString();
        WindowsFilocontentTransform.sizeDelta = new Vector2(WindowsFilocontentWidth, Mathf.Max(FilocontentText.preferredHeight, filoemptyContentSize));
        WindowsFilochatview.verticalNormalizedPosition = scrollPosition;
        chatItems2.Add(message);
#endif

#if UNITY_ANDROID
        float scrollPosition = mobilFilochatview.verticalNormalizedPosition;
        float height = mobilFilocontentTextGenerator.GetPreferredHeight(message, contentTextGeneratorSettingsFilo);
        AddNewMessageFilo(message);
        mobilFilocontentText.text = BuildContentFiloString();
        mobilFilocontentTransform.sizeDelta = new Vector2(mobilFiloContentWidth, Mathf.Max(mobilFilocontentText.preferredHeight, filoemptyContentSize));
        mobilFilochatview.verticalNormalizedPosition = scrollPosition;
        chatItems2.Add(message);
        AnaYüzChat.text = BuildContentString2();
        if (chatItems2.Count > AnaYüzChatMaxMessageCount)
        {
            chatItems2.RemoveAt(0);
        }
        Invoke("sohbetMobilMesajSilme", 7f);
#endif
    }

    public void sohbetMobilMesajSilme()
    {
        if (chatItems2.Count > 0)
        {
            chatItems2.RemoveAt(0);
            AnaYüzChat.text = BuildContentString2();
        }
    }
    public void sohbetDeletKillTableMessage()
    {
        if (chatItems3.Count > 0)
        {
            chatItems3.RemoveAt(0);
            AnaYüzKillTable.text = BuildContentStringKillTable();
        }
    }
    public void AddNewMessage(string message, int linesCount)
    {
        chatItems.Add(message);
        if (chatItems.Count > maxMessagesCount)
        {
            chatItems.RemoveAt(0);
        }
    }
    public void AddNewMessageFilo(string message)
    {
        filoChatItems.Add(message);
        if (filoChatItems.Count > maxMessagesCount)
        {
            filoChatItems.RemoveAt(0);
        }
    }

    private string BuildContentString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < chatItems.Count; i++)
        {
            if (i < chatItems.Count - 1)
            {
                sb.AppendLine(chatItems[i]);
            }
            else
            {
                sb.Append(chatItems[i]);
            }
        }
        return sb.ToString();
    }


    private string BuildContentString2()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < chatItems2.Count; i++)
        {
            if (i < chatItems2.Count - 1)
            {
                sb.AppendLine(chatItems2[i]);
            }
            else
            {
                sb.Append(chatItems2[i]);
            }
        }
        return sb.ToString();
    }

    private string BuildContentFiloString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < filoChatItems.Count; i++)
        {
            if (i < filoChatItems.Count - 1)
            {
                sb.AppendLine(filoChatItems[i]);
            }
            else
            {
                sb.Append(filoChatItems[i]);
            }
        }
        return sb.ToString();
    }
    private string BuildContentStringKillTable()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < chatItems3.Count; i++)
        {
            if (i < chatItems3.Count - 1)
            {
                sb.AppendLine(chatItems3[i]);
            }
            else
            {
                sb.Append(chatItems3[i]);
            }
        }
        return sb.ToString();
    }
}