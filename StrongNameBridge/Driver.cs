using System;
using System.IO;
using System.Reflection;

namespace StrongNameBridge
{
    public class Driver
    {
        public Driver(string installationPath, string dllName)
        {
            _dllName = dllName;
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += (s, e) => LoadFromPath(e.Name, installationPath);
            _loadedLibrary = LoadDll(installationPath, dllName);
        }

        public Guid CreateInstance(string typeName)
        {
            var typePath = string.Format("{0}.{1}", _dllName, typeName);
            var type = _loadedLibrary.GetType(typePath);
            var obj = _loadedLibrary.CreateInstance(typePath);
            var dynObject = new DynamicObject(obj, type); 
            return _objectCache.Add(dynObject);
        }

        public T CallMethod<T>(Guid objectKey, string methodName, object[] arguments = null)
        {
            var obj = _objectCache.Get(objectKey);
            return (T)obj.Call(methodName, arguments);
        }

        public T Get<T>(Guid objectKey, string propertyName)
        {
            var obj =_objectCache.Get(objectKey);
            return obj.Get<T>(propertyName);
        }

        public void Set(Guid objectKey, string propertyName, object value)
        {
            var obj = _objectCache.Get(objectKey);
            obj.Set(propertyName, value);
        }

        public void CreateListener(Guid objectKey, string eventName)
        private readonly string _dllName;
        private readonly Assembly _loadedLibrary;
        private readonly ObjectCache _objectCache = new ObjectCache();
         

        private static Assembly LoadFromPath(string dllName, string installPath)
        {
            return LoadDll(dllName, installPath);
        }

        private static Assembly LoadDll(string dllName, string installPath)
        {
            var assemblyPath = Path.Combine(installPath, string.Format("{0}.dll", new AssemblyName(dllName).Name));
            if (!File.Exists(assemblyPath)) throw new Exception(string.Format("Assembly path for {0} not found", assemblyPath));

            return Assembly.LoadFrom(assemblyPath);
        }
    }
}
/* 
 
 * //var eventType = caControl.GetType().GetEvent("m_ATekEditorPartEditedEvent").EventHandlerType;
 * //var del = Delegate.CreateDelegate(eventType, this, "SaveEdit", false);
 * //caControl.GetType().GetEvent("m_ATekEditorPartEditedEvent").AddEventHandler(caControl, del);
    
*/