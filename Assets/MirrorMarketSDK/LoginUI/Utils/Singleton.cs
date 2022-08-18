public abstract class Singleton<T> where T : class, new()
{
    protected static T _instance = null;
    public static T Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = new T();
            }
            return _instance;
        }
    }

}