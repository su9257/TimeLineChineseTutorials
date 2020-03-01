using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public class PlayAnimationUtilitiesSample : MonoBehaviour
{
    public AnimationClip clip;
    PlayableGraph playableGraph;

    void Start()
    {
        // 使用AnimationPlayableUtilities中的方法简化代码
        AnimationPlayableUtilities.PlayClip(GetComponent<Animator>(), clip, out playableGraph);
    }

    void OnDisable()
    {
        // 销毁所有的Playables和PlayableOutputs
        playableGraph.Destroy();
    }
}