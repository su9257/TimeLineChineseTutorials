using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class PlayQueuePlayable : PlayableBehaviour
{
    private int m_CurrentClipIndex = -1;
    private float m_TimeToNextClip;
    private Playable mixer;

    // 初始化方法
    public void Initialize(AnimationClip[] clipsToPlay, Playable owner, PlayableGraph graph)
    {
        // 设置输入节点的数量为1
        owner.SetInputCount(1);
        // 创建混合器
        mixer = AnimationMixerPlayable.Create(graph, clipsToPlay.Length);
        // 将混合器连接到playable上，前面俩参数是输出节点及顺序，后面俩参数是输入节点及顺序
        graph.Connect(mixer, 0, owner, 0);
        // 设置各个输入的权重，在这只有一个输入，就设置为1就好
        owner.SetInputWeight(0, 1);
        // 通过循环依次连接动画clip到混合器上面
        for (int clipIndex = 0; clipIndex < mixer.GetInputCount(); ++clipIndex)
        {
           var tempAnimationClipPlayable = AnimationClipPlayable.Create(graph, clipsToPlay[clipIndex]);
            graph.Connect(tempAnimationClipPlayable, 0, mixer, clipIndex);
            // 平均每个节点的权重
            //mixer.SetInputWeight(clipIndex, 1.0f);
            mixer.SetInputWeight(clipIndex, 0.0f);
        }
    }

    // 重写父类的方法，处理每帧的信息
    override public void PrepareFrame(Playable owner, FrameData info)
    {
        if (mixer.GetInputCount() == 0)
            return;

        // 判断是否应该切换到下一个动画clip
        m_TimeToNextClip -= (float)info.deltaTime;

        if (m_TimeToNextClip <= 0.0f)
        {
            m_CurrentClipIndex++;
            if (m_CurrentClipIndex >= mixer.GetInputCount())
                m_CurrentClipIndex = 0;
            var currentClip = (AnimationClipPlayable)mixer.GetInput(m_CurrentClipIndex);
            // 切换clip时将时间重置到0
            currentClip.SetTime(0);
            m_TimeToNextClip = currentClip.GetAnimationClip().length;
        }

        // 根据当前播放的动画设置权重，当前动画的权重设为1，其他为0，这样只播放当前动画
        for (int clipIndex = 0; clipIndex < mixer.GetInputCount(); ++clipIndex)
        {
            if (clipIndex == m_CurrentClipIndex)
                mixer.SetInputWeight(clipIndex, 1.0f);
            else
                mixer.SetInputWeight(clipIndex, 0.0f);
        }
    }
}