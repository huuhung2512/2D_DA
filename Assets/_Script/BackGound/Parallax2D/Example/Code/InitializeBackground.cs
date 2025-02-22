using Parallax2D.Modules.Background.Code.Behaviours;
using UnityEngine;

namespace Parallax2D.Example.Code
{
    public class InitializeBackground : SingletonBehavior<InitializeBackground>
    {
        public Transform Player;
        public BackgroundMain BackgroundMain;

        private void Awake() =>
          BackgroundMain.Construct(Player);

        public void UpdatTarget(Transform newTarget)
        {
            if (newTarget == null)
            {
                Debug.LogError("Player mới bị null!");
                return;
            }

            Player = newTarget;

            if (BackgroundMain != null)
            {
                BackgroundMain.Construct(Player);
            }
            else
            {
                Debug.LogError("BackgroundMain vẫn bị null!");
            }
        }

    }
}