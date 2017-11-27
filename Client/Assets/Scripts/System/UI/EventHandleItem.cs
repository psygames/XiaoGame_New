using UnityEngine;

namespace RedStone.UI
{
    public class EventHandleItem : MonoBehaviour, IEventHandler
    {
        public bool OnEvent(string sender, params object[] args)
        {
            string methodName = sender;
            System.Reflection.MethodInfo methodInfo = GetType().GetMethod(methodName,
                System.Reflection.BindingFlags.NonPublic //Support private Method
                | System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance);
            if (methodInfo != null)
            {
                methodInfo.Invoke(this, args);
                return true;
            }
            return false;
        }
    }
}
