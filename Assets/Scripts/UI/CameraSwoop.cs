using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class CameraSwoop : MonoBehaviour
    {
        public Vector3 startPosition;
        public Quaternion startRotation;

        public BattleSceneManager battleSceneManager;

        private Vector3 endPosition;
        private Quaternion endRotation;

        // Start is called before the first frame update
        private void Start()
        {
            endPosition = transform.position;
            endRotation = transform.rotation;
            transform.position = startPosition;
            transform.rotation = startRotation;
            transform.DORotateQuaternion(endRotation, 4f);
            transform.DOMove(endPosition, 5f).OnComplete(CameraFinished);
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public void CameraFinished()
        {
            battleSceneManager.StartBattle();
        }
    }
}