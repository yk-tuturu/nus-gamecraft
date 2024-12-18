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
        3.19f, 3.778f, 4.66f, 4.954f, 5.542f, 6.131f, 6.719f, 7.307f, 7.895f, 8.484f, 9.324336134428572f, 10.248f, 10.837f, 11.425f, 12.013f, 12.601f, 13.189f, 13.609168067214286f, 14.366f, 14.954f, 15.542f, 16.719f, 17.307f, 17.727168067214283f, 18.484f, 19.072f, 19.66f, 20.080168067214284f, 20.542f, 21.425f, 22.013f, 22.601f, 23.021168067214283f, 23.778f, 24.198168067214283f, 24.954f, 25.164084033607143f, 26.131f, 26.719f, 27.307f, 27.727168067214283f, 28.484f, 29.072f, 29.66f, 30.248f, 30.837f, 31.425f, 32.013f, 32.601f, 33.189f, 33.778f, 34.366f, 34.576084033607145f, 35.542f, 36.131f, 36.719f, 37.307f, 37.895f, 38.484f, 39.072f, 39.28208403360715f, 40.248f, 40.837f, 41.425f, 42.013f, 42.601f, 43.189f, 43.778f, 43.988084033607144f, 44.954f, 45.542f, 46.131f, 46.719f, 47.307f, 47.895f, 48.778f, 49.66f, 50.248f, 50.837f, 51.425f, 52.012f, 52.601f, 53.189f, 53.399084033607146f, 54.365f, 54.954f, 55.542f, 56.131f, 56.719f, 57.308f, 57.896f, 58.10608403360715f, 59.072f, 59.66f, 60.248f, 60.837f, 61.425f, 62.013f, 62.602f, 62.81208403360714f, 63.778f, 64.366f, 64.954f, 65.542f, 66.13f, 66.719f, 67.601f, 68.484f, 69.072f, 69.66f, 70.08016806721429f, 70.837f, 71.425f, 72.013f, 72.22308403360715f, 73.19f, 73.778f, 74.1981680672143f, 74.955f, 75.543f, 76.131f, 76.55116806721429f, 77.013f, 77.896f, 78.484f, 79.072f, 79.4921680672143f, 80.249f, 80.66916806721429f, 81.425f, 81.63508403360714f, 82.602f, 83.19f, 83.778f, 84.1981680672143f, 84.955f, 85.543f, 86.131f, 86.425f, 87.307f, 87.895f, 88.484f, 89.072f, 89.659f, 90.248f, 90.836f, 91.04608403360714f, 92.012f, 92.601f, 93.189f, 93.778f, 94.366f, 94.955f, 95.543f, 95.75308403360715f, 96.719f, 97.307f, 97.895f, 98.484f, 99.072f, 99.66f, 100.249f, 100.45908403360714f, 101.425f, 102.013f, 102.601f, 103.189f, 103.777f, 104.366f, 104.954f, 105.16408403360714f, 106.131f, 106.719f, 107.895f, 109.072f, 110.248f, 111.425f, 112.601f, 113.778f
    };

    public AudioSource audio;
    public AudioClip hitSound;

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

            //audio.PlayOneShot(hitSound);
        }

        //DEBUG
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
