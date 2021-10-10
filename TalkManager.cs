using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;
    public int talkId;

    public Sprite[] portraitArr;
    void Awake()
    {
        //Talk Data
        //NPC A:1000, NPC B:2000
        //Desk:100, Rock:200
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }
    //대화 데이터 생성 DB필요
    void GenerateData()
    {   
        //기본대화
        talkData.Add(1000, new string[] { "우리 마을은 처음이구나:0", "한 번 둘러보도록 해.:1" });
        talkData.Add(2000, new string[] { "잠깐:3", "이 호수대한 전설 들어본적 있니?:2", "긴 이야기라서...하지만 혼자서는 들어가지 않는편이 좋을걸.:1" });
        talkData.Add(100, new string[] { "'누군가 사용했던 흔적이 있는 책상이다.'" });
        talkData.Add(200, new string[] { "'꿈쩍도 않은 것 같은 바위다'" });
        //퀘스트 대화
        talkData.Add(10 + 1000, new string[] { "어서와:0",
                                            "이 마을에 놀라운 전설이 있다는데:1",
                                            "오른쪽 호수 쪽에 루도가 알려줄거야.:0"});
        talkData.Add(11 + 2000, new string[] { "여어.:0",
                                            "호수에 전설에 대해 알고 싶은거야?:1",
                                            "그렇다면 일 하나만 해줬으면 좋겠는데.:0",
                                            "잃어버린 금화를 좀 찾아줬으면 해.:1"});
        talkData.Add(20 + 1000, new string[] { "루도의 동전?:1", "여기보단 루도의 집 주변을 찾아보는게 어때?:2" });
        talkData.Add(20 + 2000, new string[] { "어디서 잃어버린 거지?:1" });
        talkData.Add(20 + 5000, new string[] { "근처에 떨어진 동전을 찾았다."});
        talkData.Add(21 + 2000, new string[] { "엇, 찾아줘서 고마워:2" });

        //초상화
        //0:보통, 1:대화, 2:행복함, 3.화남
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }
    public string GetTalk(int talkDataId, int talkIndex)
    {
        //퀘스트 대사 예외처리
        if (!talkData.ContainsKey(talkDataId))
        {
            if (!talkData.ContainsKey(talkDataId - talkDataId % 10))
            {   //퀘스트 맨처음 대사도 없을땐 기본대사를 한다
                return GetTalk(talkDataId - talkDataId % 100, talkIndex);
            }
            else
            {   //해당퀘스트 진행순서대사가 없을때 퀘스트 첫대사를 한다
                return GetTalk(talkDataId - talkDataId % 10, talkIndex);
            } 
    
        }

        if (talkIndex == talkData[talkDataId].Length)
        {
            return null;
        }
        else
        {
            Debug.Log("GetTalk 출력 토크id " + talkDataId);
            return talkData[talkDataId][talkIndex];
            
        }
    }
    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[ id + portraitIndex ];
    }
}
