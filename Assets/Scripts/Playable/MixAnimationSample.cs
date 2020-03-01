using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]
public class MixAnimationSample : MonoBehaviour
{
    public AnimationClip clip0;
    public AnimationClip clip1;
    public float weight;
    PlayableGraph playableGraph;
    // 记录下这个混合Playable，因为需要实时修改权重
    AnimationMixerPlayable mixerPlayable;
    void Start()
    {
        // 创建PlayableGraph
        playableGraph = PlayableGraph.Create("MixAnimationSample");
        // 创建输出节点，名称为Animation，对象为Animator组件
        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());
        // 创建混合器playable，传入参数输入节点是2个
        mixerPlayable = AnimationMixerPlayable.Create(playableGraph, 20);
        // 连接混合器到输出节点
        playableOutput.SetSourcePlayable(mixerPlayable);
        // 创建两个animation剪辑的playable
        var clipPlayable0 = AnimationClipPlayable.Create(playableGraph, clip0);
        var clipPlayable1 = AnimationClipPlayable.Create(playableGraph, clip1);
        // 将两个clipplayable连接到混合器上
        playableGraph.Connect(clipPlayable0, 0, mixerPlayable, 0);
        playableGraph.Connect(clipPlayable1, 0, mixerPlayable, 1);


        // 播放graph
        playableGraph.Play();
    }

    void Update()
    {
        weight = Mathf.Clamp01(weight);
        mixerPlayable.SetInputWeight(0, 1.0f - weight);
        mixerPlayable.SetInputWeight(1, weight);
    }

    void OnDisable()
    {
        // 销毁所有的Playables和PlayableOutputs
        playableGraph.Destroy();
    }
}