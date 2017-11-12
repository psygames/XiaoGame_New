using System;
using System.Reflection;
 namespace RedStone
{
    public class ObjectHelper
    {
        public static bool TrySetFieldPathValue(string fieldPath, object source, object value)
        {
            object target;
            FieldInfo info;
            if (ParseFieldPath(fieldPath, source, out target, out info))
            {
                if (info != null)
                {
                    info.SetValue(target, value);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool TryGetFieldPathValue(string fieldPath, object source, out object value)
        {
            object target;
            FieldInfo info;
            if (ParseFieldPath(fieldPath, source, out target, out info))
            {
                if (info != null)
                    value = info.GetValue(target);
                else
                    value = target;
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }
        /// <summary>
        /// Parses a field path (e.g. SourceViewField.IndexedValue[2].TargetObject.TargetField) and returns target object and field info.
        /// </summary>
        public static bool ParseFieldPath(string fieldPath, object sourceObject, out object targetObject, out FieldInfo objectFieldInfo)
        {
            object currentObject = sourceObject;
            objectFieldInfo = null;
            targetObject = null;

			if (String.IsNullOrEmpty(fieldPath) || currentObject == null)
                return false;

            var fields = fieldPath.Split('.');
            for (int i = 0; i < fields.Length; ++i)
            {
                // parse index
                int index = -1;
                string indexString = null;
                string fieldName = fields[i];

                int end = fields[i].IndexOf("]");
                if (end > 0)
                {
                    int start = fields[i].IndexOf('[') + 1;
                    if (fieldName[start] == '"' && fieldName[end - 1] == '"')
                    {
                        indexString = fieldName.Substring(start + 1, end - start - 2);
                    }
                    else if (!Int32.TryParse(fieldName.Substring(start, end - start), out index))
                    {
                        //Debug.LogError(String.Format("[MarkUX.301] {0}: Unable to parse field path \"{1}\".", sourceObject, fieldPath));
                        return false;
                    }

                    fieldName = fields[i].Substring(0, start - 1);
                }

                var fieldInfo = currentObject.GetType().GetField(fieldName);
                if (fieldInfo == null)
                {
                    // handle special case if the field name is "Item" (reference to list item)
                    if (String.Equals(fieldName, "Item", StringComparison.OrdinalIgnoreCase))
                    {
                        objectFieldInfo = null;
                        return false;
                    }

                    objectFieldInfo = null;
                    //Debug.LogError(String.Format("[MarkUX.302] {0}: Unable to parse field path \"{1}\". Couldn't find field/view \"{2}\".", sourceObject, fieldPath, fields[i]));
                    return false;
                }

                // is this the last field?
                bool isLastField = i == fields.Length - 1;
                bool isIndexedObject = index != -1 || !String.IsNullOrEmpty(indexString);
                if (isLastField && !isIndexedObject)
                {
                    objectFieldInfo = fieldInfo;
                    targetObject = currentObject;
                    break;
                }
                // get next object
                currentObject = fieldInfo.GetValue(currentObject);

                if (isIndexedObject)
                {
                    // indexed object
                    var getItemMethod = currentObject.GetType().GetMethod("get_Item", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (getItemMethod == null)
                    {
                        //Debug.LogError(String.Format("[MarkUX.305] {0}: Unable to parse field path \"{1}\". Unable to retrieve indexed object \"{2}\".", sourceObject, fieldPath, fields[i]));
                        return false;
                    }
                    if (index != -1)
                        currentObject = getItemMethod.Invoke(currentObject, new object[] { index });
                    else
                        currentObject = getItemMethod.Invoke(currentObject, new object[] { indexString });

                    if (isLastField)
                    {
                        targetObject = currentObject;
                    }
                }

                if (currentObject == null)
                {
                    // object along path was null
                    return false;
                }
            }

            return true;
        }
        public static void CopyData<T1,T2>(T1 srcData,T2 desData)
        {
            FieldInfo[] srcFields = typeof(T1).GetFields();
            Type desType = typeof(T2);
            foreach(FieldInfo field in srcFields)
            {
                FieldInfo desField = desType.GetField(field.Name);
                desField.SetValue(desData, field.GetValue(srcData));
            }
        }
    }
}