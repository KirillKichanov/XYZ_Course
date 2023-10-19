using System.Reflection;

namespace Tests
{
    public static class ReflectionHelper
    {
        public static T GetPrivateFieldValue<T>(object instance, string fieldName)
        {
            var type = instance.GetType();
            var fieldInfo = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var fieldValue = (T) fieldInfo.GetValue(instance);
            return fieldValue;
        }
    }
}