using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public class PlayQueueSample : MonoBehaviour
{
    public AnimationClip[] clipsToPlay;
    PlayableGraph playableGraph;

    void Start()
    {
        playableGraph = PlayableGraph.Create("PlayQueueSample");
        // 创建自定义playable的示例，注意记住这个API
        var playQueuePlayable = ScriptPlayable<PlayQueuePlayable>.Create(playableGraph);
        var playQueue = playQueuePlayable.GetBehaviour();
        // 初始化
        playQueue.Initialize(clipsToPlay, playQueuePlayable, playableGraph);
        // 创建输出节点
        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());
        // 连接playable和output
        playableOutput.SetSourcePlayable(playQueuePlayable);
        //Debug.Log($"GetPlayableCount:{playQueuePlayable.GetGraph().GetPlayableCount()}");
        //Debug.Log($"GetRootPlayable:{playQueuePlayable.GetGraph().GetRootPlayable(0).GetInput(0).GetInputCount()}");
        //Debug.Log($"GetRootPlayableCount:{playQueuePlayable.GetGraph().GetRootPlayableCount()}");
        // 播放graph
        playableGraph.Play();
    }

    void OnDisable()
    {
        // 销毁所有的Playables和PlayableOutputs
        playableGraph.Destroy();
    }
}