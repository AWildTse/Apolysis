using UnityEngine;

namespace Apolysis.Interfaces
{
    public interface IUnityService
    {
        float GetDeltaTime();
        float GetAxisRaw(string axisName);
    }

    public class UnityService : IUnityService
    {
        public float GetDeltaTime()
        {
            return Time.deltaTime;
        }

        public float GetAxisRaw(string axisName)
        {
            return Input.GetAxisRaw(axisName);
        }
    }
}