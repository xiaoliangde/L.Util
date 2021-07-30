using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace L.Util
{
    //嵌入的资源 注意 区别Resource（资源）类型
    public class ModuleResourceManage
    {
        public List<string> ResourcesNames
        {
            get { return _resourcesNames ??= _assembly.GetManifestResourceNames().ToList(); }
        }

        public Dictionary<string, Stream> _resourceMemory = new Dictionary<string, Stream>();
        private List<string> _resourcesNames;
        private readonly Assembly _assembly;
        public ModuleResourceManage(string moduleFilePath)
        {
            _assembly = Assembly.LoadFile(moduleFilePath);
        }
        public ModuleResourceManage(Assembly assembly)
        {
            _assembly = assembly;
        }

        //ManifestResourceStream类型，派生自UnmanagedMemoryStream
        //暂时不用托管内存流 var memoryStream = new MemoryStream();

        public Stream GetResourceStream(string name)
        {
            lock (_resourceMemory)
            {
                if (_resourceMemory.ContainsKey(name)) return _resourceMemory[name];
            }
            var uMStream = _assembly.GetManifestResourceStream(name);
            if (uMStream == null) return null;
            lock (_resourceMemory)
            {
                _resourceMemory.Add(name,uMStream);
            }
            return uMStream;
        }

        public void ReleaseAll()
        {
            lock (_resourceMemory)
            {
                _resourceMemory.ForEach(t=>t.Value.Dispose());
                _resourceMemory.Clear();
            }
        }
    }
}
