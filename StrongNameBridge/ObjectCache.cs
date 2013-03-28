using System;
using System.Collections.Generic;

namespace StrongNameBridge
{
    class ObjectCache : Dictionary<Guid, DynamicObject> 
    {
        public Guid Add(DynamicObject obj)
        {
            var objectKey = Guid.NewGuid();
            Add(objectKey, obj);
            return objectKey;
        } 

        public DynamicObject Get(Guid objectKey)
        {
            DynamicObject obj;
            if (!TryGetValue(objectKey, out obj)) throw new Exception("Object Key does not exist within this libraries object cache!");
            return obj;
        }
    }
}
