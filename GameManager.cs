using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public GameObject talkPanel;

    public Image portraitImg;
    public Text talkText;
    public int talkIndex;
   
    public GameObject scanObject;
    public bool isAction;

    // Start is called before the first frame update
    
    void Start()
    {   //오버로드 Start는 게임매니저를 유니티에서 켜야 작동
        Debug.Log(questManager.CheckQuest());
    }

    public void Action(GameObject scanObj)
    {
        // 스캔오브젝트
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);
        //대화패널 액티브
        talkPanel.SetActive(isAction);
    }
    void Talk(int objId, bool isNpc)
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(objId);
        //대화데이터 생성
        string talkData = talkManager.GetTalk(objId+questTalkIndex, talkIndex);
        Debug.Log("오브제id+퀘스트id+퀘스트인덱스는? "+(objId + questTalkIndex));
        //대화 끝내기
        if(talkData == null)
        {
            isAction = false;
            talkIndex=0;
            Debug.Log(questManager.CheckQuest(objId));
            return; 
        }
        //다음 대사
        if (isNpc)
        {
            talkText.text = talkData.Split(':')[0];
            portraitImg.sprite = talkManager.GetPortrait(objId, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0);
        }
        isAction = true;
        talkIndex++;
    }
}
