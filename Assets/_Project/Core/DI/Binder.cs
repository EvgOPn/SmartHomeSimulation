using System;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Core.DI
{
    public class Binder : MonoBehaviour
    {
        private static Binder _instance;

        /// <summary>
        /// Call this method only in Awake()
        /// </summary>
        public static void UpdateDependencies()
        {
            _instance = _instance == null ? InitInstance() : _instance;
            _instance.SetupAllDependentReferences();
        }

        private static Binder InitInstance()
        {
            GameObject binder = new GameObject("Binder");
            Binder instance = binder.AddComponent<Binder>();
            return instance;
        }

        private void SetupAllDependentReferences()
        {
            List<object> allDependentObjects = GetDependentObjects();
            for (int index = 0; index < allDependentObjects.Count; index++)
            {
                object monoInstance = allDependentObjects[index];
                FindFieldsWithInjectAttribute(monoInstance, allDependentObjects);
            }
        }
        
        private void FindFieldsWithInjectAttribute(object monoInstance, List<object> allDependentObjects)
        {
            Type instanceType = monoInstance.GetType();
            
            FieldInfo[] fields = instanceType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (FieldInfo field in fields)
            {
                if (field.GetValue(monoInstance) != null)
                {
                    continue;
                }
                
                InjectAttribute [] injectAttributes = (InjectAttribute[]) field.GetCustomAttributes(typeof(InjectAttribute), true);
                foreach (InjectAttribute attribute in injectAttributes)
                {
                    InjectInField(monoInstance, field, attribute, allDependentObjects);
                }
            }
        }

        private void InjectInField(object monoInstance, FieldInfo field, InjectAttribute attribute, List<object> allDependentObjects)
        {
            List<object> foundObjects = new List<object>();
            
            Type fieldType = field.FieldType;
            if (fieldType.IsInterface)
            {
                foundObjects = allDependentObjects.FindAll(dependent => 
                     dependent.GetType().GetInterfaces().ToList().Contains(fieldType));
            }
            else if(fieldType.IsClass)
            {
                foundObjects = allDependentObjects.FindAll(dependent => dependent.GetType() == fieldType);
            }
            
            if (foundObjects.Count > 1)
            {
                PrintWarning(monoInstance as MonoBehaviour, field, " - found more then one object: " + fieldType);
            }

            object foundObject = foundObjects.FirstOrDefault();
            field.SetValue(monoInstance, foundObject);
        }

        private List<object> GetDependentObjects()
        {
            List<object> dependentObjects = new List<object>();

            object[] objects = FindObjectsOfType<MonoBehaviour>();
            for (int objectIndex = 0; objectIndex < objects.Length; objectIndex++)
            {
                object objectInstance = objects[objectIndex];
                Type instanceType = objectInstance.GetType();
                if (instanceType.GetCustomAttributes(typeof(DependentAttribute), true).Length > 0)
                {
                    dependentObjects.Add(objectInstance);
                }
            }

            return dependentObjects;
        }

        public void Start()
        {
            ValidateReferences();
        }

        private void ValidateReferences()
        {
            List<object> allDependentInstances = GetDependentObjects();
            for (int index = 0; index < allDependentInstances.Count; index++)
            {
                object monoInstance = allDependentInstances[index];
                
                Type instanceType = monoInstance.GetType();
                FieldInfo[] fields = instanceType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

                foreach (FieldInfo field in fields)
                {
                    if (field.GetValue(monoInstance) == null)
                    {
                        PrintError(monoInstance as MonoBehaviour, field, " - reference is null");
                    }
                }
            }
        }

        private void OnDestroy()
        {
            _instance = null;
        }
        
        private void PrintWarning(MonoBehaviour instance, FieldInfo field, string message)
        {
            Debug.Log("<color=#e6ac2f><b>[Binder] " + instance.name + ".cs, field:  " + field.FieldType + "  " + field.Name + ": " + message + "</b></color>");
        }
        
        private void PrintError(MonoBehaviour instance, FieldInfo field, string message)
        {
            Debug.Log("<color=#e15656><b>[Binder] " + instance.name + ".cs, field:  " + field.FieldType + "  " + field.Name + ": " + message + "</b></color>");
        }
    }
}