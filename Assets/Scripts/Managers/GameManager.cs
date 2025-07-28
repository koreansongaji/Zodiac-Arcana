using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private TMP_InputField _inputField;
    //---------- 파일 저장 관련 변수 ----------
    private string saveFilePath;
    public SaveData StageData = new SaveData();
    private void Awake()
    {
        base.Awake();
        // 초기화 작업
    }
    // Start is called before the first frame update
    void Start()
    {
        saveFilePath = Path.Combine(Application.dataPath, "savefile.json");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 카드 리스트를 셔플하는 함수
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public List<T> GetShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }
    //----------Json 저장 관련 함수들----------
    /// <summary>
    /// Json 파일로 불러오기
    /// </summary>
    public void SaveDataLoad()
    {
        SaveData data = new SaveData();
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            data = JsonUtility.FromJson<SaveData>(json);
            if(data != null)
            {
                StageData = new SaveData(); // 기존 saveData를 초기화
                StageData = data; // 불러온 데이터를 saveData에 저장
            }
            Debug.Log(json);
        }
        else
        {
            SaveDataSave(); // 파일이 없으면 새로 생성
        }
    }
    /// <summary>
    /// Json 파일로 저장
    /// </summary>
    public void SaveDataSave()
    {
        SaveData data = new SaveData();
        data = StageData; // 현재 saveData를 저장할 데이터로 설정
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
        StageData = data; // 저장 후 saveData에 저장
        Debug.Log(json);
    }
    public void SaveDataDelete()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            StageData = new SaveData(); // saveData를 초기화
        }
    }
    public void SaveDataClear()
    {
        StageData = new SaveData(); // saveData를 초기화
        SaveDataSave(); // 초기화된 데이터를 저장
    }
    public void TestFile()
    {
        int stage = int.Parse(_inputField.text);
        StageData.Stage = stage;
    }
}
