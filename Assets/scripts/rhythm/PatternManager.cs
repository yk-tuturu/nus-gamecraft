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
        3.19f, 3.778f, 4.219176470575f, 4.954f, 5.248f, 5.542f, 6.1302352941f, 6.425f, 7.0132352941f, 7.307f, 7.601117647050001f, 7.895f, 8.484f, 8.778f, 9.3662352941f, 9.66f, 9.954f, 10.248f, 10.8362352941f, 11.131f, 11.7192352941f, 12.013f, 12.30711764705f, 12.601f, 13.189f, 13.631f, 14.072f, 14.366f, 14.659f, 14.953f, 15.5412352941f, 15.836f, 16.4242352941f, 16.718f, 17.01211764705f, 17.306f, 17.895f, 18.189f, 18.48311764705f, 18.777f, 19.071f, 19.365f, 19.659f, 20.247235294099998f, 20.542f, 21.1302352941f, 21.424f, 21.71811764705f, 22.012f, 22.601f, 23.042f, 23.484f, 23.777f, 24.071f, 24.365f, 24.953235294099997f, 25.248f, 25.8362352941f, 26.13f, 26.42411764705f, 26.718f, 27.307f, 27.601f, 27.89511764705f, 28.189f, 28.483f, 28.777f, 29.071f, 29.6592352941f, 29.954f, 30.5422352941f, 30.836f, 31.13011764705f, 31.425f, 32.0132352941f, 32.307f, 32.60111764705f, 32.895f, 33.189f, 33.48311764705f, 33.778f, 34.3662352941f, 34.66f, 34.954f, 35.248f, 35.542f, 36.130235294100004f, 36.425f, 36.719f, 37.013f, 37.6012352941f, 37.895f, 38.189117647050004f, 38.484f, 38.77811764705f, 39.072f, 39.366117647050004f, 39.66f, 39.954f, 40.248f, 40.54211764705f, 40.837f, 41.425235294100005f, 41.719f, 42.013f, 42.30711764705f, 42.601f, 42.895f, 43.189f, 43.7772352941f, 44.072f, 44.366f, 44.66f, 44.954f, 45.24811764705f, 45.542f, 46.130235294100004f, 46.425f, 46.719f, 47.01311764705f, 47.307f, 47.601f, 47.895f, 48.483235294100005f, 48.778f, 49.66035294115f, 50.248f, 50.8362352941f, 51.13f, 51.424117647050004f, 51.718f, 52.012f, 52.30611764705f, 52.601f, 53.1892352941f, 53.483f, 53.778f, 54.072f, 54.365f, 54.953235294100004f, 55.248f, 55.542f, 55.836f, 56.4242352941f, 56.718f, 57.012117647050005f, 57.307f, 57.60111764705f, 57.895f, 58.189117647050004f, 58.484f, 58.778f, 59.071f, 59.36511764705f, 59.66f, 60.2482352941f, 60.542f, 60.836f, 61.13011764705f, 61.424f, 61.718f, 62.012f, 62.6002352941f, 62.895f, 63.189f, 63.484f, 63.777f, 64.07111764705f, 64.365f, 64.9532352941f, 65.248f, 65.542f, 65.83611764705f, 66.13f, 66.424f, 66.718f, 67.3062352941f, 67.601f, 68.48335294115f, 69.072f, 69.66f, 70.248f, 70.542f, 70.837f, 71.13f, 71.424f, 72.01223529410001f, 72.307f, 72.8952352941f, 73.189f, 73.48311764705f, 73.777f, 74.366f, 74.66f, 74.95411764705f, 75.248f, 75.542f, 75.836f, 76.13f, 76.7182352941f, 77.013f, 77.60123529410001f, 77.895f, 78.18911764705f, 78.483f, 79.072f, 79.513f, 79.954f, 80.248f, 80.542f, 80.836f, 81.4242352941f, 81.719f, 82.3072352941f, 82.601f, 82.89511764705f, 83.189f, 83.778f, 84.072f, 84.36611764705f, 84.66f, 84.954f, 85.248f, 85.542f, 86.1302352941f, 86.425f, 87.0132352941f, 87.307f, 87.60111764705f, 87.895f, 88.4832352941f, 88.777f, 89.07111764705f, 89.365f, 89.659f, 89.95311764705001f, 90.248f, 90.8362352941f, 91.131f, 91.42511764705f, 92.012f, 92.6002352941f, 92.895f, 93.189f, 93.483f, 94.0712352941f, 94.365f, 94.65911764705f, 94.954f, 95.24811764705f, 95.542f, 95.83611764705f, 96.131f, 96.719f, 97.013f, 97.307f, 97.8952352941f, 98.189f, 98.483f, 98.77711764705f, 99.071f, 99.365f, 99.659f, 100.24723529410001f, 100.542f, 101.131f, 101.425f, 101.71911764705f, 102.012f, 102.6002352941f, 102.895f, 103.189f, 103.48311764705f, 103.777f, 104.071f, 104.365f, 104.65911764705f, 104.954f, 105.248f, 106.13035294115001f, 106.719f, 107.895f, 109.072f, 110.248f, 110.837f, 111.425f, 112.601f, 113.778f, 114.954f
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
