using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyObjectPool 
{
    public class MyObjectPool<T> where T : MonoBehaviour
    {
        #region Fields
        private readonly Func<T> _preloadFunc;
        private readonly Action<T> _getAction;
        private readonly Action<T> _returnAction;

        private Queue<T> _pool = new Queue<T>();
        private List<T> _active = new List<T>();
        #endregion

        #region Constructor
        public MyObjectPool(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int defaultCapacity)
        {
            if (preloadFunc == null)
            {
                Debug.LogError("Preload function is null");
                return;
            }

            _preloadFunc = preloadFunc;
            _getAction = getAction;
            _returnAction = returnAction;

            for (int  i = 0; i < defaultCapacity; i++)
            {
                Return(preloadFunc());
            }
        }
        #endregion

        #region Public methods
        public T Get()
        {
            T obj = _pool.Count > 0 ? _pool.Dequeue() : _preloadFunc();
            _getAction(obj);
            _active.Add(obj);

            return obj;
        }

        public void Return(T obj)
        {
            _returnAction(obj);
            _pool.Enqueue(obj);

            _active.Remove(obj);
        }

        public void ReturnAll()
        {
            foreach(T obj in _active)
            {
                Return(obj);
            }
        }
        #endregion
    }
}