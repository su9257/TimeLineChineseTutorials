using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]
public class PauseSubGraphAnimationSample : MonoBehaviour
{
    public AnimationClip clip0;
    public AnimationClip clip1;
    PlayableGraph playableGraph;
    AnimationMixerPlayable mixerPlayable;

    void Start()
    {
        // 创建graph、输出节点
        playableGraph = PlayableGraph.Create("PauseSubGraphAnimationSample");
        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());
        // 创建混合器playable，传入参数输入节点是2个
        mixerPlayable = AnimationMixerPlayable.Create(playableGraph, 2);
        playableOutput.SetSourcePlayable(mixerPlayable);
        // 创建AnimationClipPlayable并且连接到混合器
        var clipPlayable0 = AnimationClipPlayable.Create(playableGraph, clip0);
        var clipPlayable1 = AnimationClipPlayable.Create(playableGraph, clip1);
        playableGraph.Connect(clipPlayable0, 0, mixerPlayable, 0);
        playableGraph.Connect(clipPlayable1, 0, mixerPlayable, 1);
        mixerPlayable.SetInputWeight(0, 1.0f);
        mixerPlayable.SetInputWeight(1, 0.0f);

        // 暂停第2个动画
        clipPlayable0.Pause();
        // 播放整张图
        playableGraph.Play();
    }

    void OnDisable()
    {
        // 销毁所有的Playables和PlayableOutputs
        playableGraph.Destroy();
    }
}