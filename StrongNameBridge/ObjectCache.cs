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
    }
}
