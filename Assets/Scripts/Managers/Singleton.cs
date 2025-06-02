/// <summary>
/// MonoBehaviour를 상속받지 않는 싱글톤 클래스 (인스턴스 호출 시 싱글톤 생성)
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> where T : class, new()
{
    private static volatile T _instance = null;
    private static readonly object _lock = new object();
    
    private Singleton() { }
    
    /// <summary>
    /// 인스턴스 반환
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_instance is null)
            {
                lock (_lock)
                {
                    if (_instance is null)
                    {
                        _instance = new T();
                    }
                }
            }
            return _instance;
        }
    }
}