using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    public static PatternManager instance;
    List<Vector3> pat1 = new List<Vector3>() {
        new Vector3(-2.8f, 0.97f, 0f),
        new Vector3(-0.43f, -0.93f, 0f),
        new Vector3(2.17f, 0.97f, 0f)
    };

    List<Vector3> pat2 = new List<Vector3>() {
        new Vector3(-2.03f, -0.93f, 0f),
        new Vector3(-0.34f, 0.55f, 0f),
        new Vector3(1.18f, -0.89f, 0f)
    };

    public List<List<Vector3>> patternList = new List<List<Vector3>>();

    public List<float> beatTimings = new List<float>() {
        0.5f, 1.5f, 2.5f, 3.5f, 4.5f, 5.5f, 6.5f, 7.5f
    };

    void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        patternList.Add(pat1);
        patternList.Add(pat2);
    }

    void Update() {
        float songPos = MusicManager.instance.songPosition;
        if (songPos > beatTimings[0]) {
            float temp = beatTimings[0];
            beatTimings.RemoveAt(0);
            beatTimings.Add(temp + MusicManager.instance.songLength);
        }
    }

    public List<Vector3> getPattern() {
        int index = Random.Range(0, patternList.Count);

        List<Vector3> newList = new List<Vector3>();
        for (int i = 0; i < patternList[index].Count; i++) {
            newList.Add(new Vector3(patternList[index][i].x, patternList[index][i].y, patternList[index][i].z));
        }
        return newList;
    }

    public List<float> getBeats(float start, int count) {
        int i = 0;
        int counter = 0;

        List<float> beats = new List<float>();
        while (counter < count) {
            if (beatTimings[i] > start) {
                beats.Add(beatTimings[i]);
                counter++;
            }
            i++;
        }

        return beats;
    } 
}
