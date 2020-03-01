using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]
public class RuntimeControllerSample : MonoBehaviour
{
    public AnimationClip clip;
    public RuntimeAnimatorController controller;
    public float weight;
    PlayableGraph playableGraph;
    AnimationMixerPlayable mixerPlayable;

    void Start()
    {
        // 创建PlayableGraph
        playableGraph = PlayableGraph.Create("RuntimeControllerSample");
        // 创建输出节点，名称为Animation，对象为Animator组件
        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());
        // 创建混合器playable，传入参数输入节点是2个
        mixerPlayable = AnimationMixerPlayable.Create(playableGraph, 2);
        
        // 连接混合器到输出节点
        playableOutput.SetSourcePlayable(mixerPlayable);
        // 创建两个playable
        var clipPlayable = AnimationClipPlayable.Create(playableGraph, clip);
        var ctrlPlayable = AnimatorControllerPlayable.Create(playableGraph, controller);
        // 将两个playable连接到混合器上
        playableGraph.Connect(clipPlayable, 0, mixerPlayable, 0);
        playableGraph.Connect(ctrlPlayable, 0, mixerPlayable, 1);

        // 播放graph
        playableGraph.Play();
    }

    void Update()
    {
        weight = Mathf.Clamp01(weight);
        mixerPlayable.SetInputWeight(0, 1.0f - weight);
        mixerPlayable.SetInputWeight(1, weight);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("Jump");
            Debug.Log("按键1");
        }
    }

    void OnDisable()
    {
        // 销毁所有的Playables和PlayableOutputs
        playableGraph.Destroy();
    }
}