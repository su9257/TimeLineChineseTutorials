using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class MultiOutputSample : MonoBehaviour
{
    public AnimationClip animationClip;
    public AudioClip audioClip;
    PlayableGraph playableGraph;

    void Start()
    {
        // 创建PlayableGraph
        playableGraph = PlayableGraph.Create("MultiOutputSample");
        // 创建动画output
        var animationOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());
        // 创建音频output
        var audioOutput = AudioPlayableOutput.Create(playableGraph, "Audio", GetComponent<AudioSource>());

        // 创建playables
        var animationClipPlayable = AnimationClipPlayable.Create(playableGraph, animationClip);
        var audioClipPlayable = AudioClipPlayable.Create(playableGraph, audioClip, true);
        // 把playables连接到对应的output
        animationOutput.SetSourcePlayable(animationClipPlayable);
        audioOutput.SetSourcePlayable(audioClipPlayable);

        // 播放graph
        playableGraph.Play();
    }

    void OnDisable()
    {
        // 销毁所有的Playables和PlayableOutputs
        playableGraph.Destroy();
    }
}