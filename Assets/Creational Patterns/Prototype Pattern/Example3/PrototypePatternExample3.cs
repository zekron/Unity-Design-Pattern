//-------------------------------------------------------------------------------------
//	PrototypePatternExample2.cs
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System;

namespace PrototypePatternExample3
{
    public class PrototypePatternExample3 : MonoBehaviour
    {
        void Start()
        {
            Ghost ghostPrototype = new Ghost("Normal Ghost", 20, 10, new Skill("Roar"));
            Ghost ghostWizardPrototype = new Ghost("Ghost Wizard", ghostPrototype, new Skill("Nova"));
            Spawner ghostSpawner = new Spawner(ghostPrototype);

            Ghost cloneGhost = ghostSpawner.SpawnMonster() as Ghost;
            Debug.LogFormat("Prototype hash: {0}, clone hash: {1}.", ghostPrototype.GetHashCode(), cloneGhost.GetHashCode());
            Debug.LogFormat("Prototype name: {0}", ghostWizardPrototype.Name);

            Spawner ghostSpawner2 = new Spawner(Ghost.SpawnGhost("Normal Ghost", 20, 10, new Skill("Roar")));
        }
    }

    public interface IMonster
    {
        IMonster ShallowClone();
        IMonster DeepClone();
    }

    public class Skill
    {
        private string[] _skills;

        public Skill(params string[] skills)
        {
            _skills = skills;
        }

        public string[] Skills
        {
            get { return _skills; }
        }
    }

    public class Ghost : IMonster
    {
        private string _name;
        private int _health;
        private int _speed;
        private Skill _skills;

        #region 属性
        public string Name
        {
            get { return _name; }
        }

        public Skill Skills
        {
            get { return _skills; }
        }
        #endregion

        #region 构造函数
        public Ghost(string name, int health, int speed)
        {
            _name = name;
            _health = health;
            _speed = speed;
        }

        public Ghost(Ghost prototype)
        {
            _name = prototype._name;
            _health = prototype._health;
            _speed = prototype._speed;
        }

        public Ghost(string name, int health, int speed, Skill skills)
        {
            _name = name;
            _health = health;
            _speed = speed;
            _skills = new Skill(skills.Skills);
        }

        public Ghost(string name, Ghost prototype, Skill skills)
        {
            _name = name;
            _health = prototype._health;
            _speed = prototype._speed;
            _skills = new Skill(skills.Skills);
        }
        #endregion

        public IMonster ShallowClone()
        {
            return (Ghost)this.MemberwiseClone();
        }

        public IMonster DeepClone()
        {
            try
            {
                return new Ghost(_name, _health, _speed, new Skill(_skills.Skills));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static IMonster SpawnGhost(string name, int health, int speed, Skill skills)
        {
            return new Ghost(name, health, speed, new Skill(skills.Skills));
        }
    }

    public delegate IMonster SpawnMonsterEventHandler();
    public class Spawner
    {
        private IMonster _prototype;
        private SpawnMonsterEventHandler _spawn;

        public Spawner(IMonster prototype)
        {
            _prototype = prototype;
        }
        public Spawner(SpawnMonsterEventHandler spawn)
        {
            _spawn = spawn;
        }

        public IMonster SpawnMonster()
        {
            if (_prototype != null)
                return _prototype.DeepClone();
            else
                return _spawn();
        }
    }
}

