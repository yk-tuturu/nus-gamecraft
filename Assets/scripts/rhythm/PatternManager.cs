using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    public static PatternManager instance;
    List<Vector3> pat1 = new List<Vector3>() {
        new Vector3(-1.35f,0.45f,0),
        new Vector3(-0.2f,-0.64f,0),
        new Vector3(0.9f,-0.43f,0)
    };

    List<Vector3> pat2 = new List<Vector3>() {
        new Vector3(-1.35f,-0.44f,0),
        new Vector3(-0.2f,0.64f,0),
        new Vector3(0.9f,-0.42f,0)
    };

    List<Vector3> pat3 = new List<Vector3>() {
        new Vector3(-0.36f,0.89f,0),
        new Vector3(-1.29f,-0.01f,0),
        new Vector3(0.41f,-1.03f,0)
    };

    List<Vector3> pat4 = new List<Vector3>() {
        new Vector3(0.12f,1.02f,0),
        new Vector3(1.22f,0.38f,0),
        new Vector3(-0.1f,-1.06f,0)
    };

    List<Vector3> pat5 = new List<Vector3>() {
        new Vector3(-1.11f,0.74f,0),
        new Vector3(-0.21f,-0.16f,0),
        new Vector3(0.73f,-1.07f,0)
    };

    List<Vector3> pat6 = new List<Vector3>() {
        new Vector3(0.83f,0.74f,0),
        new Vector3(-0.04f,-0.1f,0),
        new Vector3(-0.96f,-0.96f,0)
    };


    public List<List<Vector3>> patternList = new List<List<Vector3>>();

    public List<float> beatTimings = new List<float>() {
        0.117f, 2.643f, 3.906f, 5.169f, 7.695f, 8.959f, 9.59f, 10.222f, 11.801f, 12.432f, 12.748f, 14.327f, 15.274f, 16.853f, 17.485f, 17.801f, 19.38f, 20.327f, 21.906f, 22.538f, 22.853f, 24.432f, 25.064f, 25.38f, 26.959f, 27.906f, 29.169f, 29.485f, 30.432f, 31.38f, 32.011f, 32.327f, 32.959f, 34.222f, 35.485f, 36.432f, 37.064f, 37.695f, 38.011f, 39.59f, 40.538f, 41.485f, 42.432f, 43.064f, 44.327f, 44.643f, 45.59f, 46.538f, 47.169f, 47.801f, 48.117f, 49.538f, 50.327f, 50.643f, 51.59f, 52.222f, 53.169f, 54.117f, 54.432f, 55.38f, 55.695f, 56.011f, 57.274f, 57.906f, 58.222f, 59.801f, 60.432f, 60.748f, 61.695f, 62.643f, 63.274f, 64.222f, 64.853f, 65.485f, 65.801f, 66.748f, 67.38f, 68.327f, 70.853f, 71.801f, 72.117f, 72.748f, 73.38f, 74.327f, 74.959f, 75.906f, 76.853f, 77.485f, 78.432f, 79.38f, 79.695f, 80.327f, 80.959f, 81.906f, 82.222f, 82.853f, 83.485f, 84.432f, 85.064f, 86.011f, 86.959f, 87.906f, 88.538f, 89.801f, 91.064f, 92.011f, 92.643f, 93.59f, 94.538f, 94.853f, 95.485f, 96.117f, 97.064f, 97.695f, 98.643f, 99.906f, 100.538f, 101.169f, 102.117f, 103.064f, 103.695f, 104.643f, 105.274f, 106.222f, 107.169f, 107.801f, 108.748f, 109.695f, 110.643f, 111.274f, 112.222f, 112.853f, 113.801f, 114.748f, 115.695f, 116.327f, 116.959f, 117.59f, 118.222f, 118.853f, 120.432f, 121.064f
    };

    void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        patternList.Add(pat1);
        patternList.Add(pat2);
        patternList.Add(pat3);
        patternList.Add(pat4);
        patternList.Add(pat5);
        patternList.Add(pat6);
    }

    void Update() {
        float songPos = MusicManager.instance.GetSongPos();
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
