using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]
public class PlayWithTimeControlSample : MonoBehaviour
{
    public AnimationClip clip;
    public float time;
    [Range(0,3)]
    public float speed;
    PlayableGraph playableGraph;
    AnimationClipPlayable playableClip;

    void Start()
    {
        // 创建playableGraph
        playableGraph = PlayableGraph.Create("PlayWithTimeControlSample");
        // 创建输出节点
        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());

        // 创建playableClip，用于播放clip
        playableClip = AnimationClipPlayable.Create(playableGraph, clip);
        // 将Playable连接到output
        playableOutput.SetSourcePlayable(playableClip);

        // 暂停playableClip，不会随着时间自动更新
        playableClip.Play();

        // 播放graph
        playableGraph.Play();
    }

    void Update()
    {
        // 手动控制playableClip的时间
        //playableClip.SetTime(time);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playableClip.Pause();
            playableGraph.Stop();
            Debug.Log("按键1");
        }
        playableClip.SetSpeed(speed);
        //playableClip.SetTime(time);
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playableClip.SetSpeed(speed);
            Debug.Log("按键2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playableClip.Play();
            playableGraph.Play();
            Debug.Log("按键3");
        }
    }

    void OnDisable()
    {
        // 销毁所有的Playables和PlayableOutputs
        playableGraph.Destroy();
    }
}