//-------------------------------------------------------------------------------------
//	SingletonPatternExample4.cs
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

namespace SingletonPatternExample4
{
    public class SingletonPatternExample4 : MonoBehaviour
    {
        void Start()
        {
            FileSystem.Instance.Show();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                FileSystem.Instance.Show();
            }
        }
    }

    /// <summary>
    /// 某单例manager
    /// </summary>
    public class FileSystem : GameSystem
    {
        private static FileSystem instance;
        public static FileSystem Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.Log("Property");
                    instance = new FileSystem();
                }
                return instance;
            }
        }
        /// <summary>
        /// 静态构造函数用于初始化任何静态数据，或执行仅需执行一次的特定操作。 将在创建第一个实例或引用任何静态成员之前自动调用静态构造函数。
        /// </summary>
        static FileSystem()
        {
            if (instance == null)
            {
                instance = new FileSystem();
            }
        }
        private FileSystem()
        {
            Debug.Log("FileSystem Construction.");
        }

        /// <summary>
        /// 随便某方法
        /// </summary>
        public void Show()
        {
            Debug.Log("FileSystem is a Singleton ! " + this.GetHashCode());
            GetLogger().GetLog();
        }
    }
    public class GameSystem
    {
        private Log logger;

        protected GameSystem()
        {
            logger = new Log();
            Debug.Log("GameSystem Construction.");
        }
        protected Log GetLogger() { return logger; }
    }

    public class Game
    {

        private static Game _instance;
        private FileSystem _fileSystem;
        private Log _log;

        public static Game Instance
        {
            get { return _instance; }
        }

        //……

        public FileSystem GetFileSystem() { return _fileSystem; }
        public Log GetLog() { return _log; }
    }

    public class Log
    {
        public void GetLog()
        {
            Debug.Log("I can log.");
        }
    }

}